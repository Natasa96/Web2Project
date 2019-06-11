import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserValidateInfoComponent } from './user-validate-info.component';

describe('UserValidateInfoComponent', () => {
  let component: UserValidateInfoComponent;
  let fixture: ComponentFixture<UserValidateInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserValidateInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserValidateInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
