
# Report

Course: **C# Development SS20?? (4 ECTS, 3 SWS)**

Student ID: **CC241008**

BCC Group: **B**

Name: **Florian Golubic**

## Methodology: 
### Recursive implementation
The following code snippet demonstrates the core logic for the **Recursive** method:

1. **Move $m - 1$ disks** from the source to the spare peg using the same general solving procedure. Rules are not violated, by assumption. This leaves disk $m$ as the top disk on the source peg.
2. **Move disk $m$** from the source to the target peg. This is guaranteed to be a valid move by the assumptions—a simple step.
3. **Move the $m - 1$ disks** previously placed on the spare peg to the target peg using the same procedure. They are placed on top of disk $m$ without violating the rules.
4. **Base Case:** Move 0 disks (in steps 1 and 3), which means doing nothing—this does not violate the rules.

---
```csharp
static void Recursive(int n, Peg source, Peg dest, Peg spare)
{
    // Base case
    if (n == 0)
        return;

    // Move all disks smaller than this current to the spare.
    Recursive(n - 1, source, spare, dest);

    // Move disk to the destination peg
    int disk = source.DiskSizes.Pop();
    dest.DiskSizes.Push(disk);
    //PrintDiskMovement(disk, source.Name, dest.Name);
    DrawBoard(); // Draw the board after the move

    // Move disk from the spare back to the dest peg.
    Recursive(n - 1, spare, dest, source);
}
```

### Iterative implementation
#### Parameters
* **n (int):** Total number of disks set by the user via the command line (default = 3).
* **source (Peg):** The starting point from which the disks originate; initialized in `Main`.
* **dest (Peg):** The destination to which the disks will be moving.
* **spare (Peg):** Represents the peg not involved in the current disk movement.

---

#### Iterative Logic
> "For step 1, whenever we're moving the top disk, we always move it to the next position in the same direction. This is to the right if the starting number of pieces is **even**, or to the left if the starting number of pieces is **odd**.
>
> * **Even number of pieces:** Steps 1, 3, 5, 7... will place the top disk $A \rightarrow B \rightarrow C \rightarrow A \dots$
> * **Odd number of pieces:** Steps 1, 3, 5, 7... will place the top disk $A \rightarrow C \rightarrow B \rightarrow A \dots$
>
> For step 2, whenever we move another piece, there is always only one legal move, since no piece may be moved onto the smallest, and of any combination of other pieces, only one will fit the other." — *Wikipedia*

---
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
### MoveBetweenPegs function
The following code snippets demonstrate the core logic of the `MoveBetweenPegs` function.

**Parameters:**
* `peg1` (Peg): The first peg to check for valid disk movement.
* `peg2` (Peg): The second peg to check for valid disk movement.

**Logic:**
1. Check if either peg is empty to move the top disk from the occupied peg to the empty one.
2. If both pegs have disks, inspect the top disk of each stack via `Peek()` and compare them.
3. Depending on which disk is smaller, it will be placed on the peg with the larger disk.

---
```csharp
static void MoveBetweenPegs(Peg peg1, Peg peg2)
{
    // If peg 1 is empty, peg 2 top disk must move to peg 1
    if (peg1.DiskSizes.Count == 0)
    {
        int disk = peg2.DiskSizes.Pop();
        peg1.DiskSizes.Push(disk);
        //PrintDiskMovement(disk, peg2.Name, peg1.Name);
    }
    // If peg 2 is empty, move peg 1 top disk to peg 2
    else if (peg2.DiskSizes.Count == 0)
    {
        int disk = peg1.DiskSizes.Pop();
        peg2.DiskSizes.Push(disk);
        //PrintDiskMovement(disk, peg1.Name, peg2.Name);
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
            //PrintDiskMovement(topDisk1, peg1.Name, peg2.Name);
        }
        else
        {
            peg2.DiskSizes.Pop();
            peg1.DiskSizes.Push(topDisk2);
            //PrintDiskMovement(topDisk2, peg2.Name, peg1.Name);
        }
    }
            
    DrawBoard();
}
```

### Class Structure: Peg
**Primary Constructor:** Takes a `string name` as a parameter.

**Fields:**
* `Name` (string): Saves the name of the peg (e.g., "(L)", "(M)", or "(R)").
* `DiskSizes` (Stack<int>): Contains all disks currently on the peg.

---
```csharp
public class Peg(string name)
{
    public string Name { get; set; } = name;
    public Stack<int> DiskSizes { get; } = new Stack<int>();
}
```

### InitializePegs function
This function is used to initialize the pegs and fill the source (left) peg with $n$ disks. During initialization, the `DiskSizes` stack of `pegL` is populated. The `DrawBoard` function is called to render the starting position as ASCII art in the terminal.
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
### DrawBoard function
Used to draw the pegs and disks as ASCII art. `DrawBoard` is called at the initialization stage and after every disk movement in either the Recursive or Iterative method. A `Thread.Sleep(500)` call at the end of the function allows for ASCII animation effect.
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
    string lLabel = PadCenter(pegL.Name, colWidth);
    string mLabel = PadCenter(pegM.Name, colWidth);
    string rLabel = PadCenter(pegR.Name, colWidth);
    Console.WriteLine(lLabel + "   " + mLabel + "   " + rLabel);
    Console.WriteLine(new string('-', (colWidth * 3) + 6)); // Divider line

    Thread.Sleep(500);
}
```
### GetDiskString function
This function draws a disk, the size of which is **dependent** on the disk length, or represents an empty peg space.
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
### PadCenter function
A utility function that centers text within a given width.
```csharp
static string PadCenter(string text, int width)
{
    int padTotal = width - text.Length;
    int padLeft = padTotal / 2;
    int padRight = padTotal - padLeft;
    return new string(' ', padLeft) + text + new string(' ', padRight);
}
```
## Discussion/Conclusion
The Recursive method was fairly easy to implement due to the Wikipedia entry on the recursive solution, which goes into great depth.

The Iterative implementation was a bit more complicated. At first, I created some nested for loops, which quickly led to a dead end. Then, I switched from Arrays to Stacks; this proved to be very convenient for comparing the topDisks of two pegs, thanks to methods like Peek, Push, and Pop.

The main hurdle I faced was the calculation of totalMoves and the disk movement order. While the Wikipedia entry touched on the iterative methodology, it wasn’t as in-depth as the recursive section. I also decided to create a simple Peg class with a primary constructor to keep the code compact.

For the ASCII drawings and animations, I used the "Snake Game" animation and some AI assistance to ensure the spacing and disks were properly rendered in the terminal. I wish I had understood the iterative approach a bit sooner, but in the end, I am very happy with the result.

## Work with: 
Flo Madner

## Reference: 
[Tower of Hanoi - Wikipedia](https://en.wikipedia.org/wiki/Tower_of_Hanoi)
[Example](https://www.mathsisfun.com/games/towerofhanoi.html)
[Stack Class](https://learn.microsoft.com/en-us/dotnet/api/system.collections.stack?view=net-10.0 )
[Tower of Hanoi - Iterative](https://stackoverflow.com/questions/77292542/c-sharp-hanoi-tower-iterative-version)
[Snake Game Animation](https://yun-vis.net/ustp-bcc-csharp/pages/snake)