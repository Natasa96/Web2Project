import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricelistEditComponent } from './pricelist-edit.component';

describe('PricelistEditComponent', () => {
  let component: PricelistEditComponent;
  let fixture: ComponentFixture<PricelistEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricelistEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PricelistEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
