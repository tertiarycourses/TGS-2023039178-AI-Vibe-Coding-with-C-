# Activity 10 — Documented stock component and handover evidence

AI Vibe Coding with C# · TGS-2023039178 · v7.0 · 13 September 2026

**Alignment:** LO5 · K5 · A5

**Suggested time:** 45 minutes.

A warehouse requires a reusable stock service that prevents negative stock, reports changes and can be traced to R-STOCK-01. Deliver a design decision, dependency inventory, public API comments and executable evidence.

## Prerequisites

Install the .NET 10 SDK for your Windows/macOS/Linux machine and VS Code. C# Dev Kit is optional for debugging. Use `dotnet --list-sdks` to confirm a 10.x SDK. All sample data are synthetic. An AI subscription is optional; every activity works without AI and without API keys. The classroom programme includes guided demonstrations before this independent task.

## Files

- `Activity.csproj`: compile settings.
- `Program.cs`: runnable teaching example and acceptance checks.
- `requirements.md`: business specification and extension tasks.
- `prompts.md`: bounded AI review prompts.
- `expected-output.txt`: baseline demonstration output.
- `evidence.md`: learner evidence checklist.

## Detailed procedure

1. Open this activity folder in VS Code. Read requirements.md and identify the input, output, invariant and error path before editing.

2. Open a terminal in this folder and verify an SDK is installed. An SDK is required; a runtime alone cannot compile the project.

```sh
dotnet --list-sdks
```

3. Read Activity.csproj. Confirm net10.0, nullable reference checking and warnings-as-errors. Read Program.cs and locate the business rule named in the requirements.

4. Build the supplied project and resolve every compiler error before running it.

```sh
dotnet build Activity.csproj
```

5. Run the deterministic demonstration. Compare each line to expected-output.txt; record the actual output in your evidence checklist.

```sh
dotnet run --project Activity.csproj
```

6. Run the in-program acceptance checks. Read the checks in Program.cs; they call real production methods and must finish without an exception.

```sh
dotnet run --project Activity.csproj -- --self-test
```

7. Make a baseline copy of Program.cs outside this activity, then complete the extension tasks below. Explain each change in your own words; keep the code runnable.

8. Ask an approved AI assistant for a review using prompts.md, or perform the same review manually. Share only the synthetic sample, specification and error excerpt. Inspect the proposed diff before applying it.

9. Rebuild, rerun the normal/invalid/boundary tests and verify that the change preserves the invariant. If it fails, record the failing input, expected/actual output and smallest repair.

```sh
dotnet build Activity.csproj
dotnet run --project Activity.csproj -- --self-test
```

10. Complete the evidence record with source file names, actual outputs, one failure/repair, the requirement mapping and a short explanation of why the chosen controls fit.

## Extension tasks

- Move StockService into a documented class library and add a project reference; rerun the same tests.
- Draw a class and sequence diagram and map R-STOCK-01 to the public API.
- Create a decision record comparing in-memory state and persistence, with tradeoffs and a future migration test.
- Archive your actual test outputs and one reviewed change in the handover.

## Acceptance criteria

- 10 − 3 + 2 produces stock 9.
- Overselling is rejected without changing existing stock.
- Negative and overflow mutations are rejected.
- Handover states the contract, requirement trace, dependencies and limitations.

## Expected baseline output

```text
Stock after remove/add: 9
Handover: output/handover.txt
```

## Troubleshooting

- `NETSDK1045`: install/select .NET 10 SDK; do not silently retarget the project.
- `CS...`: read the first compiler error, fix the referenced line and rebuild. Nullable warnings identify a missing null check.
- Wrong folder/project: open the terminal beside `Activity.csproj` and use the explicit `--project` option.
- Failed assertion: compare the test input, expected value and production rule before changing either.
- A generated file cannot be written: check the current folder permissions; use the local `output/` folder and close open file handles.
- AI proposes C++ syntax, a new package or destructive file operations: reject that portion and request a C#/.NET-only diff within the existing project.

## References

[Microsoft C# documentation](https://learn.microsoft.com/en-us/dotnet/csharp/) · [Microsoft beginner training](https://learn.microsoft.com/en-us/training/paths/get-started-c-sharp-part-1/)
