import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckTicketInfoComponent } from './check-ticket-info.component';

describe('CheckTicketInfoComponent', () => {
  let component: CheckTicketInfoComponent;
  let fixture: ComponentFixture<CheckTicketInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CheckTicketInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckTicketInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
