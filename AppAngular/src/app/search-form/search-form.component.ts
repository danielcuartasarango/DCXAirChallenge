import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FlightSearchService } from '../services/flight-search.service';
import { SearchResultsComponent } from '../search-results/search-results.component';
import { CommonModule } from '@angular/common';

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
  showResults = false; // Flag to control results display

  constructor(private fb: FormBuilder, private flightSearchService: FlightSearchService) { }

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      origin: ['', Validators.required],
      destination: ['', Validators.required],
      currency: ['USD'],
      tripType: ['oneWay'] // Default trip type to 'oneWay'
    });
  }

  onSubmit() {
    if (this.searchForm.valid) {
      const searchParams = this.searchForm.value;
      console.log(searchParams)
      this.flightSearchService.searchFlights(searchParams)
        .subscribe(response => {
          this.flightSearchResults = response;
          this.showResults = true; // Show results if data is received
        }, error => {
          console.error('Error searching flights:', error);
          this.showResults = false; // Hide results on error
        });
    } else {
      console.error('Search form is invalid.');
    }
  }
}
