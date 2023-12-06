import { Component, OnInit } from '@angular/core';
import { MovieService } from '../../_services/movie.service';
import { Movie } from '../../models/movie';
import { Pagination } from '../../models/pagination';
import { PageEvent } from '@angular/material/paginator';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoadingService } from '../../_services/loading.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit { 
  movies: Movie[] = []
  pageNumber = 1
  pageSize = 12
  pagination: Pagination | undefined
  isLoading: boolean = false

  constructor(private movieService: MovieService) {}

  ngOnInit() {
    this.loadMovies()
  }

  ngAfterViewChecked() {
    if(this.pagination) {
      const list = document.getElementsByClassName('mat-mdc-paginator-range-label')
      list[0].innerHTML = this.pageNumber.toString()
    }
  }

  refresh() {
    this.loadMovies()
  }

  showRefresh() { this.isLoading = false }
  hideRefresh() { this.isLoading = true }

  loadMovies() {
    this.hideRefresh()
    this.movieService.getMovies(this.pageNumber, this.pageSize).subscribe({
      next: response =>
      {
        this.movies = response.result
        this.pagination = response.pagination
      },
      error: err => this.showRefresh()
    })
  }

  pageChanged(pageEvent: PageEvent) {
    this.pageNumber = pageEvent.pageIndex + 1
    this.pageSize = pageEvent.pageSize
    this.loadMovies()
  }

}
