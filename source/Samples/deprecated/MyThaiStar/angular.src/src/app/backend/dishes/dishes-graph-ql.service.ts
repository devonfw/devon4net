import { Filter } from '../backendModels/interfaces';
import { ApolloQueryResult } from 'apollo-client';
import { IDishesDataService } from './dishes-data-service-interface';
import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { DishView } from '../../shared/viewModels/interfaces';

import { Apollo } from 'apollo-angular';
import gql from 'graphql-tag';

import 'rxjs/add/operator/map';

// We use the gql tag to parse our query string into a query document
const getDishesQuery: any = gql`
  query getDishes {
    dishes {
      name
      description
      image
      likes
      ingredients {
        price
        name
      }
      price
    }
  }
`;

// TODO: This is a temporary type as graphQL backend was developed in parallel to angular app
// and java server implementation, model was not yet established so for now conversion
// will be implemented as a part of this service to expose consistient service API
class GqlDish {
  id: number; // added by Roberto, please revise
  image: string;
  likes: number;
  ingredients: { id: number, name: string, price: number }[];
  description: string;
  name: string;
  price: number;
  categories: [ // added by Roberto, please revise
    { id: string }];
}

class DishesQueryRepsonse {
  dishes: GqlDish[];
}

@Injectable()
export class DishesGraphQlService implements IDishesDataService {

  private apollo: Apollo;

  constructor(private injector: Injector) {
    this.apollo = injector.get(Apollo);
  }

  // added by Roberto, please, revise
  filter(filters: Filter): Observable<DishView[]> {
    return this.apollo.watchQuery<DishesQueryRepsonse>({ query: getDishesQuery })
      .map((result: ApolloQueryResult<DishesQueryRepsonse>) => result.data.dishes)
      .map((dishes: GqlDish[]) => dishes.map(this.convertToBackendDish));
  }

  // TODO: see the comment above
  private convertToBackendDish(dish: GqlDish): DishView {
    return {
      dish: {
        id: dish.id, // added by Roberto, please revise
        description: dish.description,
        name: dish.name,
        price: dish.price,
      },
      isfav: false,
      image: { content: dish.image },
      likes: dish.likes,
      extras: dish.ingredients.map((extra: any) => ({ id: extra.id, name: extra.name, price: extra.price, selected: false })),
      categories: dish.categories, // added by Roberto, please revise
    };
  }
}
