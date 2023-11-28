import { Component, OnInit } from '@angular/core';
import { MovieService } from '../../_services/movie.service';
import { Movie } from '../../models/movie';
import { Pagination } from '../../models/pagination';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit { 
  movies: Movie[] = [];
  displayedColumns: string[] = ['movieId'];
  pageNumber = 1;
  pageSize = 9;
  pagination: Pagination | undefined;

  constructor(private movieService: MovieService){}

  ngOnInit() {
    this.loadMovies()
  }

  ngAfterViewChecked() {
    if(this.pagination) {
      const list = document.getElementsByClassName('mat-mdc-paginator-range-label');
      list[0].innerHTML = `${this.pageNumber} / ${Math.ceil(this.pagination!.totalCount / this.pageSize)}`;
    }
  }

  loadMovies() {
    this.movieService.getMovies(this.pageNumber, this.pageSize).subscribe(response => {
      this.movies = response.result;
      this.pagination = response.pagination;
    })
  }

  pageChanged(event: any) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadMovies();
  }
}
