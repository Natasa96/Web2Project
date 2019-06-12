import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StationEditInfoComponent } from './station-edit-info.component';

describe('StationEditInfoComponent', () => {
  let component: StationEditInfoComponent;
  let fixture: ComponentFixture<StationEditInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StationEditInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StationEditInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
