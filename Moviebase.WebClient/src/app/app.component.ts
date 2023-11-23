import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { TestService } from './_services/test.service';
import { TestItem } from './_models/test.item';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table'  
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, 
    HttpClientModule, MatTableModule, MatButtonModule, 
    MatToolbarModule ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [TestService]
})
export class AppComponent {
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
