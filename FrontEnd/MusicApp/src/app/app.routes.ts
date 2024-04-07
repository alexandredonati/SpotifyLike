import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DetailArtistComponent } from './detail-artist/detail-artist.component';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    },
    {
        path: 'detail/:id',
        component: DetailArtistComponent
    }
];
