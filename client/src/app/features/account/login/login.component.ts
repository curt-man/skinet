import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../../core/services/account.service';
import { Router } from '@angular/router';

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

  returnUrl: string = '.';

  loginForm = this.formBuilder.group({
    email: [''],
    password: [''],
  })

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: ()=> {
        this.accountService.getUserInfo();
        this.router.navigateByUrl(this.returnUrl);
      }
    })
  }
}
