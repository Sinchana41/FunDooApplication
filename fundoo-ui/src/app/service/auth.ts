import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest, SignupRequest } from '../pages/model/model';
import { environment } from '../pages/environment/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = `${environment.apiUrl}/User`;

  constructor(private http: HttpClient) {}

  // LOGIN
  login(data: LoginRequest) {
    return this.http.post<any>(`${this.baseUrl}/login`, data);
  }

  // SIGNUP
  signup(data: SignupRequest) {
    return this.http.post<any>(`${this.baseUrl}/register`, data);
  }

  // SAVE TOKEN
  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  // GET TOKEN
  getToken() {
    return localStorage.getItem('token');
  }

    getNotes() {
    return this.http.get<any[]>(this.baseUrl);
  }

  createNote(note: any) {
    return this.http.post(this.baseUrl, note);
  }

  // LOGOUT
  logout() {
    localStorage.clear();
  }
}
