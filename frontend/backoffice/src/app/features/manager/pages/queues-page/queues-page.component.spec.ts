/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { QueuesPageComponent } from './queues-page.component';

describe('QueuesPageComponent', () => {
  let component: QueuesPageComponent;
  let fixture: ComponentFixture<QueuesPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QueuesPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QueuesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
