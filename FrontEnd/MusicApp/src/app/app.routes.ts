import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DetailArtistComponent } from './detail-artist/detail-artist.component';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
    {
        // define this as the landing page
        path: '',
        component: LoginComponent
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'detail/:id',
        component: DetailArtistComponent
    }
];
