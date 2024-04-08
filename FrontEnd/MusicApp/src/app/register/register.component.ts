import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatCalendarCellClassFunction, MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { UserService } from '../services/user.service';
import { Cartao } from '../model/cartao';
import { PlanoService } from '../services/plano.service';
import { Plano } from '../model/plano';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatInputModule, MatSelectModule, 
    FormsModule, ReactiveFormsModule, MatButtonModule, MatDatepickerModule,MatExpansionModule, MatDialogModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  providers: [provideNativeDateAdapter()]
})

export class RegisterComponent implements OnInit{

  dateClass: MatCalendarCellClassFunction<Date> = (cellDate, view) => {
    // // Only highligh dates inside the month view.
    // if (view === 'month') {
    //   const date = cellDate.getDate();

    //   // Highlight the 1st and 20th day of each month.
    //   return date === 1 || date === 20 ? 'example-custom-date-class' : '';
    // }
    return '';
  };

  panelOpenState = false;

  nome = new FormControl('', [Validators.required]);
  email = new FormControl('', [Validators.required]);
  senha = new FormControl('', [Validators.required]);
  cartaoNumero = new FormControl('', [Validators.required]);
  cartaoLimite = new FormControl('', [Validators.required]);
  cartaoVencimento = new Date();

  dataNascimento = new Date();

  planos!: Plano[];
  selectedPlanoId!: string;

  errorMessage = '';

  constructor(private userService: UserService, private planoSevice: PlanoService, private router: Router, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.planoSevice.getPlanos().subscribe(
      {
        next: (response) => {
          this.planos = response;
        },
        error: (e) => {
          this.errorMessage = e.error;
        }
      }
    );
  };

  public newUser() {
    if(this.nome.invalid || this.email.invalid || this.senha.invalid) {
      this.errorMessage = 'Todos os campos são obrigatórios';
      return;
    }

    let nomeValue = this.nome.getRawValue() as string;
    let emailValue = this.email.getRawValue() as string;
    let senhaValue = this.senha.getRawValue() as string;
    let planoId = this.selectedPlanoId;

    let cartaoValue = new Object as Cartao;
    cartaoValue.numero = this.cartaoNumero.getRawValue() as string;
    cartaoValue.limite = this.cartaoLimite.getRawValue() as string;
    cartaoValue.dataVencimento = this.cartaoVencimento;
    cartaoValue.ativo = true;
    cartaoValue.id = '00000000-0000-0000-0000-000000000000';

    this.userService.register(nomeValue, emailValue, planoId, senhaValue, this.dataNascimento, cartaoValue).subscribe(
      {
        next: (response) => {
          this.errorMessage = '';
          this.openDialog();},
        error: (e) => {
          if(e.error) {
            this.errorMessage = e.error;
          }
        }
      }
    );
  }

  public getPlanos() {
    this.planoSevice.getPlanos().subscribe(
      {
        next: (response) => {
          console.log(response);
        },
        error: (e) => {
          this.errorMessage = e.error;
        }
      });
  }

  openDialog() {
    this.dialog.open(DialogSuccess);
  }
}

@Component({
  selector: 'app-register-success-dialog',
  templateUrl: './register-success-dialog.html',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
})
export class DialogSuccess {

  constructor(private router: Router) { }

  public goToLogin() {
    this.router.navigate([""]);
  }
}