
# Report

Course: C# Development SS20?? (4 ECTS, 3 SWS)

Student ID: CC241008

BCC Group: B

Name: Florian Golubic

## Methodology: 
### 3.1 Recursive method
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

### 3.2 Iterative method
### 3.2 Iterative algorithm
The following code snippet demonstrates the core logic for Recursive:
- Paramters :
  - n - int - total number of disks which is set by the user via command line (default = 3)
  - source - Peg - starting point from where the disks originate, is set in main
  - dest - Peg - the destination to which the disks will be moving (from source peg to dest peg)
  - spare - Peg - represents the peg not involved in diskMovement
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
```

## Additional Features
(explain your extra implementations that are not stated in the requirements, if any)

## Discussion/Conclusion
(the challenges you faced and how you solved it)

## Work with: 
(List down the name of your colleagues if you work with them)

## Reference: 
[(Tower of Hanoi - Wikipedia)](https://en.wikipedia.org/wiki/Tower_of_Hanoi)
[(Example)](https://www.mathsisfun.com/games/towerofhanoi.html)

