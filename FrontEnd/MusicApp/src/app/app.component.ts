import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatGridListModule } from '@angular/material/grid-list';
import { Artist } from './model/artist';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatToolbarModule, MatButtonModule, MatIconModule, MatGridListModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'MusicApp';

  constructor(private router: Router) { }

  public goToHome(){
    if(!this.blockAccess()){
    this.router.navigate(['/home']);
    }
  } 

  public blockAccess(): boolean {
    return sessionStorage.getItem('user') === null;
  }


  public goToDetails(item:Artist) {
    this.router.navigate(["detail", item.id]);
  }

  public goToLogin() {
    this.router.navigate([""]);
  }
}
