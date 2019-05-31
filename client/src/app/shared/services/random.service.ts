import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, timer } from 'rxjs';
import { map, retryWhen, tap, delayWhen } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class RandomService {
  constructor(private http: HttpClient) {}

  getRandom(apiVersion: Number): Observable<any> {
    return this.http.get<any>(`/api_v${apiVersion}/random-generator/getrandom`);
  }
}
