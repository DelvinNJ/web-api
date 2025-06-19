# Unit Testing

- Create new project under the same solution explore : xUnit Test Project
- Add project reference
    
```bash
    - Goto dependencies
    - Add project reference
```
- Install packages
```
    - FakeItEasy
    - Moq
```

# Api version

- Install packages
``` 
    - Asp.Versioning.Mvc
    - Asp.Versioning.Mvc.ApiExplorer
```
- Setup the code
```
- Add builder.Services.AddApiVersioning
- Add builder.Services.AddTransient
- Update app.UseSwaggerUI 
```