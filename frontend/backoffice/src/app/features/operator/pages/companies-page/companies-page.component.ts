import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import { CompaniesClient, CompanyDto, CompanyModel } from 'src/app/shared/clients';

@Component({
  selector: 'app-companies-page',
  templateUrl: './companies-page.component.html',
  styleUrls: ['./companies-page.component.scss']
})
export class CompaniesPageComponent implements OnInit {

  companies: CompanyDto[];
  selected: CompanyDto;
  columnsToDisplay = [ 'name', 'address', 'adminName', 'numberOfSites' ];
  isNew = false;


  constructor(private companiesClient: CompaniesClient) {
    companiesClient.getCompanies().pipe(map(data => {
      this.companies = data;
    },
    error => {
      console.log(error);
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
          this.companies.push(this.selected);
          this.isNew = false;
          this.selected = null;
        },
        error => {
          console.log(error);
        });
    } else {
      this.companiesClient.updateCompany(this.selected.id, model)
        .subscribe(_ => {
          console.log('Company saved');
        },
        error => {
          console.log(error);
        });
    }
  }

  onNew(): void {
    this.isNew = true;
    this.selected = new CompanyDto();
  }
}
