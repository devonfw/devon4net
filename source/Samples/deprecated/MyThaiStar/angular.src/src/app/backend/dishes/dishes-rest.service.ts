import { environment } from './../../../environments/environment';
import { Filter } from '../backendModels/interfaces';
import { Injectable, Injector } from '@angular/core';
import { Response, Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { IDishesDataService } from './dishes-data-service-interface';
import { config } from '../../config';
import { DishResponse, DishView } from '../../shared/viewModels/interfaces';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DishesRestService implements IDishesDataService {

  private readonly filtersRestPath: string = 'dishmanagement/v1/dish/search';

  results: string[];

  constructor(private http: HttpClient) {
  }

  filter(filters: Filter): Observable<DishView[]> {
    // Returns DishResponse
    return this.http.post<DishResponse>(`${environment.restServiceRoot}${this.filtersRestPath}`, filters)
      .map((res: any) => res.result);
  }

}
