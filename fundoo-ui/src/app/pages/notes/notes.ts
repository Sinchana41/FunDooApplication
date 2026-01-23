import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-notes',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './notes.html',
  styleUrl: './notes.css'
})



export class Notes {

  isExpanded = false;

  title = '';
  description = '';

  // dummy notes (replace with backend later)
  notes: any[] = [];

  expandNote() {
    this.isExpanded = true;
  }

  closeNote(event: Event) {
    event.stopPropagation();

    if (this.title || this.description) {
      this.notes.unshift({
        title: this.title,
        description: this.description
      });
    }

    this.title = '';
    this.description = '';
    this.isExpanded = false;
  }
}
