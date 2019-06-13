import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomePriceListComponent } from './home-price-list.component';

describe('HomePriceListComponent', () => {
  let component: HomePriceListComponent;
  let fixture: ComponentFixture<HomePriceListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomePriceListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomePriceListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
