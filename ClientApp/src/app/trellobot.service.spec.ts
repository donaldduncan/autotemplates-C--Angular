import { TestBed } from '@angular/core/testing';

import { TrellobotService } from './trellobot.service';

describe('TrellobotService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TrellobotService = TestBed.get(TrellobotService);
    expect(service).toBeTruthy();
  });
});
