import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { ArtistService } from '../services/artist.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { Artist } from '../model/artist';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatCardModule, HttpClientModule, FlexLayoutModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

    public artists = null;

    constructor(private artistService: ArtistService, private router: Router){}

    ngOnInit(): void {
      this.artistService.getArtist().subscribe(response => {
        console.log(response);
        this.artists = response as any;
      })
    }

    public goToDetails(item:Artist) {
      this.router.navigate(["detail", item.id]);
    }
}
