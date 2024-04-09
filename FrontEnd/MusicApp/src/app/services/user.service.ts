import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../model/user'; // Import the 'Usuario' type from the appropriate file
import { Cartao } from '../model/cartao';
import { Song } from '../model/song';
import { Playlist } from '../model/playlist';



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

  public getFavorites(userId: string) : Observable<Song[]> {
    return this.httpClient.get<Song[]>(`${this.url}/${userId}/Favoritas`);
  }

  public getPlaylists(userId: string) : Observable<Playlist[]> {
    return this.httpClient.get<Playlist[]>(`${this.url}/${userId}/Playlists`);
  }

  public favoriteSong(userId: string, songId: string) : Observable<User> {
    return this.httpClient.post<User>(`${this.url}/Favoritar`, 
    {
      idUser: userId,
      idSong: songId
    });
  }

  public unfavoriteSong(userId: string, songId: string) : Observable<User> {
    return this.httpClient.post<User>(`${this.url}/Desfavoritar`, 
    {
      idUser: userId,
      idSong: songId
    });
  }
}
