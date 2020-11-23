/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AdministratorMenuPageComponent } from './administrator-menu-page.component';

describe('AdministratorMenuPageComponent', () => {
  let component: AdministratorMenuPageComponent;
  let fixture: ComponentFixture<AdministratorMenuPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdministratorMenuPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministratorMenuPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
