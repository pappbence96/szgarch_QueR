import { Component, OnInit } from '@angular/core';
import { ErrorDetails, TicketsClient, UserTicketDto } from 'src/app/clients';
import { SnackbarService } from 'src/app/utilities/Snackbar.service';

@Component({
  selector: 'app-own-tickets',
  templateUrl: './own-tickets.component.html',
  styleUrls: ['./own-tickets.component.scss']
})
export class OwnTicketsComponent implements OnInit {
  tickets: UserTicketDto[];

  constructor(
    private ticketsClient: TicketsClient,
    private snackbar: SnackbarService
    ) {
    ticketsClient.getOwnTicketsForUser()
      .subscribe(data => {
        console.log(data);
        this.tickets = data;
      }, (e: ErrorDetails) => {
        console.log(e);
        snackbar.showSnackbar(e.message);
      });
  }

  ngOnInit(): void {
  }

}
