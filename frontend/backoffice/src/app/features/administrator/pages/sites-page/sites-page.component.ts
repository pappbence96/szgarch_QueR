import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { filter, switchMap } from 'rxjs/operators';
import { ErrorDetails, SiteDto, SiteModel, SitesClient } from 'src/app/shared/clients';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { SnackbarService } from 'src/app/shared/utilities/Snackbar.service';

@Component({
  selector: 'app-sites-page',
  templateUrl: './sites-page.component.html',
  styleUrls: ['./sites-page.component.scss']
})
export class SitesPageComponent implements OnInit {
  dataSource: MatTableDataSource<SiteDto>;
  sites: SiteDto[];
  selected: SiteDto;
  columnsToDisplay = [ 'name', 'address', 'managerName', 'numberOfEmployees' ];
  isNew = false;
  siteForm: FormGroup;


  constructor(
    private sitesClient: SitesClient,
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private snackbar: SnackbarService
  ) {
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

  removeManager(siteId: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.sitesClient.removeManagerFromSite(siteId))
      ).subscribe( () => {
        this.snackbar.showSnackbar('Administrator successfully removed.');
        this.sites.find((company) => company.id === siteId).managerName = '-';
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
