import { CommonModule } from '@angular/common';
import { Component, Injectable, Input, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SearchFormComponent } from '../search-form/search-form.component';
@Component({
  selector: 'app-search-results',
  standalone: true,
  imports: [ 
  FormsModule,
  CommonModule,
  ReactiveFormsModule,
  SearchFormComponent,
  
],
  templateUrl: './search-results.component.html',
  styleUrl: './search-results.component.css'
})
@Injectable() 
export class SearchResultsComponent implements OnInit {
  @Input() flightResults!: any[];

  constructor() { }

  ngOnInit(): void {
  }
}
