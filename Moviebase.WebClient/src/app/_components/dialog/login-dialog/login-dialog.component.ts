import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AccountService } from '../../../_services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { RegistrationDialogComponent } from '../registration-dialog/registration-dialog.component';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrl: './login-dialog.component.css'
})
export class LoginDialogComponent {
  loginForm: FormGroup

  constructor(
    public dialogRef: MatDialogRef<LoginDialogComponent>,
    private accountService: AccountService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder,
    public dialog: MatDialog
  )
  {
    this.loginForm = this.formBuilder.group({
      username: ['admin', [Validators.required]],
      password: ['admin', [Validators.required]]
    })
  }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe(user => {
      this.toastrService.success(`Welcome ${user.username}!`)
      this.dialogRef.close()
    })
  }
  
  openRegistrationDialog() {
    this.dialogRef.close()
    this.dialog.open(RegistrationDialogComponent)
  }
}
