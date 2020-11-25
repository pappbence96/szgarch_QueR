import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { filter, switchMap } from 'rxjs/operators';
import { ApplicationUserDto, CompaniesClient, CompanyDto, CompanyModel, ErrorDetails, UsersClient } from 'src/app/shared/clients';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { SnackbarService } from 'src/app/shared/utilities/Snackbar.service';

@Component({
  selector: 'app-companies-page',
  templateUrl: './companies-page.component.html',
  styleUrls: ['./companies-page.component.scss']
})
export class CompaniesPageComponent implements OnInit {
  dataSource: MatTableDataSource<CompanyDto>;
  admins: ApplicationUserDto[];
  companies: CompanyDto[];
  selected: CompanyDto;
  columnsToDisplay = [ 'name', 'address', 'adminName', 'numberOfSites', 'numberOfEmployees' ];
  isNew = false;
  companyForm: FormGroup;
  selectedAdminId: number;


  constructor(
    private companiesClient: CompaniesClient,
    private usersClient: UsersClient,
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private snackbar: SnackbarService
  ) {
    companiesClient.getCompanies().subscribe(data => {
      this.companies = data;
      this.dataSource = new MatTableDataSource<CompanyDto>(this.companies);
      this.setFilter();
    },
    (error: ErrorDetails) => {
      this.snackbar.showSnackbar(error.message);
    });
    usersClient.getAdmins().subscribe(data => {
      this.admins = data;
    },
    (error: ErrorDetails) => {
      snackbar.showSnackbar(error.message);
    });
  }

  ngOnInit(): void {
  }

  selectRow(row: CompanyDto): void {
    this.selected = new CompanyDto(row);
    this.isNew = false;
    this.selectedAdminId = this.selected.adminId;

    this.companyForm = this.formBuilder.group({
      name: [this.selected.name, Validators.required],
      address: [this.selected.address, Validators.required],
      adminName: [{ value: this.selected.adminName, disabled: true}]
    });
  }

  onSubmit(): void {
    if (!this.companyForm.valid) {
      return;
    }

    console.log('onSubmit()');
    const model = new CompanyModel({name: this.companyForm.value.name, address: this.companyForm.value.address});
    if (this.isNew){
      this.companiesClient.createCompany(model)
        .subscribe(created => {
          console.log('Company created');
          this.snackbar.showSnackbar('Company created');
          this.companies.push(created);
          this.dataSource = new MatTableDataSource<CompanyDto>(this.companies);
          this.isNew = false;
          this.selected = null;
          this.setFilter();
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    } else {
      this.companiesClient.updateCompany(this.selected.id, model)
        .subscribe(() => {
          console.log('Company updated');
          this.snackbar.showSnackbar('Company updated');
          const updated = this.companies.find((item: CompanyDto) => item.id === this.selected.id);
          updated.name = this.companyForm.value.name;
          updated.address = this.companyForm.value.address;
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    }
  }

  onNew(): void {
    this.isNew = true;
    this.selected = new CompanyDto();
    this.companyForm = this.formBuilder.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      adminName: [{ value: '', disabled: true}]
    });
  }

  onSearchChange(searchValue: string): void {
    console.log(searchValue);
    this.dataSource.filter = searchValue;
  }

  removeAdmin(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.companiesClient.removeAdminOfCompany(this.selected.id))
      ).subscribe(() => {
        this.snackbar.showSnackbar('Administrator successfully removed from the company');
        const updated = this.companies.find((item: CompanyDto) => item.id === this.selected.id);
        updated.adminName = '-';
        updated.adminId = null;
        this.selectedAdminId = null;
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
      });
  }

  assignAdmin(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.companiesClient.assignAdminToCompany(this.selected.id, this.selectedAdminId))
      ).subscribe(() => {
        console.log('Remove successful');
        this.snackbar.showSnackbar('Administrator successfully removed from company');
        const updated = this.companies.find((item: CompanyDto) => item.id === this.selected.id);
        const selectedAdmin = this.admins.find((item: ApplicationUserDto) => item.id === this.selectedAdminId);
        updated.adminId = selectedAdmin.id;
        updated.adminName = selectedAdmin.userName;
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
      });
  }

  private setFilter(): void {
    this.dataSource.filterPredicate = (company: CompanyDto, filterText: string) => {
      return company.name.toLowerCase().indexOf(filterText.toLocaleLowerCase()) === 0;
    };
  }
}
