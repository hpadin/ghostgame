import { Component, OnInit } from '@angular/core';
import { GameService } from './game.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Ghost Game Challenge!';
  alphabet = null;
  word = '';
  status;
  message = '';
  isLoading;

  constructor( private gameService: GameService) {
  }

  ngOnInit(): void {
    this.isLoading = true;
    this.gameService.getAlphabet().then(data => {
      let response : any = data;
      this.alphabet = response.data.split("");
      this.isLoading = false;
    });

    this.isLoading = true;
    this.gameService.getGameState().then(data => {      
      let response : any = data;
      console.log(response);
      this.word = response.data.word;
      this.status = response.data.gameStatus;
      this.message = response.data.message;
      this.isLoading = false;
    })
  }

  letterClicked(letter : string) {
    this.isLoading = true;
    this.gameService.processLetter(letter).then(data => {
      let response : any = data;
      this.word = response.data.word;
      this.status = response.data.gameStatus;
      this.message = response.data.message;
      this.isLoading = false;
    });
  }

  resetGame(){
    this.isLoading = true;
    this.gameService.resetGame().then(data => {
      let response : any = data;
      this.word = response.data.word;
      this.status = response.data.gameStatus;
      this.message = response.data.message;
      this.isLoading = false;
    });
  }

}
