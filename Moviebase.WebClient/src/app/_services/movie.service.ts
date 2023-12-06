import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { getPaginatedResult, getPaginationHeaders } from '../helpers/pagination.helper';
import { Movie } from '../models/movie';
import { CreateMovie } from '../models/create.movie';

@Injectable({providedIn: 'root'})
export class MovieService {
    baseUrl = environment.apiUrl + 'movies';

    constructor(private http: HttpClient) { }

    getMovies(pageNumber: number, pageSize: number) {
        let params = getPaginationHeaders(pageNumber, pageSize);
        return getPaginatedResult<Movie[]>(this.baseUrl, params, this.http);
    }

    addMovies(createMovie: CreateMovie) {
        return this.http.post<Movie>(this.baseUrl, createMovie);
    }
}