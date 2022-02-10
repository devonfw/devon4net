import { CoreModule } from '../../core/core.module';
import { TestBed, inject } from '@angular/core/testing';
import { HttpRequestInterceptorService } from './httpRequestInterceptor.service';

describe('HttpRequestInterceptorService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [CoreModule],
            providers: [HttpRequestInterceptorService],
        });
    });

    it('should create', inject([HttpRequestInterceptorService], (service: HttpRequestInterceptorService) => {
        expect(service).toBeTruthy();
    }));
});
