import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root' // Se utiliza la inyección de dependencia a nivel de raíz
})
export class FlightSearchService {

  constructor(private http: HttpClient) { } // Se inyecta el servicio HttpClient

  searchFlights(params: any): Observable<any> {
    // Se construye la URL de la API utilizando la variable de entorno
    const apiUrl = `${environment.apiUrl}/${environment.get_routes}?origin=${params.origin}&destination=${params.destination}`;
    console.log(apiUrl)
    // Se realiza la solicitud HTTP POST con los parámetros proporcionados
    return this.http.post(apiUrl, params);
  }
}
