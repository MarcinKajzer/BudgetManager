import { Component } from '@angular/core';
import { SignUp } from 'src/app/types/sign-up.type';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent {
  
  protected signUpData: SignUp = {email: '', password: '', confirmPassword: ''};

  constructor(private authService: AuthService) {}

  signUp() {
    this.authService.signUp(this.signUpData)
  }
}
