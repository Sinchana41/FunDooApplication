import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { TrashComponent } from './pages/trash/trash';


@Component({
  
  selector: 'app-root',
  imports: [RouterOutlet,
    HttpClientModule,
    ReactiveFormsModule,
    
   ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  protected readonly title = signal('fundoo-ui');

}
