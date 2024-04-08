import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../model/user'; // Import the 'Usuario' type from the appropriate file
import { Cartao } from '../model/cartao';



@Injectable({
  providedIn: 'root'
})
export class UserService {

  private url = 'https://localhost:7003/api/Users'

  constructor(private httpClient: HttpClient) { }

  public authenticate(email: string, password: string) : Observable<User> {
    return this.httpClient.post<User>(`${this.url}/Login`, 
    { email: email, 
      senha: password 
    });
  }

  public register(nome: string, email: string, planoId: string, senha: string, dataNascimento: Date, cartao: Cartao) : Observable<User> {
    return this.httpClient.post<User>(this.url,
    { 
      nome: nome,
      email: email,
      planoId: planoId,
      senha: senha,
      dataNascimento: dataNascimento,
      cartao: cartao});
  }
}
