import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Plano } from '../model/plano';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlanoService {

  private url = 'https://localhost:7003/api/Planos'

  constructor(private httpClient: HttpClient) { }

  public getPlanos(): Observable<Plano[]> {
    return this.httpClient.get<Plano[]>(this.url, this.setAuthenticationHeader());
  }

  public getPlanoById(id: string): Observable<Plano> {
    return this.httpClient.get<Plano>(`${this.url}/${id}`, this.setAuthenticationHeader());
  }

  private setAuthenticationHeader() {
    let token = sessionStorage.getItem('access_token');
    return {
      headers: new HttpHeaders().set("Authorization", `Bearer ${token}`)
    }
  }
}
