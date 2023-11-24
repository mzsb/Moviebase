import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { TestItem } from '../models/test.item';

@Injectable({providedIn: 'root'})
export class TestService {
    baseUrl = environment.apiUrl + 'test';

    constructor(private http: HttpClient) { }

    getTestItems() {
        return this.http.get<TestItem[]>(this.baseUrl)
    }
}