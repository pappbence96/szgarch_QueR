/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { QueueTypesPageComponent } from './queue-types-page.component';

describe('QueueTypesPageComponent', () => {
  let component: QueueTypesPageComponent;
  let fixture: ComponentFixture<QueueTypesPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QueueTypesPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QueueTypesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
