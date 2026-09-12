# Activity 02 AI review prompts

## Design prompt

Act as a C# reviewer. Work only in this .NET 10 console project. Read the synthetic specification: A greeter formats a warehouse welcome message. AI-generated text is a candidate change; compile it, test null/blank input and explain the result before accepting it. Identify the input/output contract, invariants and error path. Return a short design and three test cases before proposing code. No new dependencies, network calls or file deletion.

## Review prompt

Review my Program.cs against requirements.md. Explain each defect using a concrete input and expected versus actual behaviour. Return the smallest C# diff and tests. Do not invent successful test results. If uncertain about an API, point to current Microsoft documentation.

## Evidence prompt

Using only my supplied actual build/run output, help me summarize one requirement-to-code-to-test link. Label any assumption. I must explain and approve the change myself.
