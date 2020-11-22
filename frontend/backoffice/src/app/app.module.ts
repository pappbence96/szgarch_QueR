import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { API_BASE_URL } from './shared/clients';
import { LoginComponent } from './core/components/login/login.component';

import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './core/components/header/header.component';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    CoreModule
  ],

  providers: [
    {
      provide: API_BASE_URL,
      useValue: 'https://localhost:5001'
    }
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
