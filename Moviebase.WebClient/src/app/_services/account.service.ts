import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Logged } from '../models/logged';
import { Login } from '../models/login';
import { ReplaySubject, map } from 'rxjs';
import { Register } from '../models/register';

@Injectable({providedIn: 'root'})
export class AccountService {
  baseUrl = environment.apiUrl + 'account';
  private currentLoggedSource = new ReplaySubject<Logged | null>(1);
  currentLogged$ = this.currentLoggedSource.asObservable();

  constructor(private http: HttpClient) { }

  login(login: Login) {
    return this.http.post<Logged>(this.baseUrl + '/login', login).pipe(
      map((response: Logged) => {
        if (response) {
          this.setCurrentLogged(response)
        }
        return response.user
      })
    )
  }

  logout() {
    localStorage.removeItem('logged');
    this.currentLoggedSource.next(null);
  }

  register(register: Register) {
    return this.http.post<Logged>(this.baseUrl + '/register', register).pipe(
      map((response: Logged) => {
        if (response) {
          this.setCurrentLogged(response)
        }
        return response.user
      })
    )
  }

  setCurrentLogged(logged: Logged) {
    let user = logged.user
    user.roles = []
    const roles = this.getDecodedToken(logged.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('logged', JSON.stringify(logged));
    this.currentLoggedSource.next(logged);
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
  
}