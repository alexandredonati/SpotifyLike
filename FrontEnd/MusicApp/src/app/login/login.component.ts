import { Component } from '@angular/core';
import { FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { UserService } from '../services/user.service';
import { User } from '../model/user';
import { Route, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButtonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  email = new FormControl('', [Validators.required, Validators.email]);
  senha = new FormControl('', [Validators.required]);

  errorMessage = '';

  user!: User;
  token!: any;

  constructor(private userService: UserService, private router: Router) { }

  public login() {
    if (this.email.invalid || this.senha.invalid) {
      this.errorMessage = 'Email e senha são obrigatórios';
      return;
    }

    let emailValue = this.email.getRawValue() as string;
    let senhaValue = this.senha.getRawValue() as string;

    this.userService.authenticate(emailValue, senhaValue).subscribe(
      {
        next: (response) => {
          this.token = jwtDecode(response.access_token);
          sessionStorage.setItem('user_session', JSON.stringify(this.token));
          sessionStorage.setItem('access_token', response.access_token);
          this.router.navigate(['/home']);
        },
        error: (e) => {
          if (e.error) {
            this.errorMessage = 'Erro: ' + e.error.error_description;
          }
        }
    });
  } 

  public goToRegister() {
    this.router.navigate(["register"]);
  }
}
