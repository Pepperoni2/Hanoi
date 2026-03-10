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
The project utilizes several key C# features, including [list features, e.g., LINQ, Collections, or File I/O], to achieve its functionality.

### 3.1 Key Algorithm
The following code snippet demonstrates the core logic for [Function Name]:

```csharp
// Example implementation of the primary logic
public void ExecuteTask(string input)
{
    if (string.IsNullOrEmpty(input))
    {
        throw new ArgumentException("Input cannot be null");
    }
    // Logic implementation here
}