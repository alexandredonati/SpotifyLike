import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { SongService } from '../services/song.service';
import { Song } from '../model/song';
import { UserService } from '../services/user.service';
import { User } from '../model/user';

@Component({
  selector: 'app-explore-songs',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButtonModule, MatCardModule, MatIconModule],
  templateUrl: './explore-songs.component.html',
  styleUrl: './explore-songs.component.css'
})
export class ExploreSongsComponent implements OnInit {
  
  user = JSON.parse(sessionStorage.getItem('user') as string) as User;

  public errorMessage = '';  

  public songs:Song[] = [];

  public searchSongsText = new FormControl('');

  constructor(private songService: SongService, private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.getAllSongs();
  }

  public blockAccess(): boolean {
    return sessionStorage.getItem('user') === null;
  }

  public goToLogin() {
    this.router.navigate([""]);
  }

  public getAllSongs() {
    this.songService.getAllSongs().subscribe(response => {
      this.songs = response as any;
      this.searchSongsText.setValue('');
    })
  }

  public searchSongs() {
    let searchSongsTextValue = this.searchSongsText.getRawValue() as string;

    this.songService.searchSongs(searchSongsTextValue).subscribe(
      {
        next: (response) => {
          this.songs = response as any;
        },
        error: (e) => {

          console.log(e);
        }
      }
    )
  }

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

  public unfavoriteSong(idUser: string, idSong: string) {
    this.userService.unfavoriteSong(idUser, idSong).subscribe(
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
