import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FlightSearchService } from '../../services/flight-search.service';
import { SearchResultsComponent } from '../search-results/search-results.component';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SearchResultsComponent
  ]
})
export class SearchFormComponent implements OnInit {
  searchForm!: FormGroup;
  flightSearchResults: any[] = [];
  showResults = false;  

  constructor(private fb: FormBuilder, private flightSearchService: FlightSearchService) { }

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      origin: ['', Validators.required],
      destination: ['', Validators.required],
      currency: ['USD'],
      tripType: ['oneWay']  
    });
  }

  onSubmit() {
    if (this.searchForm.valid) {
      const searchParams = this.searchForm.value;
      console.log(searchParams);
      this.flightSearchResults = [];
      this.showResults = false;
      this.flightSearchService.searchFlights(searchParams)
        .subscribe(response => {
          if (response.length === 0) {
            this.showNoRoutesAvailableMessage(searchParams.origin,searchParams.destination );
          } else {
            this.flightSearchResults = response;
            this.showResults = true;
          }
        }, error => {
          console.error('Error searching flights:', error);
          this.showResults = false;
        });
    } else {
      console.error('Search form is invalid.');
    }
  }

  private showNoRoutesAvailableMessage(origin: string, destination:string): void {
    Swal.fire({
      icon: 'warning',
      title: 'No hay rutas disponibles',
      text: `Lo siento, no fue posible encontrar una ruta de ${origin} a ${destination}`,
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Aceptar',
      customClass: {
        popup: 'my-swal-popup',  
        title: 'my-swal-title',  
        confirmButton: 'my-swal-confirm-button', 
      },
    });
  }
  
  
  
}
