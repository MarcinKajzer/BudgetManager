import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { SignIn } from '../models/sign-in.type';
import { SignUp } from '../models/sign-up.type';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable, filter } from 'rxjs';
import { Router } from '@angular/router';
import { Tokens } from '../models/tokens.type';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private ACCESS_TOKEN_KEY = "accessToken";
  private REFRESH_TOKEN_KEY = "refreshToken";

  private refreshTokenInProgress = false;
  private accessToken$;

  constructor(private httpClient: HttpClient, private jwtHelper: JwtHelperService, private router: Router) {
    this.accessToken$ = new BehaviorSubject<string | null>(localStorage.getItem(this.ACCESS_TOKEN_KEY));
  }

  private apiUrl = `${environment.apiUrl}/auth`;

  signIn(signInModel: SignIn) {
    this.httpClient.post<Tokens>(`${this.apiUrl}/signin`, signInModel)
    .subscribe((tokens: Tokens) => 
      {
        localStorage.setItem(this.ACCESS_TOKEN_KEY, tokens.accessToken);
        localStorage.setItem(this.REFRESH_TOKEN_KEY, tokens.refreshToken);
        this.accessToken$.next(tokens.accessToken)
      });
  }

  signUp(signUpModel: SignUp) {
    this.httpClient.post(`${this.apiUrl}/signup`, signUpModel)
    .subscribe(() => 
      {
        console.log("registration success")
      });
  }

  signOut() {
    localStorage.removeItem(this.ACCESS_TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);

    // this.httpClient.post(`${this.apiUrl}/signout`, null).subscribe((result: any) => {
    //   console.log(result)
    // })

    this.router.navigateByUrl("/signin")
  } 

  getToken(): Observable<string | null> {
    const accessToken = localStorage.getItem(this.ACCESS_TOKEN_KEY);
    
    if (!accessToken || this.jwtHelper.isTokenExpired(accessToken)){
      const refreshToken = localStorage.getItem(this.REFRESH_TOKEN_KEY);
      if (!refreshToken) {
        this.signOut(); 
        this.accessToken$.next(null);
      }
      else {
        if (this.refreshTokenInProgress) {
          this.accessToken$.next(null);
        }
        else {
          this.refreshTokenInProgress = true;
          this.accessToken$.next(null);
          this.httpClient.post<Tokens>(`${this.apiUrl}/refresh`, {
            accessToken, 
            refreshToken 
          }).subscribe({
            next: (tokens: Tokens) => {
              localStorage.setItem(this.ACCESS_TOKEN_KEY, tokens.accessToken);
              localStorage.setItem(this.REFRESH_TOKEN_KEY, tokens.refreshToken);
              this.accessToken$.next(tokens.accessToken);
              this.refreshTokenInProgress = false;
            },
            error: error => {
              console.log(error);
              if (error.status == 401) {
                this.signOut();
              }
              this.refreshTokenInProgress = false;
            }
          });
        }
      }
    }

    return this.accessToken$.asObservable().pipe(filter(token => !!token));
  }
}
