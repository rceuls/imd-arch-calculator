# Pocket Calculator

## Use Case

Create a new console app that contains the following functionality:

- Add, subtract, divide, multiply two (or more) numbers.
- When dividing, the inputs can only contain non-zero numbers
- Input is parsed via the CLI (asked for by the user). Data is separated by spaces.
  - The first one is the action, the rest of them are the numbers.
  - The numbers should be parseable doubles.
  - The action is either `+`, `-`, `/` or `*`.
  - Non-covered cases give a user friendly error.

Example valid input: `+ 1 2 3`, `- 1 2 3`.

## Program structure

- There are remarks in the code, read them (start in `Program.cs`).
- Most classes are static. This is because they don't contain state and only contain [Pure Functions](https://en.wikipedia.org/wiki/Pure_function).
- The non-static classes (and helper enums) are contained in the DTO folder. These are simple [Value objects](https://en.wikipedia.org/wiki/Value_object), containing some state. These are the only objects that should be passed between function calls. This is not 100% true as input needs to be parsed (from a primitive - a string, to a more complex object).
- A function has two return types, always: either the expected result or an error.
  - those two results are wrapped in a tuple (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples) with the error on the left (first element) and the right the happy result (second element).