import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-create-note',
   standalone: true,
  imports: [FormsModule,MatCardModule, MatInputModule,MatButtonModule ],
  templateUrl: './create-note.html',
  styleUrl: './create-note.css',
})
export class CreateNote {

  title = '';
  description = '';

  addNote() {
    console.log(this.title, this.description);
  }
}
