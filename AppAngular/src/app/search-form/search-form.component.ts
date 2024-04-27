import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms'; // Import FormsModule for template-driven forms
import { FlightSearchService } from '../services/flight-search.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.css'],
  standalone: true,
  imports: [ // Add FormsModule and CommonModule to imports
    FormsModule,
    CommonModule
  ]
})
export class SearchFormComponent implements OnInit {
  searchForm: any = {}; // Initialize searchForm as an object

  constructor(
    private flightSearchService: FlightSearchService // Removed FormBuilder
  ) { }

  ngOnInit(): void {
    this.searchForm = {
      origin: '',
      destination: '',
      currency: 'USD',
      roundTrip: false
    };
  }

  nombre: string = '';

  onSubmit(event: Event): void {
    event.preventDefault();
    console.log('Formulario enviado:', this.searchForm); // Log the entire searchForm object
  }
}
