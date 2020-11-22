import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterEvent, RoutesRecognized } from '@angular/router';
import { AuthService } from 'src/app/shared/utilities/AuthService';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn = false;
  username = '';
  private title = '';

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.authService.currentLogin.subscribe(() => {
      if (this.authService.isLoggedIn) {
        this.isLoggedIn = true;
        this.username = this.authService.userName;
      } else {
        this.isLoggedIn = false;
        this.username = '';
      }
    });
    this.router.events.subscribe((data) => {
      if (data instanceof RoutesRecognized) {
        this.title = data.state.root.firstChild.data.title as string;
      }
    });
  }

  logOut(): void {
    this.authService.logout();
    this.router.navigate( [ '' ]);
  }

  public get siteTitle(): string {
    if (this.title) {
      return `- ${this.title} page`;
    }
    return '';
  }
}
