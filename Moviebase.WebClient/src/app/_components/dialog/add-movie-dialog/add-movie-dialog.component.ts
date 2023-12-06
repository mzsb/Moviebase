import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MovieService } from '../../../_services/movie.service';

@Component({
  selector: 'app-add-movie-dialog',
  templateUrl: './add-movie-dialog.component.html',
  styleUrl: './add-movie-dialog.component.css'
})
export class AddMovieDialogComponent {
  movieForm: FormGroup

  constructor(
    public dialogRef: MatDialogRef<AddMovieDialogComponent>,
    private movieService: MovieService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder,
  )
  {
    this.movieForm = this.formBuilder.group({
      title: ['Example', [Validators.required]]
    })
  }

  onSubmit() {
    this.movieService.addMovies(this.movieForm.value).subscribe(movie => {
      this.toastrService.success(`${movie.title} added!`)
      this.dialogRef.close()
    })
  }
}