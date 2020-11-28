import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthService } from 'src/app/utilities/AuthService';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  error: string;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    if (this.authService.isLoggedIn) {
      this.router.navigate( [''] );
    }

    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;

    this.authService
      .login(this.loginForm.value.username, this.loginForm.value.password)
      .subscribe(
        data => {
          this.loading = false;
          console.log(data);
          if (this.authService.isInRole('user')) {
            this.router.navigate( [''] );
          } else {
            this.error = 'You are not a regular user. Are you looking for the Backoffice application?';
            this.authService.logout();
            return;
          }
        },
        error => {
          this.loading = false;
          console.log(error);
          this.error = error.message;
        }
      );
  }
}
