import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ErrorDetails, TicketModel, TicketsClient, UserCompanyDto, UserQueueDto, UserSiteDto, UserTicketDto } from 'src/app/clients';
import { SnackbarService } from 'src/app/utilities/Snackbar.service';

@Component({
  selector: 'app-request-ticket',
  templateUrl: './request-ticket.component.html',
  styleUrls: ['./request-ticket.component.scss']
})
export class RequestTicketComponent implements OnInit {

  @Input() selectedCompany: UserCompanyDto;
  @Input() selectedSite: UserSiteDto;
  @Input() selectedQueue: UserQueueDto;

  constructor(
    private ticketsClient: TicketsClient,
    private router: Router,
    private snackbar: SnackbarService) { }

  ngOnInit(): void {
  }

  onRequestTicket(): void {
    const model = new TicketModel({
      queueId: this.selectedQueue.id
    });
    this.ticketsClient.createTicket(model)
      .subscribe(
        (data: UserTicketDto) => {
          this.snackbar.showSnackbar('Your number is ' + data.visibleNumber);
          this.router.navigate(['']);
        },
        (e: ErrorDetails) => {
          this.snackbar.showSnackbar(e.message);
        }
      );
  }
}
