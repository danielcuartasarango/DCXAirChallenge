import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FlightSearchService {

  constructor(private http: HttpClient) { }

  searchFlights(params: any): Observable<any> {
    const apiUrl = `${environment.apiUrl}/${environment.get_routes}?origin=${params.origin}&destination=${params.destination}&tripType=${params.tripType}&currency=${params.currency}`;
    console.log(apiUrl);
    // Utiliza HTTP GET en lugar de HTTP POST
    return this.http.get(apiUrl);
  }
}
  