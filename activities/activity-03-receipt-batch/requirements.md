# Activity 03 specification

A receipt calculator prices two items and totals a grade batch. Currency uses decimal, quantities use int, discounts are bounded, and invalid external text is parsed safely.

## Business rules

- 19.90 × 2 × 0.90 produces 35.82.
- Malformed quantities are rejected with TryParse.
- Grade 90 is A and 89 is B.
- Negative quantities fail; discount remains between 0 and 1.

## Extension work

- Add 59/60,69/70,79/80 and89/90 grade boundary checks.
- Use a for loop to print array indexes; compare with foreach.
- Add an empty grade list guard so division by zero cannot occur.
- Demonstrate integer division 5/2 versus decimal division 5m/2.
