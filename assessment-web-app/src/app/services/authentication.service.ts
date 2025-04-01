import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface LoginResponse {
  userId: string;
  username: string;
  accessToken: string;
}

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {
  constructor(private httpClient : HttpClient) { }

  login(username: string, password : string) {
    let url = 'http://localhost:5274/api/auth/login';
    return this.httpClient.post<LoginResponse>(url, {username, password});
  }
}
