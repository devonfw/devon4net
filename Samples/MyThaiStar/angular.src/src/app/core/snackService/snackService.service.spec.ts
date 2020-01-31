import { CoreModule } from '../../core/core.module';
import { TestBed, inject } from '@angular/core/testing';
import { SnackBarService } from './snackService.service';

describe('SnackBarService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
        imports: [CoreModule],
        providers: [SnackBarService],
    });
  });

  it('should create', inject([SnackBarService], (service: SnackBarService) => {
    expect(service).toBeTruthy();
  }));
});
