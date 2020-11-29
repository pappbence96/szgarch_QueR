import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { filter, switchMap } from 'rxjs/operators';
import { AdministratorDto, CompaniesClient, CompanyDto, CompanyModel, ErrorDetails, UsersClient } from 'src/app/shared/clients';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { SnackbarService } from 'src/app/shared/utilities/Snackbar.service';

@Component({
  selector: 'app-companies-page',
  templateUrl: './companies-page.component.html',
  styleUrls: ['./companies-page.component.scss']
})
export class CompaniesPageComponent implements OnInit {
  dataSource: MatTableDataSource<CompanyDto>;
  columnsToDisplay = [ 'name', 'address', 'adminName', 'numberOfSites', 'numberOfEmployees' ];
  companyForm: FormGroup;

  admins: AdministratorDto[];
  selectedAdmin: AdministratorDto;

  companies: CompanyDto[];
  selected: CompanyDto;
  isNew = false;


  constructor(
    private companiesClient: CompaniesClient,
    usersClient: UsersClient,
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
    this.selectedAdmin = this.admins.find((admin: AdministratorDto) => admin.id === this.selected.adminId);

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

    const model = new CompanyModel({name: this.companyForm.value.name, address: this.companyForm.value.address});
    if (this.isNew){
      this.companiesClient.createCompany(model)
        .subscribe(created => {
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
        this.selected = updated;
        this.selectedAdmin.administratedCompanyId = null;
        this.selectedAdmin = null;
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
      });
  }

  assignAdmin(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.companiesClient.assignAdminToCompany(this.selected.id, this.selectedAdmin.id))
      ).subscribe(() => {
        this.snackbar.showSnackbar('Administrator successfully assigned to the company');
        const updated = this.companies.find((item: CompanyDto) => item.id === this.selected.id);
        updated.adminId = this.selectedAdmin.id;
        updated.adminName = this.selectedAdmin.userName;
        this.selected = updated;
        this.selectedAdmin.administratedCompanyId = this.selected.id;
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

  availableAdmins(): AdministratorDto[] {
    return this.admins.filter(a => a.administratedCompanyId === null);
  }
}
