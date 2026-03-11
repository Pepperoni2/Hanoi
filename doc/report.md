
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
The following code snippet demonstrates the core logic for Recursive:
- Parameters :
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
#### 3.2.1 MoveBetweenPegs function
The following code snippets demonstrates the core logic of the function MoveBetweenPegs.
- Parameters:
  - peg1 - Peg - must contain one of the Peg you want to check if disk movement is possible
  - peg2 - Peg - other Peg to check if disk movement is possible 
```csharp
static void MoveBetweenPegs(Peg peg1, Peg peg2)
{
    // If peg 1 is empty, peg 2 top disk must move to peg 1
    if (peg1.DiskSizes.Count == 0)
    {
        int disk = peg2.DiskSizes.Pop();
        peg1.DiskSizes.Push(disk);
        PrintDiskMovement(disk, peg2.Name, peg1.Name);
    }
    // If peg 2 is empty, move peg 1 top disk to peg 2
    else if (peg2.DiskSizes.Count == 0)
    {
        int disk = peg1.DiskSizes.Pop();
        peg2.DiskSizes.Push(disk);
        PrintDiskMovement(disk, peg1.Name, peg2.Name);
    }
    // If both have disks, compare the top disks
    else
    {
        int topDisk1 = peg1.DiskSizes.Peek();
        int topDisk2 = peg2.DiskSizes.Peek();

        if (topDisk1 < topDisk2)
        {
            peg1.DiskSizes.Pop();
            peg2.DiskSizes.Push(topDisk1);
            PrintDiskMovement(topDisk1, peg1.Name, peg2.Name);
        }
        else
        {
            peg2.DiskSizes.Pop();
            peg1.DiskSizes.Push(topDisk2);
            PrintDiskMovement(topDisk2, peg2.Name, peg1.Name);
        }
    }
            
    DrawBoard();
}
```

### 3.2.2 Peg class
Primary constrcutor with the parameter of char name
- Fields:
  - Name - char - saves the name of the Peg ('L', 'M' or 'R')
  - DiskSizes - Stack of type int - contains all disk currently on the peg
```csharp
public class Peg(char name)
{
    public char Name { get; set; } = name;
    public Stack<int> DiskSizes { get; } = new Stack<int>();
}
```

### 3.2.3 InitializePegs function
This function is used to initialize the pegs and fill the source (left) peg with n disks.
During initialization Stack DiskSizes of pegL will be filled with the disks.
Call DrawBoard function for drawing the starting position as ASCII-Art in the terminal.
```csharp
static void InitializePegs(int n)
{
    totalDisks = n;
    pegL.DiskSizes.Clear();
    pegM.DiskSizes.Clear();
    pegR.DiskSizes.Clear();

    // Initialize the source peg with disks
    for (int i = n; i > 0; i--)
    {
        pegL.DiskSizes.Push(i);
    }
    
    // Initial starting state
    DrawBoard();
}
```
### 3.2.4 DrawBoard function
This function is used to draw the Pegs and disks to be displayed in the terminal as ASCII-Art 
DrawBoard is called at the Initialization stage and at every DiskMovement
in either the Recursive or Iterative method
Thread.Sleep(500) at the end of the function allowed for ASCII animation 
```csharp
static void DrawBoard()
{
    Console.Clear();
    // The width of the largest disk dictates the column width
    int colWidth = totalDisks * 2 + 3; 

    // Convert stacks to arrays (top to bottom)
    // Reverse array so bottom disk is index 0
    int[] lDisks = pegL.DiskSizes.ToArray(); Array.Reverse(lDisks);
    int[] mDisks = pegM.DiskSizes.ToArray(); Array.Reverse(mDisks);
    int[] rDisks = pegR.DiskSizes.ToArray(); Array.Reverse(rDisks);

    Console.WriteLine();
    
    // Draw row by row, from top to bottom
    for (int row = totalDisks - 1; row >= 0; row--)
    {
        string lStr = GetDiskString(lDisks, row, colWidth);
        string mStr = GetDiskString(mDisks, row, colWidth);
        string rStr = GetDiskString(rDisks, row, colWidth);
        
        Console.WriteLine(lStr + "   " + mStr + "   " + rStr);
    }

    // Draw labels centered under each peg
    string lLabel = PadCenter("(L)", colWidth);
    string mLabel = PadCenter("(M)", colWidth);
    string rLabel = PadCenter("(R)", colWidth);
    Console.WriteLine(lLabel + "   " + mLabel + "   " + rLabel);
    Console.WriteLine(new string('-', (colWidth * 3) + 6)); // Divider line

    Thread.Sleep(500);
}
```
#### 3.2.4.1 GetDiskString function
This function draws the disk, which its size is dependend on the disk.length, or the empty Peg space.
```csharp
static string GetDiskString(int[] disks, int row, int colWidth)
{
    if (row < disks.Length)
    {
        int size = disks[row];
        int plusCount = size * 2 + 1;
        string disk = "<" + new string('+', plusCount) + ">";
        return PadCenter(disk, colWidth);
    }
    else
    {
        // Empty peg space
        return PadCenter("|", colWidth);
    }
}
```
#### 3.2.4.2 PadCenter function

## Discussion/Conclusion
(the challenges you faced and how you solved it)

## Work with: 
Flo Madner

## Reference: 
[(Tower of Hanoi - Wikipedia)](https://en.wikipedia.org/wiki/Tower_of_Hanoi)
[(Example)](https://www.mathsisfun.com/games/towerofhanoi.html)

