import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Artist } from '../model/artist'
import { Album } from '../model/album';

@Injectable({
  providedIn: 'root'
})
export class ArtistService {

  private url = 'https://localhost:7003/api/Artists'

  constructor(private httpClient: HttpClient) { }

  public getAllArtists(): Observable<Artist[]> {
      return this.httpClient.get<Artist[]>(this.url, this.setAuthenticationHeader());
  }

  public searchArtists(searchText: string): Observable<Artist[]> {
    return this.httpClient.get<Artist[]>(`${this.url}/Search/${searchText}`, this.setAuthenticationHeader());
}

  public getArtistById(id: string): Observable<Artist> {
    return this.httpClient.get<Artist>(`${this.url}/${id}`, this.setAuthenticationHeader())
  }

  public getArtistAlbuns(id: string): Observable<Album[]> {
    return this.httpClient.get<Album[]>(`${this.url}/${id}/Albums`, this.setAuthenticationHeader());
  }

  private setAuthenticationHeader() {
    let token = sessionStorage.getItem('access_token');
    return {
      headers: new HttpHeaders().set("Authorization", `Bearer ${token}`)
    }
  }
}
