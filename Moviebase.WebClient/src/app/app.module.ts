import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app.routing.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { HomeComponent } from './_components/home/home.component';
import { MapPipe } from './pipes/map.pipe';
import { JoinPipe } from './pipes/join.pipe';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginDialogComponent } from './_components/dialog/login-dialog/login-dialog.component';
import { MaterialModule } from './material.module';
import { ToastrModule } from 'ngx-toastr';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { RolePipe } from './pipes/role.pipe';
import { AddMovieDialogComponent } from './_components/dialog/add-movie-dialog/add-movie-dialog.component';
import { RegistrationDialogComponent } from './_components/dialog/registration-dialog/registration-dialog.component';
import { ReviewComponent } from './_components/review/review.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ReviewComponent,
    LoginDialogComponent,
    RegistrationDialogComponent,
    AddMovieDialogComponent,
    MapPipe,
    JoinPipe,
    RolePipe
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    MaterialModule,
    NgxSpinnerModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 2500,
      positionClass: 'toast-bottom-right'
    })
  ],
  providers: [
    MapPipe,
    JoinPipe,
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
