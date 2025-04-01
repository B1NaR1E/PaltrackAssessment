import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface Customer {
  Id: number;
  Name: string;
  Phone: string;
  Email: string;
  PostalZip: string;
  Region: string;
  Country: string;
}
@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  constructor(private http: HttpClient) { }

  getCustomers(){
    let token = localStorage.getItem("token");
    console.log(token);

    var headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', 'Bearer ' + token);

    console.log(headers);

    let url = "http://localhost:5274/api/customers";
    return this.http.get<Customer[]>(url, {headers: headers});
  }
}
