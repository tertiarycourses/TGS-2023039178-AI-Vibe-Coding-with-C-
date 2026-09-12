# Activity 08 specification

A warehouse imports synthetic product data using a documented JSON contract. Reject mismatched types and negative stock, preserve UTF-8 names and prove export/import round-trip interoperability.

## Business rules

- A valid record survives JSON export/import unchanged.
- Quantity is a JSON integer, not text; stock cannot be negative.
- SKU uses exactly SKU- plus three digits.
- UTF-8 names and zero stock are preserved.

## Extension work

- Create a contract table with field, type, required value, valid example and failure.
- Add missing-field and JSON-null tests and document serializer defaults.
- Add a schema-version field and explain backward compatibility.
- Explain why using closes StreamWriter deterministically and a finalizer is unsuitable.
