# Activity 02 specification

A greeter formats a warehouse welcome message. AI-generated text is a candidate change; compile it, test null/blank input and explain the result before accepting it.

## Business rules

- Whitespace and null input produce the learner fallback.
- A non-empty name is trimmed before formatting.
- Program has one entry point and compiles without warnings.
- Self-test reports PASS: 3 greeting checks.

## Extension work

- Ask AI for an explanation of top-level statements, using only this file.
- Deliberately remove the null/blank guard, observe a failure, then restore it.
- Add a name containing Unicode and show that it is preserved.
