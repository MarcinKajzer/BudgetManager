import { Component } from '@angular/core';
import { SignUp } from 'src/app/types/sign-up.type';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  
  protected signUpData: SignUp = {email: '', password: '', confirmPassword: ''};

  constructor(private authService: AuthService) {}

  signUp() {
    this.authService.signUp(this.signUpData)
  }
}
