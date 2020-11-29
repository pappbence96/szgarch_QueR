import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { filter, switchMap } from 'rxjs/operators';
import {  AdministratorDto, CompaniesClient, CompanyDto, CreateWorkerModel, ErrorDetails, Gender, UpdateUserModel, UpdateWorkerModel, UsersClient } from 'src/app/shared/clients';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { SnackbarService } from 'src/app/shared/utilities/Snackbar.service';

@Component({
  selector: 'app-administrators-page',
  templateUrl: './administrators-page.component.html',
  styleUrls: ['./administrators-page.component.scss']
})
export class AdministratorsPageComponent implements OnInit {
  dataSource: MatTableDataSource<AdministratorDto>;
  columnsToDisplay = [ 'userName', 'firstName', 'lastName', 'email', 'gender', 'address', 'administratedCompany' ];
  adminForm: FormGroup;

  companies: CompanyDto[];
  selectedCompany: CompanyDto;

  admins: AdministratorDto[];
  selected: AdministratorDto;
  isNew = false;

  constructor(
    private companiesClient: CompaniesClient,
    private userClient: UsersClient,
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private snackbar: SnackbarService
  ) {
    userClient.getAdmins().subscribe(data => {
      this.admins = data;
      this.dataSource = new MatTableDataSource<AdministratorDto>(this.admins);
      this.setFilter();
    },
    (error: ErrorDetails) => {
      snackbar.showSnackbar(error.message);
    });
    companiesClient.getCompanies().subscribe(data => {
      this.companies = data;
    },
    (error: ErrorDetails) => {
      snackbar.showSnackbar(error.message);
    });
  }

  ngOnInit(): void {
  }

  selectRow(row: AdministratorDto): void {
    this.selected = new AdministratorDto(row);
    this.isNew = false;
    this.selectedCompany = this.companies.find((company: CompanyDto) => company.id === this.selected.administratedCompanyId);

    this.adminForm = this.formBuilder.group({
      userName: [{ value: this.selected.userName, disabled: true }],
      firstName: [this.selected.firstName, Validators.required],
      lastName: [this.selected.lastName, Validators.required],
      email: [this.selected.email, Validators.required],
      gender: [this.selected.gender, Validators.required],
      address: [this.selected.address, Validators.required]
    });
  }

  onSubmit(): void {
    if (!this.adminForm.valid) {
      return;
    }

    console.log(this.adminForm.value.gender);
    if (this.isNew){
      const model = new CreateWorkerModel ({
        userName: this.adminForm.value.userName,
        firstName: this.adminForm.value.firstName,
        lastName: this.adminForm.value.lastName,
        email: this.adminForm.value.email,
        address: this.adminForm.value.address,
        password: this.adminForm.value.password,
        gender: this.adminForm.value.gender
      });

      this.userClient.createAdmin(model)
        .subscribe(created => {
          this.snackbar.showSnackbar('Administrator created');
          this.admins.push(created);
          this.dataSource = new MatTableDataSource<AdministratorDto>(this.admins);
          this.isNew = false;
          this.selected = null;
          this.setFilter();
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    } else {
      const model = new UpdateWorkerModel ({
        firstName: this.adminForm.value.firstName,
        lastName: this.adminForm.value.lastName,
        email: this.adminForm.value.email,
        address: this.adminForm.value.address,
        gender: this.adminForm.value.gender
      });

      this.userClient.updateAdmin(this.selected.id, model)
        .subscribe(() => {
          this.snackbar.showSnackbar('Administrator updated');
          const updated = this.admins.find((item: AdministratorDto) => item.id === this.selected.id);
          updated.firstName = this.adminForm.value.firstName;
          updated.lastName = this.adminForm.value.lastName;
          updated.email = this.adminForm.value.email;
          updated.address = this.adminForm.value.address;
          updated.gender = this.adminForm.value.gender;
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    }
  }

  onNew(): void {
    this.isNew = true;
    this.selected = new AdministratorDto();
    this.adminForm = this.formBuilder.group({
      userName: [this.selected.userName, Validators.required],
      firstName: [this.selected.firstName, Validators.required],
      lastName: [this.selected.lastName, Validators.required],
      email: [this.selected.email, Validators.required],
      gender: [this.selected.gender, Validators.required],
      address: [this.selected.address, Validators.required],
      password: ['']
    });
  }

  onSearchChange(searchValue: string): void {
    console.log(searchValue);
    this.dataSource.filter = searchValue;
  }

  assingToCompany(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.companiesClient.assignAdminToCompany(this.selectedCompany.id, this.selected.id))
      ).subscribe(() => {
        console.log('Assign successful');
        this.snackbar.showSnackbar('Administrator successfully assigned to company');
        const updated = this.admins.find((item: AdministratorDto) => item.id === this.selected.id);
        updated.administratedCompanyId = this.selectedCompany.id;
        updated.administratedCompany = this.selectedCompany.name;
        this.selected = updated;
        this.selectedCompany.adminId = this.selected.id;
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
      });
  }

  removeFromCompany(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.companiesClient.removeAdminOfCompany(this.selectedCompany.id))
      ).subscribe(() => {
        console.log('Remove successful');
        this.snackbar.showSnackbar('Administrator successfully removed from company');
        const updated = this.admins.find((item: AdministratorDto) => item.id === this.selected.id);
        updated.administratedCompanyId = null;
        updated.administratedCompany = '-';
        this.selected = updated;
        this.selectedCompany.adminId = null;
        this.selectedCompany = null;
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
      });
  }

  private setFilter(): void {
    this.dataSource.filterPredicate = (admin: AdministratorDto, filterText: string) => {
      return admin.userName.toLowerCase().indexOf(filterText.toLocaleLowerCase()) === 0;
    };
  }

  availableCompanies(): CompanyDto[] {
    return this.companies.filter(c => c.adminId === null);
  }
}
