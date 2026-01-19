import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-notes-list',
  standalone: true,               // ✅ REQUIRED
  imports: [
    CommonModule,                 // ✅ for *ngFor
    MatCardModule                 // ✅ for mat-card
  ],
  templateUrl: './notes-list.html',
  styleUrls: ['./notes-list.css']
})
export class NotesListComponent implements OnInit {

  notes = [
    { title: 'First Note', description: 'Hello World' },
    { title: 'Second Note', description: 'Angular Notes App' }
  ];

  ngOnInit(): void {
    // later: fetch notes from backend
  }
}
