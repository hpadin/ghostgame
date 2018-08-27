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
  turn;
  message = '';

  constructor( private gameService: GameService) {
  }

  ngOnInit(): void {

    this.gameService.getAlphabet().then(data => {
      let response : any = data;
      this.alphabet = response.data.split("");
    });

    this.gameService.getGameState().then(data => {
      let response : any = data;
      console.log(response);
      this.word = response.data.word;
      this.turn = response.data.gameStatus;
      this.message = response.data.message;

    })
  }

  letterClicked(letter : string) {

    this.gameService.processLetter(letter).then(data => {
      let response : any = data;
      this.word = response.data.word;
      this.turn = response.data.gameStatus;
      this.message = response.data.message;

      console.log(response);
    });
  }

  resetGame(){
    this.gameService.resetGame().then(data => {
      let response : any = data;
      this.word = response.data.word;
      this.turn = response.data.gameStatus;
      this.message = response.data.message;

      console.log(response);
    });
  }

}
