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
        this.currentLoginSubject = new BehaviorSubject<LoginResponse>(JSON.parse(localStorage.getItem('currentLogin')));
        this.currentLogin = this.currentLoginSubject.asObservable();
    }

    public get currentLoginValue(): LoginResponse {
        return this.currentLoginSubject.value;
    }

    login(username: string, password: string): Observable<LoginResponse> {
        return this.identityClient.login(new LoginModel({password, username}))
            .pipe(map(login => {
                localStorage.setItem('currentLogin', JSON.stringify(login));
                this.currentLoginSubject.next(login);
                return login;
            }));
    }

    logout(): void {
        localStorage.removeItem('currentLogin');
        this.currentLoginSubject.next(null);
    }
}