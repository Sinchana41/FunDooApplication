import { Component,OnInit } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AuthService } from 'C:/Users/Sinchana .A.N/OneDrive/Desktop/Angular/fundoo-ui/src/app/service/auth';
import { Router } from '@angular/router';
import { FormBuilder, Validators, FormGroup ,ReactiveFormsModule } from '@angular/forms';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatButtonModule,MatCardModule,MatInputModule, MatFormFieldModule,ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login implements OnInit {

  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onLogin() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.authService.login(this.loginForm.value).subscribe({
      next: (res: any) => {
        localStorage.setItem('token', res.token);
         alert('Login successful');
        this.router.navigate(['/dashboard']);
      },
      error: () => {
        alert('Invalid email or password');
      }
    });
  }
}
