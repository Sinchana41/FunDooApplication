import { Component } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { SidebarComponent } from '../sidebar/sidebar';
import { NavbarComponent } from '../navbar/navbar';
import { Notes } from '../../pages/notes/notes';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NotesListComponent } from '../../pages/notes-list/notes-list';
import { Archive } from "../../pages/archive/archive";


@Component({
  selector: 'app-dashboard-display',
  imports: [MatSidenav,
    SidebarComponent,
    NavbarComponent,

    MatSidenavModule,
    MatIconModule,
    MatToolbarModule,
    NotesListComponent, Archive,
    Notes
  ],
  templateUrl: './dashboard-display.html',
  styleUrl: './dashboard-display.css',
})
export class DashboardDisplay {
  expanded=true;

}
