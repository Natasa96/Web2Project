import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeRideScheduleComponent } from './home-ride-schedule.component';

describe('HomeRideScheduleComponent', () => {
  let component: HomeRideScheduleComponent;
  let fixture: ComponentFixture<HomeRideScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeRideScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeRideScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
