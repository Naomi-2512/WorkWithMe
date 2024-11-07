import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerLogoutComponent } from './manager-logout.component';

describe('ManagerLogoutComponent', () => {
  let component: ManagerLogoutComponent;
  let fixture: ComponentFixture<ManagerLogoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManagerLogoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManagerLogoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
