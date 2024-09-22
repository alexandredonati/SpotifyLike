import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { CommonModule } from '@angular/common';
import { Artist } from '../model/artist';
import { ArtistService } from '../services/artist.service';
import { Album } from '../model/album';
import { UserService } from '../services/user.service';
import { User } from '../model/user';
import { UserSession } from '../model/userSession';

@Component({
  selector: 'app-detail-artist',
  standalone: true,
  imports: [CommonModule, MatExpansionModule,  MatButtonModule, MatCardModule, MatIconModule],
  templateUrl: './detail-artist.component.html',
  styleUrl: './detail-artist.component.css'
})
export class DetailArtistComponent implements OnInit{

    user = JSON.parse(sessionStorage.getItem('user_session') as string) as UserSession;
    
    panelOpenState = false;

    idArtist = '';
    artist!:Artist;
    albums!:Album[];

    constructor(private route: ActivatedRoute, private artistService: ArtistService, private userService: UserService, private router: Router){    }

    ngOnInit(): void {
      this.idArtist = this.route.snapshot.params["id"];

      this.artistService.getArtistById(this.idArtist).subscribe(response => {
        this.artist = response;
      });

      this.artistService.getArtistAlbuns(this.idArtist).subscribe(response => {
        this.albums = response
        console.log(this.albums)
      })
    };

    public blockAccess(): boolean {
      return sessionStorage.getItem('user_session') === null;
    };
  
    public goToLogin() {
      this.router.navigate([""]);
    };

    public favoriteSong(idUser: string, idSong: string) {
      this.userService.favoriteSong(idUser, idSong).subscribe(
        {
          next: (response) => {
            console.log(response);
          },
          error: (e) => {
            console.log(e);
          }
        })
    };
    
}
