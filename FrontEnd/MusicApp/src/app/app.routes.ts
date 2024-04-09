import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DetailArtistComponent } from './detail-artist/detail-artist.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ExploreComponent } from './explore/explore.component';
import { ExploreSongsComponent } from './explore-songs/explore-songs.component';

export const routes: Routes = [
    {
        // define this as the landing page
        path: '',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'explore',
        component: ExploreComponent
    },
    {
        path: 'explore-songs',
        component: ExploreSongsComponent
    },
    {
        path: 'detail/:id',
        component: DetailArtistComponent
    }
];
