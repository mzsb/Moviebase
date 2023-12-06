import { NgModule } from '@angular/core'
import { MatTableModule } from '@angular/material/table'
import { MatButtonModule } from '@angular/material/button'
import { MatToolbarModule } from '@angular/material/toolbar'
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';

@NgModule({
  declarations: [],
  imports: [],
  exports: [
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatTableModule, 
    MatButtonModule, 
    MatToolbarModule,
    MatDividerModule,
    MatPaginatorModule,
    MatGridListModule,
    MatCardModule,
    MatIconModule,
    MatMenuModule,
  ]
})
export class MaterialModule { }
