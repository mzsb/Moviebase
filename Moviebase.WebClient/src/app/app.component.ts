import { Component, OnInit } from '@angular/core'
import { MatDialog } from '@angular/material/dialog';
import { LoginDialogComponent } from './_components/dialog/login-dialog/login-dialog.component';
import { AccountService } from './_services/account.service';
import { Logged } from './models/logged';
import { AddMovieDialogComponent } from './_components/dialog/add-movie-dialog/add-movie-dialog.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(
    public accountService: AccountService,
    public dialog: MatDialog) {}

  ngOnInit() {
    const logged: Logged = JSON.parse(localStorage.getItem('logged') as string);
    if(logged) {
      this.accountService.setCurrentLogged(logged);
    }
  }

  openLoginDialog() {
    this.dialog.open(LoginDialogComponent);
  }

  openAddMovieDialog() {
    this.dialog.open(AddMovieDialogComponent);
  }

  logout(){
    this.accountService.logout();
  }
}
