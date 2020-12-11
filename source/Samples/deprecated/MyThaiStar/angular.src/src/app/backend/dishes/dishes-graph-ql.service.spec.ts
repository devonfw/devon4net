import { TestBed, inject } from '@angular/core/testing';

import { DishesGraphQlService } from './dishes-graph-ql.service';
import { provideClient } from '../graphql-client';
import { ApolloModule } from 'apollo-angular';

describe('DishesGraphQlService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DishesGraphQlService],
      imports: [
        ApolloModule.forRoot(provideClient),
      ],
    });
  });

  it('should be properly created', inject([DishesGraphQlService], (service: DishesGraphQlService) => {
    expect(service).toBeTruthy();
  }));
});
