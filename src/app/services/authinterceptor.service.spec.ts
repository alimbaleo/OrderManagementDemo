/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AuthinterceptorService } from './authinterceptor.service';

describe('Service: Authinterceptor', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthinterceptorService]
    });
  });

  it('should ...', inject([AuthinterceptorService], (service: AuthinterceptorService) => {
    expect(service).toBeTruthy();
  }));
});
