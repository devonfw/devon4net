import { Injector, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BackendType, BackendConfig } from './../../../app/config';
import { DishesGraphQlService } from './dishes-graph-ql.service';
import { DishesInMemoryService } from './dishes-in-memory.service';
import { DishesRestService } from './dishes-rest.service';
import { Filter } from '../backendModels/interfaces';
import { IDishesDataService } from './dishes-data-service-interface';
import { DishView } from '../../shared/viewModels/interfaces';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DishesDataService implements IDishesDataService {

    private usedImplementation: IDishesDataService;

    constructor(private injector: Injector, private http: HttpClient) {
        const backendConfig: BackendConfig = this.injector.get(BackendConfig);
        if (backendConfig.environmentType === BackendType.IN_MEMORY) {
            this.usedImplementation = new DishesInMemoryService();
        } else if (backendConfig.environmentType === BackendType.GRAPHQL) {
            this.usedImplementation = new DishesGraphQlService(this.injector);
        } else { // default
            this.usedImplementation = new DishesRestService(http);
        }
    }

    filter(filters: Filter): Observable<DishView[]> {
        return this.usedImplementation.filter(filters);
    }

}
