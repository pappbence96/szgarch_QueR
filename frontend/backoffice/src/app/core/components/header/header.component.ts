import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/utilities/AuthService';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn = false;
  username: string;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.authService.currentLogin.subscribe(() => {
      console.log('Auth state change detected in header');
      if (this.authService.isLoggedIn) {
        this.isLoggedIn = true;
        this.username = this.authService.userName;
      } else {
        this.isLoggedIn = false;
        this.username = '';
      }
    });
  }

  logOut(): void {
    this.authService.logout();
    this.router.navigate( [ '' ]);
  }
}
