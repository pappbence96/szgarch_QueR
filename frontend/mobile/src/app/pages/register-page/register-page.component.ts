import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IdentityClient, RegisterModel } from 'src/app/clients';
import { AuthService } from 'src/app/utilities/AuthService';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.scss']
})
export class RegisterPageComponent implements OnInit {
  registerForm: FormGroup;
  loading = false;
  submitted = false;
  error: string;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private client: IdentityClient) {
  }

  ngOnInit(): void {
    if (this.authService.isLoggedIn) {
      this.router.navigate( [''] );
    }

    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      password2: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.registerForm.invalid) {
      return;
    }
    if (this.registerForm.value.password !== this.registerForm.value.password2){
      this.error = 'Passwords must match';
      return;
    }

    this.loading = true;

    const model = new RegisterModel({
      userName: this.registerForm.value.username,
      email: this.registerForm.value.email,
      password: this.registerForm.value.password
    });
    this.client.register(model)
      .subscribe(
        data => {
          this.loading = false;
          console.log(data);
          this.router.navigate( ['login'] );
        },
        error => {
          this.loading = false;
          console.log(error);
          this.error = error.message;
        }
      );
  }
}
