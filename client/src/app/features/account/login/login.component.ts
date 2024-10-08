import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../../core/services/account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    MatCard, MatLabel, MatFormField, ReactiveFormsModule, MatButton, MatInput
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private formBuilder = inject(FormBuilder);
  private accountService = inject(AccountService);
  private router = inject(Router);
  private activatedRoute = inject(ActivatedRoute)

  returnUrl = '/shop';

  loginForm = this.formBuilder.group({
    email: [''],
    password: [''],
  })

  constructor() {
    const url = this.activatedRoute.snapshot.queryParams['returnUrl'];
    if(url) this.returnUrl = url;
  }
  
  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => {
        this.accountService.getUserInfo().subscribe({
          next: (user) => {
            console.log('User info fetched:', user);
          }
        });
        this.router.navigateByUrl(this.returnUrl);
      }
    })
  }
}
