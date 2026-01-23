import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  private baseUrl = 'https://localhost:7202/api/notes'; // CHANGE PORT IF NEEDED

  constructor(private http: HttpClient) {}

 private headers() {
  const token = localStorage.getItem('token');

  return {
    headers: new HttpHeaders({
      Authorization: `Bearer ${token}`
    })
  };
}


  // CREATE
 addNote(note: any) {
  return this.http.post(this.baseUrl, note, this.headers());
}

  // GET ALL
  getNotes() {
    return this.http.get(this.baseUrl, this.headers());
  }

  // UPDATE
  updateNote(id: number, note: any) {
    return this.http.put(`${this.baseUrl}/${id}`, note, this.headers());
  }

  // DELETE (MOVE TO TRASH)
  deleteNote(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`, this.headers());
  }
}
