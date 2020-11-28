import { Component, OnDestroy, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder, IHttpConnectionOptions } from '@aspnet/signalr';
import { queue } from 'rxjs/internal/scheduler/queue';
import { CompanyTicketDto, ErrorDetails, QueueDto, QueuesClient, TicketsClient } from 'src/app/shared/clients';
import { AuthService } from 'src/app/shared/utilities/AuthService';
import { SnackbarService } from 'src/app/shared/utilities/Snackbar.service';

@Component({
  selector: 'app-employee-work',
  templateUrl: './employee-work.component.html',
  styleUrls: ['./employee-work.component.scss']
})
export class EmployeeWorkComponent implements OnInit, OnDestroy {
  tickets: CompanyTicketDto[];
  queueData: QueueDto;

  private hubConnection: HubConnection;

  constructor(
    private ticketsClient: TicketsClient,
    queuesClient: QueuesClient,
    private snackbar: SnackbarService,
    private authService: AuthService
    ) {
    ticketsClient.getActiveTicketsForOwnQueue()
      .subscribe(data => {
        this.tickets = data;
      },
      (error: ErrorDetails) => {
        snackbar.showSnackbar(error.message);
    });
    queuesClient.getDetailsOfAssignedQueue()
      .subscribe(data => {
        this.queueData = data;
      },
      (error: ErrorDetails) => {
        snackbar.showSnackbar(error.message);
    });
  }

  ngOnInit(): void {
    const options: IHttpConnectionOptions = {
      accessTokenFactory: () => {
        return this.authService.token;
      }
    };
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:5001/hubs/queue', options)
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));

    this.hubConnection.on('newTicket', (queueId: number, data: CompanyTicketDto) => {
      console.log(queueId);
      console.log(data);
      if (queueId !== this.queueData.id) {
        return;
      }
      this.tickets.push(data);
    });
    this.hubConnection.on('calledTicket', (queueId: number, ticketId: number) => {
      console.log(queueId);
      console.log(ticketId);
      if (queueId !== this.queueData.id) {
        return;
      }
      const removed = this.tickets.filter(t => t.id === ticketId);
      if (removed.length === 1) {
        const indexOfTicket = this.tickets.indexOf(removed[0]);
        this.tickets.splice(indexOfTicket, 1);
      }
    });
  }

  ngOnDestroy(): void {
    this.hubConnection.stop();
  }

  callNext(): void {
    this.ticketsClient.callNextTicket()
      .subscribe(data => {
        this.snackbar.showSnackbar('Ticket ' + this.nextTicketNumber() + ' called.');
        //this.tickets = this.tickets.slice(1, this.tickets.length);
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
    });
  }

  call(ticket: CompanyTicketDto): void {
    this.ticketsClient.callTicketByNumber(ticket.number)
      .subscribe(data => {
        this.snackbar.showSnackbar('Ticket ' + ticket.formattedNumber + ' called.');
        //const indexOfTicket = this.tickets.indexOf(ticket);
        //this.tickets.splice(indexOfTicket, 1);
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
    });
  }

  title(): string {
    if (this.queueData === undefined) {
      return 'Loading...';
    }
    return this.queueData.queueType + ' queue at ' + this.queueData.worksite;
  }

  nextTicketNumber(): string {
    if (this.tickets === undefined || this.tickets.length === 0) {
      return '-';
    }
    return this.tickets[0].formattedNumber;
  }

  ticketsButFirst(): CompanyTicketDto[] {
    if (this.tickets === undefined) {
      return [];
    }
    return this.tickets.slice(1, this.tickets.length);
  }
}
