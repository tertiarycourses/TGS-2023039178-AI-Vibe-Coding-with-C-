# Activity 07 specification

A food-delivery processor must never confirm a failed payment or notify a cancelled order. Mock services exercise success, rejection and thrown exceptions; status transitions are written to a local log.

## Business rules

- Success confirms and notifies exactly once in a single call.
- Rejected or thrown payments fail without notification.
- Cancelled orders bypass payment and notification.
- Log contains the order ID and failure path.

## Extension work

- Read the local event log and build a state transition table.
- Add an invalid-state test directly against NotificationService.Send.
- State a limitation: repeat calls are not idempotent. Propose an order-ID deduplication design and its tests without claiming it exists.
