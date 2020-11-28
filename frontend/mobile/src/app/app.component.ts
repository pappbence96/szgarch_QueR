import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarService } from './utilities/Snackbar.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'mobile';

  constructor(private snackbarService: SnackbarService, private snackbarRef: MatSnackBar) {
    snackbarService.message$.subscribe(
      (message: string) => {
        this.snackbarRef.open(message, 'Dismiss', {
          duration: 1500
        });
      });
  }
}
