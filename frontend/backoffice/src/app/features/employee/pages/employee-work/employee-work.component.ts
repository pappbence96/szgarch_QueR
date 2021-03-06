import { Component, OnDestroy, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder, IHttpConnectionOptions } from '@aspnet/signalr';
import { queue } from 'rxjs/internal/scheduler/queue';
import { CompanyTicketDto, ErrorDetails, QueueDto, QueuesClient } from 'src/app/shared/clients';
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
    private queuesClient: QueuesClient,
    private snackbar: SnackbarService,
    private authService: AuthService
    ) {
    queuesClient.getActiveTicketsForOwnQueue()
      .subscribe(data => {
        this.tickets = data;
        console.log(data);
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
    this.queuesClient.callNextTicket()
      .subscribe(data => {
        this.snackbar.showSnackbar('Ticket ' + this.nextTicketNumber() + ' called.');
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
    });
  }

  call(ticket: CompanyTicketDto): void {
    this.queuesClient.callTicketByNumber(ticket.number)
      .subscribe(data => {
        this.snackbar.showSnackbar('Ticket ' + ticket.visibleNumber + ' called.');
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
    return this.tickets[0].visibleNumber;
  }

  ticketsButFirst(): CompanyTicketDto[] {
    if (this.tickets === undefined) {
      return [];
    }
    return this.tickets.slice(1, this.tickets.length);
  }
}
