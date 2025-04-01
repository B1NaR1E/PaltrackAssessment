import { Component } from '@angular/core';
import { CustomerService, Customer } from '../services/customer.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})

export class HomeComponent {
  customers: Customer[] = [];
  expanded: Boolean = true;
  constructor(private customerService: CustomerService) {
    customerService.getCustomers()
      .subscribe({
        next: data => {
          this.customers = data as Customer[];
        },
        error: err => {
          alert("An error occurred while getting customers.");
        }
      });
  }
}
