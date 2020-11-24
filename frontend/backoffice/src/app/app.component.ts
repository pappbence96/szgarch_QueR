import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarService } from './shared/utilities/Snackbar.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'backoffice';

  constructor(private snackbarService: SnackbarService, private snackbarRef: MatSnackBar) {
    snackbarService.message$.subscribe(
      (message: string) => {
        this.snackbarRef.open(message, 'Dismiss', {
          duration: 1500
        });
      });
  }
}
