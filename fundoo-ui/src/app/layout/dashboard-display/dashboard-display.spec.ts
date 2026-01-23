import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardDisplay } from './dashboard-display';

describe('DashboardDisplay', () => {
  let component: DashboardDisplay;
  let fixture: ComponentFixture<DashboardDisplay>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DashboardDisplay]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardDisplay);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
