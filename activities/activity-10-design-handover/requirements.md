# Activity 10 specification

A warehouse requires a reusable stock service that prevents negative stock, reports changes and can be traced to R-STOCK-01. Deliver a design decision, dependency inventory, public API comments and executable evidence.

## Business rules

- 10 − 3 + 2 produces stock 9.
- Overselling is rejected without changing existing stock.
- Negative and overflow mutations are rejected.
- Handover states the contract, requirement trace, dependencies and limitations.

## Extension work

- Move StockService into a documented class library and add a project reference; rerun the same tests.
- Draw a class and sequence diagram and map R-STOCK-01 to the public API.
- Create a decision record comparing in-memory state and persistence, with tradeoffs and a future migration test.
- Archive your actual test outputs and one reviewed change in the handover.
