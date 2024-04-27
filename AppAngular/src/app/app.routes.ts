import { Routes } from '@angular/router';
import { SearchFormComponent } from './search-form/search-form.component';
import { SearchResultsComponent } from './search-results/search-results.component';

export const routes: Routes = [
  { path: 'search', component: SearchFormComponent },
  { path: 'results', component: SearchResultsComponent },
  { path: '**', redirectTo: '/search' } // Redirige a /search para cualquier otra ruta no encontrada
];
