import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})

export class ApiService {
  private baseUrl = 'https://localhost:7224';

  constructor(private http: HttpClient) {}

  getCustomers(): Observable<any> {  // return as observable worked cuz of data
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return this.http.get<any>(`${this.baseUrl}/api/Customer/GetCustomers`, httpOptions);
  }

  addCustomer(customerData: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(`${this.baseUrl}/api/Customer/AddACustomer`, customerData, httpOptions);
  }

  deleteCustomer(id: number): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.delete<any>(`${this.baseUrl}/api/Customer/DeleteACustomer?id=${id}`, httpOptions);
  }
}
