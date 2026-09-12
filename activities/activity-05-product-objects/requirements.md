# Activity 05 specification

Administrators add product records. Product constructors enforce invariants and the catalogue owns its collection. Categories are typed; invalid records cannot enter the catalogue.

## Business rules

- An invalid name, price, stock or category is rejected.
- The collection is exposed read-only and stock changes through a method.
- Three named categories are available.
- Valid add increments the count and displays confirmation.

## Extension work

- Add stock adjustment checks for valid/invalid deltas.
- Implement lookup by exact name using ordinal comparison and return nullable when absent.
- Explain why a private setter alone is not authentication or authorization.
