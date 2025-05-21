# Partition Quest - Number Partitioning Game

## Overview

Partition Quest is a console-based educational game that challenges players to find all possible partitions (sum combinations) of numbers according to specific rules, inspired by the Hardy-Ramanujan asymptotic formula for integer partitions.

## Features

- Multiple game modes with different partitioning rules
- Educational math puzzles focusing on number theory concepts
- Console-based interface with clear instructions
- Automatic solution validation
- Built-in test suite for verification

## Game Modes

1. **Basic Partitions**: Find all ways to partition a number using positive integers
2. **Odd Partitions**: Only partitions using odd numbers are valid
3. **Distinct Partitions**: All numbers in the partition must be unique
4. **Fixed Length Partitions**: Partitions must contain exactly N numbers
5. **Excluded Number Partitions**: Partitions cannot contain a specific number
6. **Combination Challenges**: Mix of multiple constraints

## Installation

### Prerequisites
- .NET 8.0 SDK or later

### Steps
1. Clone the repository:
   ```
   git clone https://github.com/dexsper/PartitionQuest.git
   ```
2. Navigate to the project directory:
   ```
   cd PartitionQuest
   ```
3. Run the game:
   ```
   dotnet run --project PartitionQuest
   ```

## Project Structure

```
PartitionQuest/
├── Program.cs                // Entry point, main menu
├── GameManager.cs           // Level management and game logic
├── Puzzle.cs                // Puzzle conditions and validation
├── PartitionGenerator.cs    // Generates all valid partitions
├── PlayerInput.cs           // Handles player input and validation
├── Models/
│   └── Partition.cs         // Represents a single partition
└── Tests/                   // Unit tests
    ├── PartitionGeneratorTests.cs
    ├── PuzzleTests.cs
    └── GameFlowTests.cs
```

## How to Play

1. Launch the game
2. Each level presents a number and partitioning rules
3. Enter your partitions one number at a time
4. The game validates your solutions
5. Complete all levels to finish the game

## Testing

To run the test suite:
```
dotnet test
```

## Mathematical Background

The game is based on the concept of integer partitions from number theory. The Hardy-Ramanujan formula provides an asymptotic expression for the partition function p(n), which counts the number of distinct ways of representing n as a sum of natural numbers.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your improvements.

## License

This project is licensed under the MIT License.