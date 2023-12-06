import { Component } from '@angular/core';
import { Guid } from 'typescript-guid';
import { ActivatedRoute } from '@angular/router';
import { Review } from '../../models/review';
import { ReviewService } from '../../_services/review.service';
import { Pagination } from '../../models/pagination';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrl: './review.component.css'
})
export class ReviewComponent {
  movieId!: Guid
  reviews: Review[] = []
  pageNumber = 1
  pageSize = 12
  pagination: Pagination | undefined

  constructor(
    private route: ActivatedRoute,
    private reviewService: ReviewService) {}

  ngOnInit(): void {
    const rawMovieId = this.route.snapshot.paramMap.get('movieId')
    if(rawMovieId) {
      this.movieId = Guid.parse(rawMovieId)
    }

    this.reviewService.getReviewsOfMovie(this.movieId, this.pageNumber, this.pageSize).subscribe({
      next: response =>
      {
        this.reviews = response.result
        this.pagination = response.pagination
      },
    })
  }
}
