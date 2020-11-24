import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
  public messageSubject: Subject<string> = new Subject();
  public message$ = this.messageSubject.asObservable();

  constructor() {
  }

  showSnackbar(message: string): void {
    this.messageSubject.next(message);
  }
}
