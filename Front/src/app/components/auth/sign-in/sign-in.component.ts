import { Component } from '@angular/core';
import { SignIn } from 'src/app/types/sign-in.type';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
  signInData: SignIn = {email: '', password: ''}; 

  constructor(private authService: AuthService, private router: Router) {}

  signIn() {
    //validation
    this.authService.signIn(this.signInData).subscribe(() => {
      this.router.navigate(["incomes"]);
    })
    //error messages
  }
}
