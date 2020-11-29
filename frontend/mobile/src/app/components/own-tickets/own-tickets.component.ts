import { Component, Input, OnInit } from '@angular/core';
import { UserTicketDto } from 'src/app/clients';

@Component({
  selector: 'app-own-tickets',
  templateUrl: './own-tickets.component.html',
  styleUrls: ['./own-tickets.component.scss']
})
export class OwnTicketsComponent implements OnInit {
  @Input() tickets: UserTicketDto[];

  constructor() {
  }

  ngOnInit(): void {
  }

}
