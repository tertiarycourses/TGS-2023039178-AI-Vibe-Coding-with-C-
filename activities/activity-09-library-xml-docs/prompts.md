# Activity 09 AI review prompts

## Design prompt

Act as a C# reviewer. Work only in this .NET 10 console project. Read the synthetic specification: A council library requires book search, member registration, loan processing and a fine of 0.50 per overdue calendar day. Produce XML comments, dependency notes and an explicit requirement-to-method-to-test map. Identify the input/output contract, invariants and error path. Return a short design and three test cases before proposing code. No new dependencies, network calls or file deletion.

## Review prompt

Review my Program.cs against requirements.md. Explain each defect using a concrete input and expected versus actual behaviour. Return the smallest C# diff and tests. Do not invent successful test results. If uncertain about an API, point to current Microsoft documentation.

## Evidence prompt

Using only my supplied actual build/run output, help me summarize one requirement-to-code-to-test link. Label any assumption. I must explain and approve the change myself.
