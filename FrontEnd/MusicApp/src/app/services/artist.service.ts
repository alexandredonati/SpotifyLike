import { HttpClient } from '@angular/common/http';
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

  public getArtist(): Observable<Artist[]> {
      return this.httpClient.get<Artist[]>(this.url);
  }

  public getArtistById(id: string): Observable<Artist> {
    return this.httpClient.get<Artist>(`${this.url}/${id}`)
  }

  public getArtistAlbuns(id: string): Observable<Album[]> {
    return this.httpClient.get<Album[]>(`${this.url}/${id}/Albums`);
  }
}
