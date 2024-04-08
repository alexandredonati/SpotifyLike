import { HttpClient } from '@angular/common/http';
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
    return this.httpClient.get<Plano[]>(this.url);
}
}
