import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { stringify } from 'querystring';
import { map } from 'rxjs/operators';
import { CompaniesClient, CompanyDto, CompanyModel } from 'src/app/shared/clients';

@Component({
  selector: 'app-companies-page',
  templateUrl: './companies-page.component.html',
  styleUrls: ['./companies-page.component.scss']
})
export class CompaniesPageComponent implements OnInit {
  dataSource: MatTableDataSource<CompanyDto>;
  companies: CompanyDto[];
  selected: CompanyDto;
  columnsToDisplay = [ 'name', 'address', 'adminName', 'numberOfSites' ];
  isNew = false;


  constructor(private companiesClient: CompaniesClient, private snackBarRef: MatSnackBar) {
    companiesClient.getCompanies().pipe(map(data => {
      this.companies = data;
      this.dataSource = new MatTableDataSource<CompanyDto>(this.companies);
      this.setFilter();
    },
    error => {
      this.snackBarRef.open(error.message, 'Close');
    }
    )).subscribe();
  }

  ngOnInit(): void {
  }

  selectRow(row: CompanyDto): void {
    this.selected = row;
    this.isNew = false;
  }

  onSave(): void {
    console.log('onSave()');
    const model = new CompanyModel({name: this.selected.name, address: this.selected.address});
    if (this.isNew){
      this.companiesClient.createCompany(model)
        .subscribe(_ => {
          console.log('Company created');
          this.selected.adminName = '-';
          this.selected.numberOfSites = 0;
          this.companies.push(this.selected);
          this.dataSource = new MatTableDataSource<CompanyDto>(this.companies);
          this.isNew = false;
          this.selected = null;
          this.setFilter();
        },
        error => {
          this.snackBarRef.open(error.message, 'Close');
        });
    } else {
      this.companiesClient.updateCompany(this.selected.id, model)
        .subscribe(_ => {
          console.log('Company saved');
        },
        error => {
          this.snackBarRef.open(error.message, 'Close');
        });
    }
  }

  onNew(): void {
    this.isNew = true;
    this.selected = new CompanyDto();
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
