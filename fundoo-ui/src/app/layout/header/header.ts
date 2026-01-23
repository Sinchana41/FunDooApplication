import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SidebarComponent } from '../sidebar/sidebar';
import { DashboardComponent } from '../../pages/dashboard/dashboard';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    SidebarComponent,
    DashboardComponent],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {

}
