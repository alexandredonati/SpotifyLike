import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Artist } from '../model/artist';
import { ArtistService } from '../services/artist.service';
import { Album } from '../model/album';
import { MatExpansionModule } from '@angular/material/expansion';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-detail-artist',
  standalone: true,
  imports: [CommonModule, MatExpansionModule,  MatButtonModule],
  templateUrl: './detail-artist.component.html',
  styleUrl: './detail-artist.component.css'
})
export class DetailArtistComponent implements OnInit{

    panelOpenState = false;

    idArtist = '';
    artist!:Artist;
    albums!:Album[];

    constructor(private route: ActivatedRoute, private artistService: ArtistService, private router: Router){    }

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
      return sessionStorage.getItem('user') === null;
    };
  
    public goToLogin() {
      this.router.navigate([""]);
    };
}
