import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccoutHolderInfoComponent } from './accout-holder-info.component';

describe('AccoutHolderInfoComponent', () => {
  let component: AccoutHolderInfoComponent;
  let fixture: ComponentFixture<AccoutHolderInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccoutHolderInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccoutHolderInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
