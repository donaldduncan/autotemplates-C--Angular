import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BotSetupComponent } from './bot-setup.component';

describe('BotSetupComponent', () => {
  let component: BotSetupComponent;
  let fixture: ComponentFixture<BotSetupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BotSetupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BotSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
