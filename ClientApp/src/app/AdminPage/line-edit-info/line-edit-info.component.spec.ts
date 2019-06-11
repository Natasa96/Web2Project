import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LineEditInfoComponent } from './line-edit-info.component';

describe('LineEditInfoComponent', () => {
  let component: LineEditInfoComponent;
  let fixture: ComponentFixture<LineEditInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LineEditInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LineEditInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
