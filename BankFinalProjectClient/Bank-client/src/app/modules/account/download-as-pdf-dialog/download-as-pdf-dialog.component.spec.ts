import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DownloadAsPdfDialogComponent } from './download-as-pdf-dialog.component';

describe('DownloadAsPdfDialogComponent', () => {
  let component: DownloadAsPdfDialogComponent;
  let fixture: ComponentFixture<DownloadAsPdfDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DownloadAsPdfDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DownloadAsPdfDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
