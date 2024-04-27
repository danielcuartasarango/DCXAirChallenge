import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.css']
})
export class SearchFormComponent implements OnInit {
  searchForm!: FormGroup; // Usamos el operador '!' para indicar que será inicializado en el constructor

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      origin: ['', Validators.required],
      destination: ['', Validators.required],
      currency: ['USD', Validators.required],
      roundTrip: [false]
    });
  }

  submitForm(): void {
    if (this.searchForm.valid) {
      // Aquí puedes enviar la solicitud a la API con los parámetros del formulario
      console.log('Formulario enviado:', this.searchForm.value);
    }
  }
}
