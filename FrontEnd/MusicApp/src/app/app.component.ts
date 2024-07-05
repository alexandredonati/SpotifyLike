import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Artist } from './model/artist';
import { Unsubscribable } from 'rxjs';
import { UserService } from './services/user.service';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatToolbarModule, MatButtonModule, MatIconModule, MatGridListModule, MatSidenavModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'MusicApp';
  userName = '';

  constructor(private router: Router, private userService: UserService) { }

  ngOnInit(): void {
    if (sessionStorage.getItem('user_session')) {
      let token = JSON.parse(sessionStorage.getItem('user_session') as string);
      this.userName = token.name;
    }
  }

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
    return !(sessionStorage.getItem('user_session') === null);
  }

  public goToLogin() {
    this.router.navigate([""]);
  }

  public logout() {
    sessionStorage.removeItem('user');
    this.router.navigate([""]);
  }
}
