import { Component } from '@angular/core';
import { FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { UserService } from '../services/user.service';
import { User } from '../model/user';
import { Route, Router } from '@angular/router';

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
          this.user = response;
          this.errorMessage = '';
          sessionStorage.setItem('user', JSON.stringify(this.user));
          this.router.navigate(['/home']);
        },
        error: (e) => {
          if (e.error) {
            this.errorMessage = e.error;
          }
        }
    });
  } 

  public goToRegister() {
    this.router.navigate(["register"]);
  }
}
