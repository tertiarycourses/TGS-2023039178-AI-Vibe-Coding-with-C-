# Activity 01 specification

A store needs a catalogue for Electronics, Clothing and Food. Reject blank names, prices at or below zero, negative stock and unknown categories. Define each requirement before generating a component.

## Business rules

- R1: blank names fail without creating a record.
- R2: price must be strictly positive; stock zero is valid.
- R3: only three named categories are accepted.
- Self-test reports PASS: 5 contract checks.

## Extension work

- Write a requirements table with R1–R3, one example and one boundary case each.
- Add a maximum name length rule and show its boundary tests.
- Draw the validator/catalogue/view component boundary and identify who may mutate stock.
