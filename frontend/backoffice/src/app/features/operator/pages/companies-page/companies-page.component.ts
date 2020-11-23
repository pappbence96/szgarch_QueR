import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { CompaniesClient, CompanyDto, CompanyModel, ErrorDetails } from 'src/app/shared/clients';

@Component({
  selector: 'app-companies-page',
  templateUrl: './companies-page.component.html',
  styleUrls: ['./companies-page.component.scss']
})
export class CompaniesPageComponent implements OnInit {
  dataSource: MatTableDataSource<CompanyDto>;
  companies: CompanyDto[];
  selected: CompanyDto;
  columnsToDisplay = [ 'name', 'address', 'adminName', 'numberOfSites', 'numberOfEmployees' ];
  isNew = false;
  companyForm: FormGroup;


  constructor(
    private companiesClient: CompaniesClient,
    private snackBarRef: MatSnackBar,
    private formBuilder: FormBuilder
  ) {
    companiesClient.getCompanies().subscribe(data => {
      this.companies = data;
      this.dataSource = new MatTableDataSource<CompanyDto>(this.companies);
      this.setFilter();
    },
    (error: ErrorDetails) => {
      this.snackBarRef.open(error.message, 'Close');
    });
  }

  ngOnInit(): void {
  }

  selectRow(row: CompanyDto): void {
    this.selected = new CompanyDto(row);
    this.isNew = false;

    this.companyForm = this.formBuilder.group({
      name: [this.selected.name, Validators.required],
      address: [this.selected.address, Validators.required]
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
          this.companies.push(created);
          this.dataSource = new MatTableDataSource<CompanyDto>(this.companies);
          this.isNew = false;
          this.selected = null;
          this.setFilter();
        },
        (error: ErrorDetails) => {
          this.snackBarRef.open(error.message, 'Close');
        });
    } else {
      this.companiesClient.updateCompany(this.selected.id, model)
        .subscribe(() => {
          console.log('Company saved');
          const updated = this.companies.find((item: CompanyDto) => item.id === this.selected.id);
          updated.name = this.companyForm.value.name;
          updated.address = this.companyForm.value.address;
        },
        (error: ErrorDetails) => {
          this.snackBarRef.open(error.message, 'Close');
        });
    }
  }

  onNew(): void {
    this.isNew = true;
    this.selected = new CompanyDto();
    this.companyForm = this.formBuilder.group({
      name: ['', Validators.required],
      address: ['', Validators.required]
    });
  }

  onSearchChange(searchValue: string): void {
    console.log(searchValue);
    this.dataSource.filter = searchValue;
  }

  private setFilter(): void {
    this.dataSource.filterPredicate = (company: CompanyDto, filter: string) => {
      return company.name.toLowerCase().indexOf(filter.toLocaleLowerCase()) === 0;
    };
  }
}
