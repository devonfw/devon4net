import { filter } from 'rxjs/operator/filter';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { IDishesDataService } from './dishes-data-service-interface';
import { Filter } from '../backendModels/interfaces';
import { dishes } from '../mock-data';
import { orderBy, find } from 'lodash';
import { DishView } from '../../shared/viewModels/interfaces';

@Injectable()
export class DishesInMemoryService implements IDishesDataService {

  filter( filters: Filter): Observable <DishView[]> {
    if (!filters.sort[0]) {
      filters.sort.push({ name:  '', direction: ''});
    }
    return Observable.of(orderBy(dishes, [filters.sort[0].name], [filters.sort[0].direction])
                          .filter((plate: DishView) => {
                            if (filters.searchBy) {
                              return plate.dish.name.toLowerCase().includes(filters.searchBy.toLowerCase());
                            } else {
                              return true;
                            }
                          }).filter((plate: DishView) => {
                            if (filters.maxPrice) {
                              return plate.dish.price < filters.maxPrice;
                            } else {
                              return true;
                            }
                          }).filter((plate: DishView) => {
                            if (filters.minLikes) {
                              return plate.likes > filters.minLikes;
                            } else {
                              return true;
                            }
                          }).filter( (plate: DishView) => {
                            if (filters.categories) {
                              return filters.categories.every( (category: {id: string}) => {
                                return find(plate.categories, category);
                              });
                            } else {
                              return true;
                            }
                          }));
  }

}
