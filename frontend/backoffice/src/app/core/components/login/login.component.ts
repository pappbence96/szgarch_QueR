import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthService } from 'src/app/shared/utilities/AuthService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error: string;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    if (this.authService.isLoggedIn) {
      this.navigatePostLogin();
    }

    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
  }

  get f(): {[key: string]: AbstractControl} {
    return this.loginForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;

    this.authService
      .login(this.f.username.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        data => {
          this.loading = false;
          console.log(data);
          if (this.authService.isInRole('administrator') && !this.authService.administratedCompanyId) {
            this.error = 'You are currently not assigned as the administrator of any companies. Please contact the system operator.';
            this.authService.logout();
          } else if (this.authService.isInRole('manager') && !this.authService.managedSiteId) {
            this.error = 'You are currently not assigned as the manager of any sites. Please contact the administrator of your company.';
            this.authService.logout();
          } else if (this.authService.isInRole('employee') && !this.authService.worksiteId) {
            this.error = 'You are currently not assigned to any worksites. Please contact the administrator of your company.';
            this.authService.logout();
          } else if (this.authService.isInRole('employee') && !this.authService.assignedQueueId) {
            this.error = 'You are currently not assigned to any queues. Please contact the manager of your worksite.';
          } else {
            this.navigatePostLogin();
          }
        },
        error => {
          this.loading = false;
          console.log(error);
          this.error = error.message;
        }
      );
  }

  navigatePostLogin(): void {
    if (this.authService.isInRole('operator')){
      this.router.navigate( ['/operator'] );
    } else if (this.authService.isInRole('administrator')) {
      this.router.navigate( ['/administrator'] );
    } else if (this.authService.isInRole('manager')) {
      this.router.navigate( ['/manager'] );
    } else if (this.authService.isInRole('employee')) {
      this.router.navigate( ['/employee/'] );
    }
  }
}
