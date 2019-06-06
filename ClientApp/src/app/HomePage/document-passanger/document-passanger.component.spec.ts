import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentPassangerComponent } from './document-passanger.component';

describe('DocumentPassangerComponent', () => {
  let component: DocumentPassangerComponent;
  let fixture: ComponentFixture<DocumentPassangerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DocumentPassangerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentPassangerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
