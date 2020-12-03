import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
  public messageSubject: Subject<SnackbarMessage> = new Subject();
  public message$ = this.messageSubject.asObservable();

  constructor() {
  }

  showSnackbar(message: string, duration = 1500): void {
    this.messageSubject.next(new SnackbarMessage(message, duration));
  }
}

export class SnackbarMessage {
  constructor(public message: string, public duration = 1500){

  }
}
