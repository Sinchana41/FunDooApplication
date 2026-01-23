import { Component, OnInit } from '@angular/core';
import { NotesService } from '../notes/service';
import { Notes } from '../notes/notes';

@Component({
  selector: 'app-trash',
  templateUrl: './trash.html'
})
export class TrashComponent implements OnInit {

  trashedNotes: Notes[] = [];

  constructor(private notesService: NotesService) {}

  ngOnInit(): void {
    this.loadTrash();
  }

  loadTrash(): void {
    this.notesService.getNotes().subscribe((res: Notes[]) => {
      this.trashedNotes = res.filter(note => note.trashed === true);
    });
  }

  restore(note: Notes): void {
    note.trashed = false;
    this.notesService.updateNote(note).subscribe(() => {
      this.loadTrash();
    });
  }

  deleteForever(id: number): void {
    this.notesService.deleteForever(id).subscribe(() => {
      this.loadTrash();
    });
  }
}
