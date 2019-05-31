import {
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { RandomService } from '../shared/services/random.service';
import { timer, interval, Observable } from 'rxjs';
import { map, tap, retryWhen, delayWhen } from 'rxjs/operators';

@Component({
  selector: 'app-index-page',
  templateUrl: './index-page.component.html',
  styleUrls: ['./index-page.component.css'],
})
export class IndexPageComponent implements OnInit, OnDestroy {
  randomNumber: any;
  nowLoading = false;

  constructor(private router: Router, private randomService: RandomService) {}

  ngOnInit() {}

  onGetRandomClick() {
    this.getRandom();
  }

  getRandom() {
    this.nowLoading = true;
    this.randomService.getRandom(1).subscribe(
      res => {
        this.nowLoading = false;
        this.randomNumber = JSON.stringify(res);
      },
      error => {
        this.randomService.getRandom(2).subscribe(
          res2 => {
            this.nowLoading = false;
            this.randomNumber = JSON.stringify(res2);
          },
          error2 => {
            this.getRandom();
          }
        );
      }
    );
  }

  ngOnDestroy() {}
}
