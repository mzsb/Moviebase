import { CanActivateFn, Router } from '@angular/router';
import { map, take } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';

export const AuthenticationGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService)
  const router = inject(Router)
  return accountService.currentLogged$.pipe(take(1)).pipe(
    map(logged => {
      if(logged?.user.roles.includes("User")){
        return true;
      }
      router.navigate(['/']);
      return false;
    }))
}
