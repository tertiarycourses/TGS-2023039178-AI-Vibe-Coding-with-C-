# Activity 04 specification

A bank requires console MVC: Model owns a non-negative balance, View formats output, Controller coordinates. Public members use PascalCase and local/private variables camelCase.

## Business rules

- The Model rejects negative initial balance.
- View owns formatting; Controller holds no account rule.
- Factorial(0) is 1 and Factorial(5) is 120.
- Public method/property names follow PascalCase.

## Extension work

- Add nullable account lookup and a view error message without moving business rules into the View.
- Replace recursion with iteration and compare the same boundary cases.
- Draw a sequence diagram for Controller → Model → View.
