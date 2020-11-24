import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { filter, switchMap } from 'rxjs/operators';
import { ApplicationUserDto, CompaniesClient, CompanyDto, CreateUserModel, ErrorDetails, Gender, UpdateUserModel, UsersClient } from 'src/app/shared/clients';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-administrators-page',
  templateUrl: './administrators-page.component.html',
  styleUrls: ['./administrators-page.component.scss']
})
export class AdministratorsPageComponent implements OnInit {
  dataSource: MatTableDataSource<ApplicationUserDto>;
  admins: ApplicationUserDto[];
  companies: CompanyDto[];
  selected: ApplicationUserDto;
  columnsToDisplay = [ 'userName', 'firstName', 'lastName', 'email', 'gender', 'address', 'administratedCompany' ];
  isNew = false;
  adminForm: FormGroup;
  selectedCompanyId: number;


  constructor(
    private companiesClient: CompaniesClient,
    private userClient: UsersClient,
    private snackBarRef: MatSnackBar,
    private formBuilder: FormBuilder,
    private dialog: MatDialog
  ) {
    userClient.getAdmins().subscribe(data => {
      this.admins = data;
      this.dataSource = new MatTableDataSource<ApplicationUserDto>(this.admins);
      this.setFilter();
    },
    (error: ErrorDetails) => {
      this.snackBarRef.open(error.message, 'Close');
    });
    companiesClient.getCompanies().subscribe(data => {
      this.companies = data;
    },
    (error: ErrorDetails) => {
      this.snackBarRef.open(error.message, 'Close');
    });
  }

  ngOnInit(): void {
  }

  selectRow(row: ApplicationUserDto): void {
    this.selected = new ApplicationUserDto(row);
    this.isNew = false;
    this.selectedCompanyId = this.companies.find((c: CompanyDto) => c.name === this.selected.administratedCompany)?.id || null;
    console.log(this.selectedCompanyId);

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

    console.log('onSubmit()');
    console.log(this.adminForm.value.gender);
    if (this.isNew){
      const model = new CreateUserModel ({
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
          console.log('Admin created');
          this.admins.push(created);
          this.dataSource = new MatTableDataSource<ApplicationUserDto>(this.admins);
          this.isNew = false;
          this.selected = null;
          this.setFilter();
        },
        (error: ErrorDetails) => {
          this.snackBarRef.open(error.message, 'Close');
        });
    } else {
      const model = new UpdateUserModel ({
        firstName: this.adminForm.value.firstName,
        lastName: this.adminForm.value.lastName,
        email: this.adminForm.value.email,
        address: this.adminForm.value.address,
        gender: this.adminForm.value.gender
      });

      this.userClient.updateAdmin(this.selected.id, model)
        .subscribe(() => {
          console.log('Admin saved');
          const updated = this.admins.find((item: ApplicationUserDto) => item.id === this.selected.id);
          updated.firstName = this.adminForm.value.firstName;
          updated.lastName = this.adminForm.value.lastName;
          updated.email = this.adminForm.value.email;
          updated.address = this.adminForm.value.address;
          updated.gender = this.adminForm.value.gender;
        },
        (error: ErrorDetails) => {
          this.snackBarRef.open(error.message, 'Close');
        });
    }
  }

  onNew(): void {
    this.isNew = true;
    this.selected = new ApplicationUserDto();
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
      switchMap(() => this.companiesClient.assignAdminToCompany(this.selectedCompanyId, this.selected.id))
      ).subscribe(() => {
        console.log('Assign successful');
        const updated = this.admins.find((item: ApplicationUserDto) => item.id === this.selected.id);
        updated.administratedCompany = this.companies.find((c: CompanyDto) => c.id === this.selectedCompanyId).name;
      },
      (error: ErrorDetails) => {
        this.snackBarRef.open(error.message, 'Close');
      });
  }

  removeFromCompany(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.companiesClient.removeAdminOfCompany(this.selectedCompanyId))
      ).subscribe(() => {
        console.log('Remove successful');
        const updated = this.admins.find((item: ApplicationUserDto) => item.id === this.selected.id);
        updated.administratedCompany = '-';
        this.selectedCompanyId = null;
      },
      (error: ErrorDetails) => {
        this.snackBarRef.open(error.message, 'Close');
      });
  }

  private setFilter(): void {
    this.dataSource.filterPredicate = (admin: ApplicationUserDto, filterText: string) => {
      return admin.userName.toLowerCase().indexOf(filterText.toLocaleLowerCase()) === 0;
    };
  }

}
