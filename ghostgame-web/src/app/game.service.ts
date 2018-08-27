import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError as observableThrowError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { IGameStatus } from '../interfaces/igamestatus';

@Injectable()
export class GameService {
    private apiUrl = 'http://localhost:52722/api/GameService/';

  constructor(private http: HttpClient) {}

  getAlphabet() {
    return this.http.get(this.apiUrl + 'GetAlphabet')
    .pipe(map(data => data, catchError(this.handleError))).toPromise();
  }

  getGameState() {
    return this.http.get(this.apiUrl + 'GetGameState')
    .pipe(map(data => data, catchError(this.handleError))).toPromise();
  }

  processLetter(letter : string) {
    return this.http.get(this.apiUrl + 'ProcessLetter?pLetter=' + letter)
    .pipe(map(data => data, catchError(this.handleError))).toPromise();
  }

  resetGame() {
    return this.http.get(this.apiUrl + 'ResetGame')
    .pipe(map(data => data, catchError(this.handleError))).toPromise();
  }

  private handleError(res: HttpErrorResponse | any) {
    console.error(res.error || res.body.error);
    return observableThrowError(res.error || 'Server error');
  }
}
