import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment'; // Importar el archivo de entorno para acceder a las variables de configuración
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FlightSearchService {

  constructor(private http: HttpClient) { }  
  // Método para buscar vuelos preguntando a la api
  // Params: objeto que contiene los parámetros de búsqueda (origin, destination, tripType, currency)
  // Retorna: un observable que emite la respuesta de la solicitud HTTP
  searchFlights(params: any): Observable<any> {
    const apiUrl = `${environment.apiUrl}/${environment.get_routes}?origin=${params.origin}&destination=${params.destination}&tripType=${params.tripType}&currency=${params.currency}`;
    return this.http.get(apiUrl);
  }
}
