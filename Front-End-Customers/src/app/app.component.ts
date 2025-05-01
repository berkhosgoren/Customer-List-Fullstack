import { Component, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { Table } from 'primeng/table';
import { ViewChild } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  Customer: any[] = [];
  showCustomerList: boolean = false;
  showCustomerForm: boolean = false;

  newCustomer: any = {
    firstName: '',
    lastName: '',
    email: '',
    customerInfo: {
      city: '',
      street: '',
      address: '',
      phoneNumber: ''
    }
  };

  @ViewChild('customerTable') customerTable!: Table;

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.getCustomers();
  }

  getCustomers(): void {
    this.apiService.getCustomers().subscribe({
      next: (data) => {
        this.Customer = data;
        console.log('Customers loaded:', this.Customer);
      },
      error: (error) => {
        console.error('Error fetching customers:', error);
      },
      complete: () => {
        console.log('Request completed');
      }
    });
  }

  addCustomer(): void {
    this.apiService.addCustomer(this.newCustomer).subscribe({
      next: (response) => {
        console.log('Customer added:', response);
        this.getCustomers();
        this.customerTable.reset();
        this.resetForm();
        this.showCustomerForm = false;
      },
      error: (error) => {
        console.error('Error adding customer:', error);
      },
      complete: () => {
        console.log('Add customer request completed');
      }
    });
  }

  deleteCustomer(id: number): void {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.apiService.deleteCustomer(id).subscribe({
        next: (response) => {
          console.log('Customer deleted:', response);
          this.getCustomers();
          this.customerTable.reset(); 
        },
        error: (error) => {
          console.error('Error deleting customer:', error);
        },
        complete: () => {
          console.log('Delete customer request completed');
        }
      });
    }
  }

  resetForm(): void {
    this.newCustomer = {
      firstName: '',
      lastName: '',
      email: '',
      customerInfo: {
        city: '',
        street: '',
        address: '',
        phoneNumber: ''
      }
    };
  }
}
