import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { HubConnection, HubConnectionBuilder, IHttpConnectionOptions } from '@aspnet/signalr';
import { ErrorDetails, IdentityClient, TicketsClient, UserTicketDto } from 'src/app/clients';
import { PopupDialogComponent } from 'src/app/components/popup-dialog/popup-dialog.component';
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
    private authService: AuthService,
    private dialog: MatDialog
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

    this.hubConnection.on('calledTicket', (queueId: number, ticketId: number, ticketNumber: number, handler: string) => {
      console.log('Ticket called from: ' + queueId + ', ticket id: ' + ticketId + ', ticket number: ' + ticketNumber);

      let calledTicket = null;
      for (const ticket of this.tickets) {
        if (ticket.queueId === queueId) {
          if (ticket.id === ticketId) {
            calledTicket = ticket;
          } else if (ticket.number > ticketNumber) {
            ticket.numOfTicketsBeforeThis--;
          }
        }
      }
      if (calledTicket !== null) {
        this.tickets.splice(this.tickets.indexOf(calledTicket), 1);
        const dialogRef = this.dialog.open(PopupDialogComponent);
        dialogRef.componentInstance.message = 'Your ticket ' + calledTicket.visibleNumber + ' was called at ' + calledTicket.worksite + ' by ' + handler + '.';
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
