# Dotnetsvcs
## dotnet services

SOLID stack to create CRUD services:

* DbContext Wrapper: mockable dbcontext with essential operations
* Facade layer
* Service layer: Operations should implement IDbOpCreate, IDbOpDelete, .... and inherit from DbOpCreate, DbOpDelete, ...
* Parms DTO: parameters to service
* Data DTO: data from service
* Projection: Model to Data DTO
* Visibility: Where expression generator. What is visible out of service
* Pre and Post conditions
* Criterias: Helpers to generate Where clause from Criteria DTOs

```
Blazor --------------> Facade ------------------------> Service -------------> models
         Parm DTO                Parm Dto                
                                 DbContextWrapper
                                 Projection

Blazor <-------------- Facade <------------------------ Service <------------- models
         DTO Result                Dto Data                
```

#### To full understand:
* Check [SampleApp](.SampleApp)
* Check `Do` method of each [DbOperation](./src/Dotnetsvcs.Svc)

#### Why so many Interfaces?

* You can test elements at any level (Ex: you can mock pre and post conditions to test a service. Ex: you can mock a whole facade to test ui)
* You can Mock elements easyly:
   * No need to call construnctors.
   * Override all public members.


#### State of art

* In development.


#### Contributions

* All contributions are welcome. Plase, create an issue explaining your future contribution and link issue on PR.

MIT License
