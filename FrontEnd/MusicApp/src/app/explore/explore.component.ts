import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { Artist } from '../model/artist';
import { Router } from '@angular/router';
import { ArtistService } from '../services/artist.service';
import { User } from '../model/user';

@Component({
  selector: 'app-explore',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButtonModule, MatCardModule, FlexLayoutModule],
  templateUrl: './explore.component.html',
  styleUrl: './explore.component.css'
})
export class ExploreComponent implements OnInit{

    public errorMessage = '';  

    public artists = null;

    public searchArtistsText = new FormControl('');

    constructor(private artistService: ArtistService, private router: Router){}

    ngOnInit(): void {
      this.getAllArtists();
    }

    public blockAccess(): boolean {
      return sessionStorage.getItem('user_session') === null;
    }

    public goToDetails(item:Artist) {
      this.router.navigate(["detail", item.id]);
    }

    public goToLogin() {
      this.router.navigate([""]);
    }

    public getAllArtists() {
      this.artistService.getAllArtists().subscribe(response => {
        this.artists = response as any;
        this.searchArtistsText.setValue('');
      })
    }

    public searchArtists() {
      let searchArtistsTextValue = this.searchArtistsText.getRawValue() as string;

      this.artistService.searchArtists(searchArtistsTextValue).subscribe(
        {
          next: (response) => {
            this.artists = response as any;
          },
          error: (e) => {

            console.log(e);
          }
        }
      )
    }
}