# Activity 06 specification

A school needs extensible grade classification. A base Student owns validated grades; GradedStudent and HonoursStudent implement a shared IGradable contract and demonstrate virtual dispatch.

## Business rules

- All grades are within 0–100.
- Empty grade collection fails with a meaningful error.
- The same mean 85 is B for standard and A for honours.
- Both students are processed through IGradable.

## Extension work

- Add 89/90 and84/85 boundary cases for the two policies.
- Draw an editable UML class diagram with one base class and the interface contract.
- List suitable GUI controls (numeric grade input, student selector, results grid) and map each to its property/method; the supplied console app is the portable implementation.
