import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SecondSideAccountInfoComponent } from './second-side-account-info.component';

describe('SecondSideAccountInfoComponent', () => {
  let component: SecondSideAccountInfoComponent;
  let fixture: ComponentFixture<SecondSideAccountInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SecondSideAccountInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SecondSideAccountInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
