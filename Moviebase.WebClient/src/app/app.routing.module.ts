import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './_components/home/home.component';
import { ReviewComponent } from './_components/review/review.component';
import { AuthenticationGuard } from './_guards/authentication.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'review/:movieId', component: ReviewComponent, canActivate: [AuthenticationGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
