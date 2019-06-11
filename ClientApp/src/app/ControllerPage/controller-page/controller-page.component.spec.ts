import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ControllerPageComponent } from './controller-page.component';

describe('ControllerPageComponent', () => {
  let component: ControllerPageComponent;
  let fixture: ComponentFixture<ControllerPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ControllerPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ControllerPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
