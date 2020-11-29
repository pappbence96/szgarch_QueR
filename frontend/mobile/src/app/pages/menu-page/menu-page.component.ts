import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HubConnection, HubConnectionBuilder, IHttpConnectionOptions } from '@aspnet/signalr';
import { ErrorDetails, IdentityClient, TicketsClient, UserTicketDto } from 'src/app/clients';
import { AuthService } from 'src/app/utilities/AuthService';
import { SnackbarService } from 'src/app/utilities/Snackbar.service';

@Component({
  selector: 'app-menu-page',
  templateUrl: './menu-page.component.html',
  styleUrls: ['./menu-page.component.scss']
})
export class MenuPageComponent implements OnInit, OnDestroy {
  tickets: UserTicketDto[] = [];
  hubConnection: HubConnection = null;

  constructor(
    identityClient: IdentityClient,
    private snackbar: SnackbarService,
    private router: Router,
    private authService: AuthService
  ) {
    identityClient.getOwnTicketsForUser()
      .subscribe(data => {
        console.log(data);
        this.tickets = data;
      }, (e: ErrorDetails) => {
        console.log(e);
        snackbar.showSnackbar(e.message);
      });
    }

  ngOnInit(): void {
    const options: IHttpConnectionOptions = {
    accessTokenFactory: () => {
        return this.authService.token;
      }
    };
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:5001/hubs/ticket', options)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));

    this.hubConnection.on('calledTicket', (queueId: number, ticketId: number, ticketNumber: number) => {
      console.log('Ticket called from: ' + queueId + ', ticket id: ' + ticketId + ', ticket number: ' + ticketNumber);

      let ticketIndex = -1;
      for (const ticket of this.tickets) {
        if (ticket.queueId === queueId) {
          if (ticket.id === ticketId) {
            this.snackbar.showSnackbar('Your ticket ' + ticket.visibleNumber + ' was called!');
            ticketIndex = this.tickets.indexOf(ticket);
          } else if (ticket.number > ticketNumber) {
            ticket.numOfTicketsBeforeThis--;
          }
        }
      }
      if (ticketIndex !== -1) {
        this.tickets.splice(ticketIndex, 1);
      }
    });
  }

  ngOnDestroy(): void {
    this.hubConnection.stop();
  }

  onAdd(): void {
    this.router.navigate([ '/new' ]);
  }
}
