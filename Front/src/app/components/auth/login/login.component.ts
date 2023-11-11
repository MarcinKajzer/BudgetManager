import { Component } from '@angular/core';
import { SignIn } from 'src/app/models/sign-in.type';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  signInData: SignIn = {email: '', password: ''}; 

  constructor(private authService: AuthService) {}

  signIn() {
    //validation
    this.authService.signIn(this.signInData);
    //error messages
  }
}
