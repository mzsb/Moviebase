import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AccountService } from '../../../_services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration-dialog',
  templateUrl: './registration-dialog.component.html',
  styleUrl: './registration-dialog.component.css'
})
export class RegistrationDialogComponent {
  registrationForm: FormGroup

  constructor(
    public dialogRef: MatDialogRef<RegistrationDialogComponent>,
    private accountService: AccountService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder
  )
  {
    this.registrationForm = this.formBuilder.group({
      username: ['user', [Validators.required]],
      password: ['user', [Validators.required]],
      email: ['user@examp.le', [Validators.required]],
    })
  }

  onSubmit() {
    this.accountService.register(this.registrationForm.value).subscribe(user => {
      this.toastrService.success(`Welcome ${user.username}!`)
      this.dialogRef.close()
    })
  }
}