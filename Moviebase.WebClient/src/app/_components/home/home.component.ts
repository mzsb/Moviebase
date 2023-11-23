import { Component } from '@angular/core';
import { TestItem } from '../../_models/test.item';
import { TestService } from '../../_services/test.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  displayedColumns: string[] = ['testItemId'];
  dataSource: TestItem[] = [];

  constructor(private testService: TestService) {}

  getTestItems() { 
    this.testService.getTestItems().subscribe(reponse => {
      this.dataSource = reponse
    });
  }

  clear() {
    this.dataSource = []
  }
}
