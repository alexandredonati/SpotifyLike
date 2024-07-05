import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Song } from '../model/song';

@Injectable({
  providedIn: 'root'
})
export class SongService {

  private url = 'https://localhost:7003/api/Songs'

  constructor(private httpClient: HttpClient) { }

  public getAllSongs(): Observable<Song[]> {
      return this.httpClient.get<Song[]>(this.url, this.setAuthenticationHeader());
  }

  public searchSongs(searchText: string): Observable<Song[]> {
    return this.httpClient.get<Song[]>(`${this.url}/Search/${searchText}`, this.setAuthenticationHeader());
  }

  private setAuthenticationHeader() {
    let token = sessionStorage.getItem('access_token');
    return {
      headers: new HttpHeaders().set("Authorization", `Bearer ${token}`)
    }
  }
}
