import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../model/user'; // Import the 'Usuario' type from the appropriate file
import { Cartao } from '../model/cartao';
import { Song } from '../model/song';
import { Playlist } from '../model/playlist';
import { Subscription } from '../model/subscription';



@Injectable({
  providedIn: 'root'
})
export class UserService {

  private authUrl = 'https://localhost:7084/connect/token'
  private url = 'https://localhost:7003/api/Users'

  constructor(private httpClient: HttpClient) { }

  public authenticate(email: string, password: string) : Observable<any> {

    let body = new URLSearchParams();
    body.set('username', email);
    body.set('password', password);
    body.set('client_id', 'client-angular-spotify');
    body.set('client_secret', 'SpotifyLikeSecret');
    body.set('grant_type', 'password');
    body.set('scope', 'SpotifyLikeScope') 

    let options = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    }

    return this.httpClient.post(`${this.authUrl}`, body.toString(), options);
  }

  public register(nome: string, email: string, planoId: string, senha: string, dataNascimento: Date, cartao: Cartao) : Observable<User> {
    return this.httpClient.post<User>(this.url,
    { 
      nome: nome,
      email: email,
      planoId: planoId,
      senha: senha,
      dataNascimento: dataNascimento,
      cartao: cartao}, this.setAuthenticationHeader());
  }

  public getFavorites(userId: string) : Observable<Song[]> {
    return this.httpClient.get<Song[]>(`${this.url}/${userId}/Favoritas`, this.setAuthenticationHeader());
  }

  public getPlaylists(userId: string) : Observable<Playlist[]> {
    return this.httpClient.get<Playlist[]>(`${this.url}/${userId}/Playlists`, this.setAuthenticationHeader());
  }

  public getActiveSubscription(userId: string) : Observable<Subscription> {
    return this.httpClient.get<Subscription>(`${this.url}/${userId}/AssinaturaAtiva`, this.setAuthenticationHeader());
  }

  public favoriteSong(userId: string, songId: string) : Observable<User> {
    return this.httpClient.post<User>(`${this.url}/Favoritar`, 
    {
      idUser: userId,
      idSong: songId
    }, this.setAuthenticationHeader());
  }

  public unfavoriteSong(userId: string, songId: string) : Observable<User> {
    return this.httpClient.post<User>(`${this.url}/Desfavoritar`, 
    {
      idUser: userId,
      idSong: songId
    }, this.setAuthenticationHeader());
  }

  private setAuthenticationHeader() {
    let token = sessionStorage.getItem('access_token');
    return {
      headers: new HttpHeaders().set("Authorization", `Bearer ${token?.toString()}`)
    }
  }
}
