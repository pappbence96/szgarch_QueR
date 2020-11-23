/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { OperatorMenuPageComponent } from './operator-menu-page.component';

describe('OperatorMenuPageComponent', () => {
  let component: OperatorMenuPageComponent;
  let fixture: ComponentFixture<OperatorMenuPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OperatorMenuPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OperatorMenuPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
