import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplliedComponent } from './apllied.component';

describe('AplliedComponent', () => {
  let component: AplliedComponent;
  let fixture: ComponentFixture<AplliedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AplliedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AplliedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
