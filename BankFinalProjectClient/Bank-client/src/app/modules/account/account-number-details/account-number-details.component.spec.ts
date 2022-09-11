import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountNumberDetailsComponent } from './account-number-details.component';

describe('AccountNumberDetailsComponent', () => {
  let component: AccountNumberDetailsComponent;
  let fixture: ComponentFixture<AccountNumberDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountNumberDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountNumberDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
