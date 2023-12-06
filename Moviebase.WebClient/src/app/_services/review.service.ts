import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { getPaginatedResult, getPaginationHeaders } from '../helpers/pagination.helper';
import { Review } from '../models/review';
import { Guid } from 'typescript-guid';

@Injectable({providedIn: 'root'})
export class ReviewService {
    baseUrl = environment.apiUrl + 'reviews';

    constructor(private http: HttpClient) { }

    getReviewsOfMovie(movieId: Guid, pageNumber: number, pageSize: number) {
        let params = getPaginationHeaders(pageNumber, pageSize);
        return getPaginatedResult<Review[]>(`${this.baseUrl}/${movieId}`, params, this.http);
    }
}