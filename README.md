# AlphaDiamond

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)
[![C# 14](https://img.shields.io/badge/C%23-14.0-blue)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![NUnit](https://img.shields.io/badge/tests-NUnit%204-green)](https://nunit.org/)
[![Coverage](https://img.shields.io/badge/coverage-97.5%25-brightgreen)]()
[![BenchmarkDotNet](https://img.shields.io/badge/benchmarks-BenchmarkDotNet-blue)](https://benchmarkdotnet.org/)

A **.NET 10** solution for generating text-based **diamond patterns** from any ordered alphabet. Given a target letter and an alphabet, it produces a symmetrical diamond shape where the first symbol appears at the top and bottom vertices and the target letter appears at the widest horizontal row. Supports custom alphabets (uppercase, lowercase, numeric, mixed symbols), configurable whitespace visualization, and dependency injection via `Microsoft.Extensions.DependencyInjection`.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
- [Core Library — DiamondCreatorLib](#core-library--diamondcreatorlib)
  - [IDiamondCreatorService](#idiamondcreatorservice)
  - [DiamondCreatorService](#diamondcreatorservice)
  - [DiamondCreatorException](#diamondcreatorexception)
- [Diamond Algorithm](#diamond-algorithm)
- [Applications](#applications)
  - [Diamond (Console)](#diamond-console)
  - [DiamondCreatorBenchmark](#diamondcreatorbenchmark)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Build](#build)
  - [Run the Console Application](#run-the-console-application)
- [Testing](#testing)
  - [Test Coverage by Category](#test-coverage-by-category)
  - [Test Categories](#test-categories)
- [Benchmarks](#benchmarks)
- [License](#license)

## Overview

The **Diamond Kata** is a classic programming exercise: given a letter from an alphabet, produce a diamond shape with that letter at its widest point. **AlphaDiamond** extends this concept beyond the English alphabet — it supports any ordered character set including lowercase letters, digits, and mixed symbols.

**Example** — target letter `D` with the default uppercase English alphabet (A–Z):

```
_ _ _ A _ _ _
_ _ B _ B _ _
_ C _ _ _ C _
D _ _ _ _ _ D
_ C _ _ _ C _
_ _ B _ B _ _
_ _ _ A _ _ _
```

The diamond is rendered as a square matrix where each character is separated by a space. Whitespace positions are filled with a configurable visualizer character (default `_`).

## Features

- **Custom alphabet support** — define alphabets from char ranges (`'A'` to `'Z'`), strings, or any ordered character set
- **Automatic deduplication and sorting** — string-based alphabet definitions remove duplicates, strip whitespace, and sort by ordinal value
- **Configurable whitespace visualizer** — replace the default `_` with any character (`*`, `.`, ` `, etc.)
- **Dependency injection** — `IDiamondCreatorService` / `DiamondCreatorService` registered via `Microsoft.Extensions.DependencyInjection`
- **Custom exception type** — `DiamondCreatorException` with resource-based error message formatting
- **Resource-based strings** — error messages and alphabet constants stored in `.resx` files for maintainability
- **60 unit tests** with NUnit 4 covering alphabet definition, diamond creation, exception handling, and structural properties
- **97.5% line coverage** measured with [Coverlet](https://github.com/coverlet-coverage/coverlet)
- **BenchmarkDotNet integration** for performance measurement across initialization, alphabet definition, and diamond creation
- **Full XML documentation** for IntelliSense support

## Project Structure

```
AlphaDiamond/
├── DiamondCreatorLib/                       # Core diamond creation library
│   ├── Interfaces/
│   │   └── IDiamondCreatorService.cs        # Service interface
│   ├── Utils/
│   │   └── DiamondCreatorException.cs       # Custom exception type
│   ├── Properties/
│   │   ├── Resources.resx                   # Resource strings (error patterns, alphabet)
│   │   └── Resources.Designer.cs            # Auto-generated resource accessor
│   └── DiamondCreatorService.cs             # Diamond creation implementation
├── Diamond/                                 # Console application
│   ├── Program.cs                           # Entry point with DI setup
│   └── Properties/
│       └── launchSettings.json              # Default launch arguments
├── DiamondCreatorBenchmark/                 # BenchmarkDotNet performance suite
│   ├── BenchmarkDiamondCreator.cs           # Benchmark definitions
│   └── Program.cs                           # Benchmark runner
├── DiamondCreatorLib.Tests/                 # Unit tests
│   └── DiamondCreatorServiceTests.cs        # 30 tests — service behavior
├── LICENSE.txt
└── AlphaDiamond.sln
```

## Core Library — DiamondCreatorLib

### IDiamondCreatorService

Defines the contract for diamond creation services.

| Member | Type | Description |
|---|---|---|
| `Alphabet` | `string` | The ordered set of symbols used for diamond generation |
| `TargetLetter` | `char` | The letter at the widest horizontal point of the diamond |
| `WhitespaceVisualizer` | `char` | Character used to visualize whitespace positions (default `_`) |
| `DefineAlphabet(char, char)` | `void` | Defines an alphabet from a first and last symbol (inclusive range) |
| `DefineAlphabet(string)` | `void` | Defines an alphabet from a string (deduplicated, sorted, whitespace-stripped) |
| `CreateDiamond(char)` | `string` | Creates a diamond pattern for the given target letter |

### DiamondCreatorService

Implementation of `IDiamondCreatorService`. The default alphabet is the uppercase English alphabet (`A`–`Z`).

**Alphabet definition — char range:**

```csharp
var service = new DiamondCreatorService();
service.DefineAlphabet('A', 'Z'); // "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
service.DefineAlphabet('1', '9'); // "123456789"
service.DefineAlphabet('c', 'g'); // "cdefg"
```

**Alphabet definition — string (auto-sorted, deduplicated, whitespace-stripped):**

```csharp
service.DefineAlphabet("ZYXWVUTSRQPONMLKJIHGFEDCBA");
// Result: "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

service.DefineAlphabet("qqWWWeRRRRRt112Yuu2uuiOP[O}OP\\[qP");
// Result: "12OPRWY[\\eiqtu}"
```

**Diamond creation:**

```csharp
service.DefineAlphabet('A', 'Z');
string diamond = service.CreateDiamond('C');
// _ _ A _ _
// _ B _ B _
// C _ _ _ C
// _ B _ B _
// _ _ A _ _
```

**Custom whitespace visualizer:**

```csharp
service.WhitespaceVisualizer = '*';
string diamond = service.CreateDiamond('B');
// * A *
// B * B
// * A *
```

### DiamondCreatorException

Custom exception type for diamond creation errors. Implements `ISerializable` and formats error messages using resource string patterns.

Thrown when:
- Alphabet is defined with reversed char range (e.g., `'Z'` to `'A'`)
- Alphabet string is null, empty, or whitespace-only
- Target letter is outside the defined alphabet
- Service fails to initialize

```csharp
// Throws DiamondCreatorException:
service.DefineAlphabet('Z', 'A');           // Reversed range
service.DefineAlphabet("");                 // Empty string
service.CreateDiamond('b');                 // Lowercase 'b' not in uppercase alphabet
```

## Diamond Algorithm

The diamond is constructed as a square matrix of size `N × N`, where `N = 2 × position - 1` and `position` is the 1-based index of the target letter in the alphabet.

```
Target Letter → Calculate Matrix Size (N)
             → For each row (1..N):
                 → Determine letter index (ascending to midpoint, then descending)
                 → Calculate leading whitespace count
                 → Calculate in-between whitespace count
                 → Build row string
             → Join rows with newlines
```

**Row construction rules:**

| Row Type | Pattern | Example (N=5, alphabet A–C) |
|---|---|---|
| **First/Last letter** (top/bottom vertex) | `{leading}{letter}{leading}` | `_ _ A _ _` |
| **Target letter** (horizontal vertices) | `{letter}{between}{letter}` | `C _ _ _ C` |
| **Middle letters** | `{leading}{letter}{between}{letter}{leading}` | `_ B _ B _` |

Each character in the output row is separated by a space for visual clarity.

**Visual example** — target `D`, alphabet `A`–`Z`:

```
Row 1: letterIndex=0 (A)  →  _ _ _ A _ _ _     (leading=3)
Row 2: letterIndex=1 (B)  →  _ _ B _ B _ _     (leading=2, between=1)
Row 3: letterIndex=2 (C)  →  _ C _ _ _ C _     (leading=1, between=3)
Row 4: letterIndex=3 (D)  →  D _ _ _ _ _ D     (leading=0, between=5)
Row 5: letterIndex=2 (C)  →  _ C _ _ _ C _     (mirror of row 3)
Row 6: letterIndex=1 (B)  →  _ _ B _ B _ _     (mirror of row 2)
Row 7: letterIndex=0 (A)  →  _ _ _ A _ _ _     (mirror of row 1)
```

## Applications

### Diamond (Console)

Interactive console application that accepts a single letter as a command-line argument, creates the diamond using the uppercase English alphabet, and prints it to the console.

```
> Diamond.exe K

_ _ _ _ _ _ _ _ _ _ A _ _ _ _ _ _ _ _ _ _
_ _ _ _ _ _ _ _ _ B _ B _ _ _ _ _ _ _ _ _
_ _ _ _ _ _ _ _ C _ _ _ C _ _ _ _ _ _ _ _
_ _ _ _ _ _ _ D _ _ _ _ _ D _ _ _ _ _ _ _
_ _ _ _ _ _ E _ _ _ _ _ _ _ E _ _ _ _ _ _
_ _ _ _ _ F _ _ _ _ _ _ _ _ _ F _ _ _ _ _
_ _ _ _ G _ _ _ _ _ _ _ _ _ _ _ G _ _ _ _
_ _ _ H _ _ _ _ _ _ _ _ _ _ _ _ _ H _ _ _
_ _ I _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ I _ _
_ J _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ J _
K _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ K
_ J _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ J _
_ _ I _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ I _ _
_ _ _ H _ _ _ _ _ _ _ _ _ _ _ _ _ H _ _ _
_ _ _ _ G _ _ _ _ _ _ _ _ _ _ _ G _ _ _ _
_ _ _ _ _ F _ _ _ _ _ _ _ _ _ F _ _ _ _ _
_ _ _ _ _ _ E _ _ _ _ _ _ _ E _ _ _ _ _ _
_ _ _ _ _ _ _ D _ _ _ _ _ D _ _ _ _ _ _ _
_ _ _ _ _ _ _ _ C _ _ _ C _ _ _ _ _ _ _ _
_ _ _ _ _ _ _ _ _ B _ B _ _ _ _ _ _ _ _ _
_ _ _ _ _ _ _ _ _ _ A _ _ _ _ _ _ _ _ _ _
```

The application uses **dependency injection** (`Microsoft.Extensions.DependencyInjection`) to resolve `IDiamondCreatorService`. Input is automatically converted to uppercase. Invalid input (multiple characters, missing arguments, letters outside the alphabet) produces a descriptive error message.

### DiamondCreatorBenchmark

BenchmarkDotNet suite measuring performance across the diamond creation pipeline:

| Benchmark | Description |
|---|---|
| `InitializeDiamondCreator` | Creates a new `DiamondCreatorService` instance |
| `DefineAlphabetFromString` | Defines alphabet from a full string (dedup + sort) |
| `DefineAlphabetFromFirstAndLastSymbols` | Defines alphabet from char range (A–Z) |
| `CreateDiamondBenchmark_A` | Creates a 1×1 diamond (first letter — smallest) |
| `CreateDiamondBenchmark_L` | Creates a 23×23 diamond (12th letter — medium) |
| `CreateDiamondBenchmark_Z` | Creates a 51×51 diamond (26th letter — largest) |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Build

```shell
dotnet build AlphaDiamond.sln
```

### Run the Console Application

```shell
dotnet run --project Diamond -- K
```

Where `K` is any single letter from the English alphabet (A–Z). The letter is case-insensitive.

## Testing

The solution includes **60 unit tests** written with **NUnit 4**, covering `DiamondCreatorService` behavior and `DiamondCreatorException` construction.

```shell
dotnet test
```

### Code Coverage

Code coverage is collected with [Coverlet](https://github.com/coverlet-coverage/coverlet) (via `coverlet.msbuild`). Auto-generated resource accessors are excluded.

```shell
dotnet test DiamondCreatorLib.Tests/DiamondCreatorLib.Tests.csproj \
  /p:CollectCoverage=true \
  /p:CoverletOutputFormat=cobertura \
  /p:Include="[DiamondCreatorLib]*" \
  /p:ExcludeByAttribute="GeneratedCodeAttribute%2cCompilerGeneratedAttribute" \
  /p:CoverletOutput=./TestResults/coverage.cobertura.xml
```

#### Summary

| Module | Line | Branch | Method |
|---|---|---|---|
| **DiamondCreatorLib** | **97.5%** | **95.4%** | **100%** |

#### Per-Class Breakdown

| Class | Line | Branch | Uncovered Lines |
|---|---|---|---|
| `DiamondCreatorService` | 96.8% | 95.4% | Defensive `Alphabet is null` guard (unreachable) |
| `DiamondCreatorException` | 100% | 100% | — |

### Test Coverage by Category

| Test Group | Tests | Target |
|---|---|---|
| `DefineAlphabetByFirstAndLastLettersTest` | 12 | Alphabet definition via char range: uppercase, lowercase, numeric, reverse order, same values, cross-case |
| `DefineAlphabetByStringTest` | 10 | Alphabet definition via string: ordering, deduplication, whitespace stripping, null/empty/whitespace-only |
| `CreateDiamondTest` | 24 | Diamond creation: full output, row count, symmetry, uniform width, widest row, custom visualizers, gap alphabets, boundary targets, numeric alphabets, property state |
| `DefaultStateTest` | 3 | Default property values: alphabet, whitespace visualizer, target letter |
| `DiamondCreatorExceptionTest` | 11 | All constructors (message, format+args, inner exception, serialization), resource pattern, exception details from service operations |
| | | |
| **Total** | **60 tests** | |

### Test Categories

| Category | Description | Examples |
|---|---|---|
| **Unit** | Individual method behavior | Alphabet construction, diamond string output, exception construction |
| **Structural** | Diamond geometric properties | Row count, vertical symmetry, uniform row width, widest row identification |
| **Edge Cases** | Boundary conditions and error handling | Single-letter alphabet, reversed range, null/empty input, cross-case ranges, gap alphabets |
| **Validation** | Input rejection and exception throwing | Invalid letters, out-of-alphabet targets, whitespace-only strings, target before/after range |
| **Serialization** | Legacy serialization support | Serialization constructor via reflection, `ISerializable.GetObjectData` |

## Benchmarks

Run benchmarks with:

```shell
dotnet run --project DiamondCreatorBenchmark -c Release
```

The benchmark suite uses **BenchmarkDotNet** with `MemoryDiagnoser` to measure allocation and throughput across initialization, alphabet definition, and diamond creation for various sizes (1×1 to 51×51 matrices).

## License

This project is licensed under the [MIT License](LICENSE.txt).

## Author

**TigoS** — [GitHub](https://github.com/TigoS)
