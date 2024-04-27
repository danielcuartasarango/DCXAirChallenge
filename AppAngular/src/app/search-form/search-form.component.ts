import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms'; // Import FormsModule for template-driven forms
import { FlightSearchService } from '../services/flight-search.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.css'],
  standalone: true,
  imports: [ // Add FormsModule and CommonModule to imports
    FormsModule,
    CommonModule,
    ReactiveFormsModule
  ]
})
export class SearchFormComponent implements OnInit {
  searchForm!: FormGroup;

  constructor(private fb: FormBuilder,  private flightSearchService: FlightSearchService) { }

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      origin: ['', Validators.required],
      destination: ['', Validators.required],
      currency: ['USD'],
      roundTrip: [false]
    });
  }

  onSubmit() {
    if (this.searchForm.valid) {
      const searchParams = this.searchForm.value; // Access form data
      this.flightSearchService.searchFlights(searchParams)
        .subscribe(response => {
          console.log('Flight search results:', response);
          // Handle successful flight search response (e.g., display results)
        }, error => {
          console.error('Error searching flights:', error);
          // Handle errors during flight search
        });
    } else {
      console.error('Search form is invalid.');
      // Handle form validation errors (e.g., display error messages)
    }
  }
}