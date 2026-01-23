import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NotesService } from '../notes/service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css']
})
export class DashboardComponent {

  isExpanded = false;
  title = '';
  description = '';

  constructor(private notesService: NotesService) {}

  expandNote() {
    this.isExpanded = true;
  }

  closeNote() {
    if (!this.title.trim() && !this.description.trim()) {
      this.reset();
      return;
    }

    this.notesService.addNote({
      title: this.title,
      description: this.description
    }).subscribe(() => this.reset());
  }

  reset() {
    this.isExpanded = false;
    this.title = '';
    this.description = '';
  }

  stop(e: Event) {
    e.stopPropagation();
  }
}
