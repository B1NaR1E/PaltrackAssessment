import { Component } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service'
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})

export class LoginComponent {
  strongPasswordRegx: RegExp = /^(?=[^A-Z]*[A-Z])(?=[^a-z]*[a-z])(?=\D*\d).{6,}$/;

  form: FormGroup = this.formBuilder.group({
    username: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.pattern(this.strongPasswordRegx)]),
  });
  constructor(
    private authService: AuthenticationService,
    private formBuilder: FormBuilder,
    private router: Router) {
  }
  login(){
    this.authService.login(this.form.value.username, this.form.value.password)
      .subscribe({
        next: data => {
          localStorage.setItem('token', data.accessToken);
          this.router.navigate(['/home']);
        },
        error: err => {
          alert("An error has occurred while trying to login.");
        }
      });
  }

  get username(){
    return this.form.get('username');
  }
  get password(){
    return this.form.get('password');
  }
}
