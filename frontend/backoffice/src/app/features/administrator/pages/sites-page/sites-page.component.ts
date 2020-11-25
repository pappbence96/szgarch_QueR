import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { filter, switchMap } from 'rxjs/operators';
import { ApplicationUserDto, ErrorDetails, SiteDto, SiteModel, SitesClient, UsersClient } from 'src/app/shared/clients';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { SnackbarService } from 'src/app/shared/utilities/Snackbar.service';

@Component({
  selector: 'app-sites-page',
  templateUrl: './sites-page.component.html',
  styleUrls: ['./sites-page.component.scss']
})
export class SitesPageComponent implements OnInit {
  dataSource: MatTableDataSource<SiteDto>;
  columnsToDisplay = [ 'name', 'address', 'managerName', 'numberOfEmployees' ];
  siteForm: FormGroup;

  employees: ApplicationUserDto[];
  selectedEmployee: ApplicationUserDto;
  
  sites: SiteDto[];
  selected: SiteDto;
  isNew = false;

  constructor(
    private sitesClient: SitesClient,
    userClient: UsersClient,
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private snackbar: SnackbarService
  ) {
    userClient.getEmployees().subscribe(data => {
      this.employees = data;
    },
    (error: ErrorDetails) => {
      snackbar.showSnackbar(error.message);
    });
    sitesClient.getSites().subscribe(data => {
      this.sites = data;
      this.dataSource = new MatTableDataSource<SiteDto>(this.sites);
      this.setFilter();
    },
    (error: ErrorDetails) => {
      this.snackbar.showSnackbar(error.message);
    });
  }

  ngOnInit(): void {
  }

  selectRow(row: SiteDto): void {
    this.selected = new SiteDto(row);
    this.isNew = false;
    this.selectedEmployee = this.employees.find((employee: ApplicationUserDto) => employee.id === this.selected.managerId);

    this.siteForm = this.formBuilder.group({
      name: [this.selected.name, Validators.required],
      address: [this.selected.address, Validators.required],
      managerName: [{ value: this.selected.managerName, disabled: true}]
    });
  }

  onSubmit(): void {
    if (!this.siteForm.valid) {
      return;
    }

    const model = new SiteModel({name: this.siteForm.value.name, address: this.siteForm.value.address});
    if (this.isNew){
      this.sitesClient.createSite(model)
        .subscribe(created => {
          this.snackbar.showSnackbar('Company created');
          this.sites.push(created);
          this.dataSource = new MatTableDataSource<SiteDto>(this.sites);
          this.isNew = false;
          this.selected = null;
          this.setFilter();
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    } else {
      this.sitesClient.updateSite(this.selected.id, model)
        .subscribe(() => {
          this.snackbar.showSnackbar('Company updated');
          const updated = this.sites.find((item: SiteDto) => item.id === this.selected.id);
          updated.name = this.siteForm.value.name;
          updated.address = this.siteForm.value.address;
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    }
  }

  onNew(): void {
    this.isNew = true;
    this.selected = new SiteDto();
    this.siteForm = this.formBuilder.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      managerName: [{ value: '', disabled: true}]
    });
  }

  onSearchChange(searchValue: string): void {
    console.log(searchValue);
    this.dataSource.filter = searchValue;
  }

  promoteManager(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.sitesClient.assignManagerToSite(this.selected.id, this.selectedEmployee.id))
      ).subscribe(() => {
        this.snackbar.showSnackbar('Manager successfully promoted');
        const updated = this.sites.find((item: SiteDto) => item.id === this.selected.id);
        updated.managerName = this.selectedEmployee.userName;
        updated.managerId = this.selectedEmployee.id;
        this.selected = updated;
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
      });
  }

  demoteManager(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.sitesClient.removeManagerFromSite(this.selected.id))
      ).subscribe(() => {
        this.snackbar.showSnackbar('Manager successfully demoted');
        const updated = this.sites.find((item: SiteDto) => item.id === this.selected.id);
        updated.managerName = '-';
        updated.managerId = null;
        this.selectedEmployee = null;
        this.selected = updated;
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
      });
  }

  private setFilter(): void {
    this.dataSource.filterPredicate = (site: SiteDto, filterText: string) => {
      return site.name.toLowerCase().indexOf(filterText.toLocaleLowerCase()) === 0;
    };
  }
}
