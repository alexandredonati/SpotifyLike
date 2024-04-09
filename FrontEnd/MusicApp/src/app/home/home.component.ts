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

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatButtonModule, MatExpansionModule, MatIconModule, CommonModule, NgFor, MatCardModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

    public artists = null;
    user = JSON.parse(sessionStorage.getItem('user') as string) as User;

    favoritas: Song[] = [];

    playlists: Playlist[] = [];

    planos:Plano[] = [];
    planoSelecionado!: Plano;

    dataNascimento = new Date(this.user.dataNascimento);
    strDataNascimento = this.dataNascimento.getDate() + '/' + (this.dataNascimento.getMonth() + 1) + '/' + this.dataNascimento.getFullYear();

    constructor(private artistService: ArtistService, private userService: UserService, private planoSevice: PlanoService, private router: Router){}

    ngOnInit(): void {

      this.getFavoritas();
      this.getPlaylists();
      this.getPlanos();
    };

    public blockAccess(): boolean {
      return sessionStorage.getItem('user') === null;
    };


    public goToDetails(item:Artist) {
      this.router.navigate(["detail", item.id]);
    };

    public goToLogin() {
      this.router.navigate([""]);
    };

    public getFavoritas() {
      this.userService.getFavorites(this.user?.id as string).subscribe(
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

    public getPlaylists() {
      this.userService.getPlaylists(this.user?.id as string).subscribe(
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

    public getPlanos() {
      this.planoSevice.getPlanos().subscribe(
        {
          next: (response) => {
            this.planos = response;
            this.planoSelecionado = this.planos.find(p => p.id === this.user.planoId) as Plano;
          },
          error: (e) => {
            console.log(e.error);
          }
        });
    }

    public async getPlanoSelecionado() {
      await this.getPlanos();
      this.planoSelecionado = this.planos.find(p => p.id === this.user.planoId) as Plano;
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
