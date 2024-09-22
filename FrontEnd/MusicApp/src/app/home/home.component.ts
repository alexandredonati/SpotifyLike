import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule, NgFor } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Artist } from '../model/artist';
import { ArtistService } from '../services/artist.service';
import { User } from '../model/user';
import { UserService } from '../services/user.service';
import { Song } from '../model/song';
import { Playlist } from '../model/playlist';
import { Plano } from '../model/plano';
import { PlanoService } from '../services/plano.service';
import { UserSession } from '../model/userSession';
import { Subscription } from '../model/subscription';
import { firstValueFrom } from 'rxjs'
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatButtonModule, MatExpansionModule, MatIconModule, CommonModule, NgFor, MatCardModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

    public artists = null;
    userSession = JSON.parse(sessionStorage.getItem('user_session') as string) as UserSession;

    favoritas: Song[] = [];
    

    playlists: Playlist[] = [];

    subscription!: Subscription;
    planos:Plano[] = [];
    planoSelecionado!: Plano;

    //dataNascimento = new Date(this.user.dataNascimento);
    //strDataNascimento = this.dataNascimento.getDate() + '/' + (this.dataNascimento.getMonth() + 1) + '/' + this.dataNascimento.getFullYear();

    constructor(private artistService: ArtistService, private userService: UserService, private planoSevice: PlanoService, private router: Router){}

    ngOnInit(): void {

      this.getFavoritas();
      this.getPlaylists();
      //this.getActiveSubscription()
      this.getPlanoSelecionado();
    };

    public blockAccess(): boolean {
      return sessionStorage.getItem('user_session') === null;
    };


    public goToDetails(item:Artist) {
      this.router.navigate(["detail", item.id]);
    };

    public goToLogin() {
      this.router.navigate([""]);
    };

    public async getFavoritas() {
      await this.userService.getFavorites(this.userSession?.sub as string).subscribe(
        {
          next: (response) => {
            this.favoritas = response;
          },
          error: (e) => {
            console.log(e);
          }
        }
      );
    };

    public async getPlaylists() {
      await this.userService.getPlaylists(this.userSession?.sub as string).subscribe(
        {
          next: (response) => {
            this.playlists = response;
          },
          error: (e) => {
            console.log(e);
          }
        }
      );
    };

    public getPlanoSelecionado() {

      this.userService.getActiveSubscription(this.userSession?.sub as string).subscribe(
        {
          next: (response) => {
            this.subscription = response;
          },
          error: (e) => {
            console.log(e);
          },
          complete: () => {
            this.planoSevice.getPlanoById(this.subscription.planoId).subscribe(
              {
                next: (response) => {
                  this.planoSelecionado = response;
                },
                error: (e) => {
                  console.log(e);
                }
              }
            );
          }
        }
      );
    };

    public async unfavoriteSong(idUser: string, idSong: string) {
      await this.userService.unfavoriteSong(idUser, idSong).subscribe(
        {
          next: (response) => {
            console.log(response);
          },
          error: (e) => {
            console.log(e);
          }
        }
      );
      
      await this.delay(500);
      await this.getFavoritas();
    };
    
    public delay(ms: number) {
      return new Promise(resolve => setTimeout(resolve, ms));
    }
    
}
