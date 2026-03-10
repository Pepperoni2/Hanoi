# Project Report: [Project Name]

**Course:** CRC-CD  
**Student Name:** Florian Golubic 
**Student ID:** cc241008 
**Date:** March 10, 2026

---

## 1. Introduction
This report outlines the implementation of a C# Console Application developed for CRC-CD UE. The primary objective of this application is to implement the Tower of Hanoi Game via Recursive and Iterative method.

## 2. System Architecture
The application is built using the .NET 10.



![Image of program class and class peg containing a Stack of type int called DiskSizes and a char name field](images/classDiagram.png)[Software class diagram]


### 2.1 Component Overview
* **Program.cs:** Serves as the entry point and handles the main application lifecycle.
* **Logic Layer:** Contains the methods and algorithms required to process data.
* **Data Model:** Defines the structure of the objects used within the system.

## 3. Implementation Details
The project uses two implementation functions (Recursive, Iterative) to create the Tower of Hanoi algorithm using multiple features like classes, stack management for the pegs and multiple ASCII Methods for drawing the Disks including animation

### 3.1 Recursive algorithm
The following code snippet demonstrates the core logic for Recursive:

```csharp
static void Recursive(int n, Peg source, Peg dest, Peg spare)
{
    if (n == 0)
        return;

    // Move all disks smaller than this current to the spare.
    Recursive(n - 1, source, spare, dest);

    // Move disk to the destination peg
    int disk = source.DiskSizes.Pop();
    dest.DiskSizes.Push(disk);
    PrintDiskMovement(disk, source.Name, dest.Name);
    DrawBoard(); // Draw the board after the move

    // Move disk from the spare back to the dest peg.
    Recursive(n - 1, spare, dest, source);
}
```

### 3.2 Iterative algorithm
The following code snippet demonstrates the core logic for Recursive:
```csharp
static void Iterative(int n, Peg source, Peg dest, Peg spare)
{
    // If the total number of disks is even, swap destination and spare
    // ensures that the disks will be built on the right peg 
    if (n % 2 == 0)
    {
        // tuple deconstruction
        (spare, dest) = (dest, spare);
    }

    int totalMoves = (int)Math.Pow(2, n) - 1;

    for (int i = 1; i <= totalMoves; i++)
    {
        
        if (i % 3 == 1) // odd numbered moves 
            MoveBetweenPegs(source, dest);
        else if (i % 3 == 2)
            MoveBetweenPegs(source, spare);
        else if (i % 3 == 0)
            MoveBetweenPegs(spare, dest);
    }
}
