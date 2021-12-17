import { TestBed } from '@angular/core/testing';

import { EditEventsService } from './edit-events.service';

describe('EditEventsService', () => {
  let service: EditEventsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditEventsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
