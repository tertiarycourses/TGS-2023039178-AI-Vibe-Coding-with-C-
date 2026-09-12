# Activity 09 specification

A council library requires book search, member registration, loan processing and a fine of 0.50 per overdue calendar day. Produce XML comments, dependency notes and an explicit requirement-to-method-to-test map.

## Business rules

- Book search is case-insensitive.
- Only registered members may loan an available book.
- Three overdue calendar days produce 1.50; early return produces zero.
- Generated report maps R1–R4 to named public methods.

## Extension work

- Build XML documentation and locate bin/Debug/net10.0/Activity.xml.
- Add duplicate loan and duplicate member tests.
- Complete a dependencies table with SDK target, built-in namespaces and no third-party package assumption.
- Add summary/param/returns/exception tags to a new return-book method.
