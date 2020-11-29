import { Component, OnInit, ViewChild } from '@angular/core';
import { MatAccordion } from '@angular/material/expansion';
import { delay } from 'rxjs/operators';
import { CompaniesClient, ErrorDetails, SitesClient, UserCompanyDto, UserQueueDto, UserSiteDto } from 'src/app/clients';
import { SnackbarService } from 'src/app/utilities/Snackbar.service';

@Component({
  selector: 'app-new-ticket',
  templateUrl: './new-ticket.component.html',
  styleUrls: ['./new-ticket.component.scss']
})
export class NewTicketComponent implements OnInit {
  @ViewChild(MatAccordion) accordion: MatAccordion;

  companies: UserCompanyDto[];
  sites: UserSiteDto[];
  queues: UserQueueDto[];
  selectedCompany: UserCompanyDto;
  selectedSite: UserSiteDto;
  selectedQueue: UserQueueDto;
  loading = [ false, false, false ];
  stage = 0;

  constructor(
    private companiesClient: CompaniesClient,
    private sitesClient: SitesClient,
    private snackbar: SnackbarService
  ) {
    this.loading[0] = true;
    companiesClient.getCompaniesForUser()
      .subscribe(data => {
        this.companies = data;
        this.loading[0] = false;
      }, (e: ErrorDetails) => {
        snackbar.showSnackbar(e.message);
        this.loading[0] = false;
    });
  }


  ngOnInit(): void {
  }

  selectCompany(company: UserCompanyDto): void {
    this.selectedCompany = company;
    this.stage = 1;
    this.loading[1] = true;
    this.companiesClient.getSitesOfCompanyForUser(company.id)
      .subscribe(data => {
        this.sites = data;
        this.loading[1] = false;
      }, (e: ErrorDetails) => {
        this.snackbar.showSnackbar(e.message);
        this.loading[1] = false;
      });
  }

  selectSite(site: UserSiteDto): void {
    this.selectedSite = site;
    this.stage = 2;
    this.loading[2] = true;
    this.sitesClient.getQueuesOfSiteForUser(site.id)
      .subscribe(data => {
        this.queues = data;
        this.loading[2] = false;
      }, (e: ErrorDetails) => {
        this.snackbar.showSnackbar(e.message);
        this.loading[2] = false;
      });
  }

  selectQueue(queue: UserQueueDto): void {
    this.selectedQueue = queue;
    this.stage = 3;
    this.accordion.closeAll();
  }

  setStage(stage: number): void {
    if (stage < 3) {
      this.selectedQueue = null;
    }
    if (stage < 2) {
      this.selectedSite = null;
    }
    if (stage < 1) {
      this.selectedCompany = null;
    }
    this.stage = stage;
  }
}
