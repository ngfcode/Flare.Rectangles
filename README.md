
# Flare Rectangles

This is an service to create a grid and place rectangles within its boundaries.
It provides different functionalities such as the following:
- Create a grid
- Place a rectangle
- Find a rectangle
- List all rectangles
- Delete a rectangle

There are there major projects within the solution
1. Flare.Rectangles.Application
    - This contains the object models and contract for the service. Uses FluentValidation for validating inputs.
2. Flare.Rectangles.Infrastructure
    - This contains the actual implementation for the IGridService
    - The data is stored as an static object to retain its value, another option is to use a data source and create entities
3. Flare.Rectangles.WebApi (Start Up Project)
    - A REST Api application that provides end-points

## Author

- [Noel G. Francisco](https://www.github.com/ngfcode)