import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthService } from './AuthService';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authService: AuthService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (!this.authService.isLoggedIn) {
            this.router.navigate(['/login']/*, { queryParams: { returnUrl: state.url } }*/);
            return false;
        }

        const role = route.data.role as string;
        if (!this.authService.isInRole(role)) {
            this.router.navigate(['/login' ]);
            return false;
        }

        return true;
    }
}
