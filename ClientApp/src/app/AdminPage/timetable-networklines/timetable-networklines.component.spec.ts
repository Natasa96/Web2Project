import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimetableNetworklinesComponent } from './timetable-networklines.component';

describe('TimetableNetworklinesComponent', () => {
  let component: TimetableNetworklinesComponent;
  let fixture: ComponentFixture<TimetableNetworklinesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimetableNetworklinesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimetableNetworklinesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
