import { Routes } from '@angular/router';
import { ArtistComponent } from './pages/artist/artist.component';

export const routes: Routes = [
    {
        path: 'artist/:id',
        component: ArtistComponent
    }
];
