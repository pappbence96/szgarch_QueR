/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AdministratorsPageComponent } from './administrators-page.component';

describe('AdministratorsPageComponent', () => {
  let component: AdministratorsPageComponent;
  let fixture: ComponentFixture<AdministratorsPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdministratorsPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministratorsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
