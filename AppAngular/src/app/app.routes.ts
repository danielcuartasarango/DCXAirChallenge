import { Routes } from '@angular/router';
import { SearchFormComponent } from './components/search-form/search-form.component';
import { SearchResultsComponent } from './components/search-results/search-results.component';

export const routes: Routes = [
  { path: 'search', component: SearchFormComponent },
  { path: 'results', component: SearchResultsComponent },
  { path: '**', redirectTo: '/search' } // Redirige a /search para cualquier otra ruta no encontrada
];
