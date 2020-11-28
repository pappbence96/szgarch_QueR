import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IdentityClient, LoginModel, LoginResponse } from '../clients';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private identityClient: IdentityClient;
    private currentLoginSubject: BehaviorSubject<LoginResponse>;
    public currentLogin: Observable<LoginResponse>;

    constructor(identityClient: IdentityClient) {
        this.identityClient = identityClient;
        this.currentLoginSubject = new BehaviorSubject<LoginResponse>(JSON.parse(sessionStorage.getItem('currentLogin')));
        this.currentLogin = this.currentLoginSubject.asObservable();
    }

    public get currentLoginValue(): LoginResponse {
        return this.currentLoginSubject.value;
    }

    login(username: string, password: string): Observable<LoginResponse> {
        return this.identityClient.login(new LoginModel({password, username}))
            .pipe(map(login => {
                sessionStorage.setItem('currentLogin', JSON.stringify(login));
                this.currentLoginSubject.next(login);
                return login;
            }));
    }

    logout(): void {
        sessionStorage.removeItem('currentLogin');
        this.currentLoginSubject.next(null);
    }

    public get token(): string {
        if (!this.isLoggedIn) {
            return null;
        }
        return this.currentLoginValue.token;
    }

    public get isLoggedIn(): boolean {
        return this.currentLoginSubject.value != null;
    }

    public get roles(): string[] {
        if (!this.isLoggedIn) {
            return null;
        }
        const jwtData = this.currentLoginValue.token.split('.')[1];
        const decodedJwtJsonData = window.atob(jwtData);
        const decodedJwtData = JSON.parse(decodedJwtJsonData);
        return decodedJwtData.role;
    }

    public isInRole(role: string): boolean {
        return this.roles.indexOf(role) !== -1;
    }

    public get userName(): string {
        if (!this.isLoggedIn) {
            return null;
        }
        const jwtData = this.currentLoginValue.token.split('.')[1];
        const decodedJwtJsonData = window.atob(jwtData);
        const decodedJwtData = JSON.parse(decodedJwtJsonData);
        return decodedJwtData.userName;
    }

    public get companyId(): number {
        if (!this.isLoggedIn) {
            return null;
        }
        const jwtData = this.currentLoginValue.token.split('.')[1];
        const decodedJwtJsonData = window.atob(jwtData);
        const decodedJwtData = JSON.parse(decodedJwtJsonData);
        return decodedJwtData.userName;
    }

    public get managedSiteId(): number {
        if (!this.isLoggedIn) {
            return null;
        }
        const jwtData = this.currentLoginValue.token.split('.')[1];
        const decodedJwtJsonData = window.atob(jwtData);
        const decodedJwtData = JSON.parse(decodedJwtJsonData);
        return decodedJwtData.managed_site;
    }

    public get worksiteId(): number {
        if (!this.isLoggedIn) {
            return null;
        }
        const jwtData = this.currentLoginValue.token.split('.')[1];
        const decodedJwtJsonData = window.atob(jwtData);
        const decodedJwtData = JSON.parse(decodedJwtJsonData);
        return decodedJwtData.worksite;
    }

    public get assignedQueueId(): number {
        if (!this.isLoggedIn) {
            return null;
        }
        const jwtData = this.currentLoginValue.token.split('.')[1];
        const decodedJwtJsonData = window.atob(jwtData);
        const decodedJwtData = JSON.parse(decodedJwtJsonData);
        return decodedJwtData.assigned_queue;
    }

    public get administratedCompanyId(): number {
        if (!this.isLoggedIn) {
            return null;
        }
        const jwtData = this.currentLoginValue.token.split('.')[1];
        const decodedJwtJsonData = window.atob(jwtData);
        const decodedJwtData = JSON.parse(decodedJwtJsonData);
        return decodedJwtData.administrated_company;
    }
}
