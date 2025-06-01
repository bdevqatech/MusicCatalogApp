import { TestBed } from '@angular/core/testing';

import { RecordLabelService } from './record-label.service';

describe('RecordLabelService', () => {
  let service: RecordLabelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecordLabelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
