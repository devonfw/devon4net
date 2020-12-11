import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { DishesDataService } from '../../backend/dishes/dishes-data-service';
import { Filter } from '../../backend/backendModels/interfaces';
import { DishView, ExtraView, OrderView } from '../../shared/viewModels/interfaces';
import { map, remove, assign, merge } from 'lodash';

@Injectable()
export class MenuService {

  constructor(private dishesDataService: DishesDataService) {}

  menuToOrder(menu: DishView): OrderView {
    let order: OrderView;
    order = assign(order, menu);
    order.orderLine = {
        amount: 1,
        comment: '',
    };
    return order;
  }

  composeFilters(filters: any, sortDir: string): Filter {
    let filtersComposed: Filter;
    let categories: any = [];
    if (filters) {
      map(filters, (value: boolean, field: string) => {
        if (value === true) {
          categories.push({id: field});
        }
      });

      filtersComposed = {
        categories: categories,
        searchBy: filters.searchBy,
        sort: [{
          name: filters.sortName,
          direction: sortDir,
        }],
        maxPrice: filters.maxPrice,
        minLikes: filters.minLikes,
        isFav: filters.isFav,
      };
    } else {
      filtersComposed = {
        searchBy: undefined,
        sort: [],
        maxPrice: undefined,
        minLikes: undefined,
        isFav: undefined,
        categories: categories,
      };
    }
    return  filtersComposed;
  }

  clearSelectedExtras(menuInfo: DishView): void {
    map(menuInfo.extras, (extra: ExtraView) => { extra.selected = false; });
  }

  getDishes(filters: any): Observable<DishView[]> {
    return this.dishesDataService.filter(filters);
  }
}
