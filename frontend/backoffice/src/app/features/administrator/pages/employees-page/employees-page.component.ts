import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { filter, switchMap } from 'rxjs/operators';
import { CompaniesClient, CreateWorkerModel, EmployeeDto, ErrorDetails, SiteDto, SitesClient, UpdateUserModel, UpdateWorkerModel, UsersClient } from 'src/app/shared/clients';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { SnackbarService } from 'src/app/shared/utilities/Snackbar.service';

@Component({
  selector: 'app-employees-page',
  templateUrl: './employees-page.component.html',
  styleUrls: ['./employees-page.component.scss']
})
export class EmployeesPageComponent implements OnInit {
  dataSource: MatTableDataSource<EmployeeDto>;
  columnsToDisplay = [ 'userName', 'firstName', 'lastName', 'email', 'gender', 'address', 'worksite' ];
  employeeForm: FormGroup;

  sites: SiteDto[];
  selectedWorksite: SiteDto;

  employees: EmployeeDto[];
  selected: EmployeeDto;
  isNew = false;

  constructor(
    private sitesClient: SitesClient,
    private companiesClient: CompaniesClient,
    private userClient: UsersClient,
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private snackbar: SnackbarService
  ) {
    companiesClient.getEmployeesOfOwnCompany().subscribe(data => {
      this.employees = data;
      this.dataSource = new MatTableDataSource<EmployeeDto>(this.employees);
      this.setFilter();
    },
    (error: ErrorDetails) => {
      snackbar.showSnackbar(error.message);
    });
    companiesClient.getSitesForOwnCompany()
      .subscribe(data => {
        this.sites = data;
      },
      (error: ErrorDetails) => {
        snackbar.showSnackbar(error.message);
    });
  }

  ngOnInit(): void {
  }

  selectRow(row: EmployeeDto): void {
    this.selected = new EmployeeDto(row);
    this.isNew = false;
    this.selectedWorksite = this.sites.find((site: SiteDto) => site.id === this.selected.worksiteId);

    this.employeeForm = this.formBuilder.group({
      userName: [{ value: this.selected.userName, disabled: true }],
      firstName: [this.selected.firstName, Validators.required],
      lastName: [this.selected.lastName, Validators.required],
      email: [this.selected.email, Validators.required],
      gender: [this.selected.gender, Validators.required],
      address: [this.selected.address, Validators.required]
    });
  }

  onSubmit(): void {
    if (!this.employeeForm.valid) {
      return;
    }

    if (this.isNew){
      const model = new CreateWorkerModel ({
        userName: this.employeeForm.value.userName,
        firstName: this.employeeForm.value.firstName,
        lastName: this.employeeForm.value.lastName,
        email: this.employeeForm.value.email,
        address: this.employeeForm.value.address,
        password: this.employeeForm.value.password,
        gender: this.employeeForm.value.gender
      });

      this.userClient.createEmployee(model)
        .subscribe(created => {
          this.snackbar.showSnackbar('Employee created');
          this.employees.push(created);
          this.dataSource = new MatTableDataSource<EmployeeDto>(this.employees);
          this.isNew = false;
          this.selected = null;
          this.setFilter();
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    } else {
      const model = new UpdateWorkerModel ({
        firstName: this.employeeForm.value.firstName,
        lastName: this.employeeForm.value.lastName,
        email: this.employeeForm.value.email,
        address: this.employeeForm.value.address,
        gender: this.employeeForm.value.gender
      });

      this.userClient.updateEmployee(this.selected.id, model)
        .subscribe(() => {
          this.snackbar.showSnackbar('Employee updated');
          const updated = this.employees.find((item: EmployeeDto) => item.id === this.selected.id);
          updated.firstName = this.employeeForm.value.firstName;
          updated.lastName = this.employeeForm.value.lastName;
          updated.email = this.employeeForm.value.email;
          updated.address = this.employeeForm.value.address;
          updated.gender = this.employeeForm.value.gender;
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    }
  }

  onNew(): void {
    this.isNew = true;
    this.selected = new EmployeeDto();
    this.employeeForm = this.formBuilder.group({
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

  assignToWorksite(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.sitesClient.assignWorkerToSite(this.selectedWorksite.id, this.selected.id))
      ).subscribe(() => {
        this.snackbar.showSnackbar('Employee successfully assigned to worksite');
        const updated = this.employees.find((item: EmployeeDto) => item.id === this.selected.id);
        updated.worksiteId = this.selectedWorksite.id;
        updated.worksite = this.selectedWorksite.name;
        this.selected = updated;
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
      });
  }

  removeFromWorksite(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.sitesClient.removeEmployeeOfSite(this.selectedWorksite.id, this.selected.id))
      ).subscribe(() => {
        this.snackbar.showSnackbar('Employee successfully removed from worksite');
        const updated = this.employees.find((item: EmployeeDto) => item.id === this.selected.id);
        updated.worksiteId = null;
        updated.worksite = '-';
        this.selectedWorksite = null;
        this.selected = updated;
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
      });
  }

  private setFilter(): void {
    this.dataSource.filterPredicate = (employee: EmployeeDto, filterText: string) => {
      return employee.userName.toLowerCase().indexOf(filterText.toLocaleLowerCase()) === 0;
    };
  }
}
