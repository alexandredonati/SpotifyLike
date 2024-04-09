import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Artist } from './model/artist';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatToolbarModule, MatButtonModule, MatIconModule, MatGridListModule, MatSidenavModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'MusicApp';

  constructor(private router: Router) { }

  public goToHome(){
    if(this.checkUserAuthenticated()){
    this.router.navigate(['/home']);
    }
  }

  public goToExplore(){
    if(this.checkUserAuthenticated()){
    this.router.navigate(['/explore']);
    }
  }

  public goToExploreSongs(){
    if(this.checkUserAuthenticated()){
    this.router.navigate(['/explore-songs']);
    }
  }

  public checkUserAuthenticated(): boolean {
    return !(sessionStorage.getItem('user') === null);
  }

  public goToLogin() {
    this.router.navigate([""]);
  }
}
