namespace Hanoi;

class Program
{
    static void Main(string[] args)
    {
        // Default values
        int diskCount = 3; 

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].Equals("-Recursive", StringComparison.OrdinalIgnoreCase))
            {
                // Checking if there is a number following the flag
                if (i + 1 < args.Length && int.TryParse(args[i + 1], out int n))
                {
                    diskCount = n;
                    Recursive(diskCount, 'L', 'M', 'R');
                    return;
                }
                else
                {
                    Console.WriteLine("Error: Please provide the number of disks after -Recursive.");
                    return;
                }
            }
            if (args[i].Equals("-Iterative", StringComparison.OrdinalIgnoreCase)){
                if(i + 1 <args.Length && int.TryParse(args[i+1], out int n))
                {
                    diskCount = n;
                    Iterative(diskCount, 'L', 'M', 'R');
                    return;
                }
                else
                {
                    Console.WriteLine("Error: Please provide the number of disks after -Iterative.");
                    return;
                } 
            }
        }

        Console.WriteLine("Usage: dotnet run -Recursive [numberOfDisks] or -Iterative [numberOfDisks]");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n">The total number of disks</param>
    /// If h > 1, then first use this procedure to move the h − 1 smaller disks from peg A to peg B.
    /// Now the largest disk, i.e. disk h can be moved from peg A to peg C.
    /// If h > 1, then again use this procedure to move the h − 1 smaller disks from peg B to peg C.
    /// <returns></returns>
    static void Recursive(int n, char source, char dest, char spare)
    {
        if (n == 0)
            return;
        
        // Move all disks smaller than this one over to the spare.
        Recursive(n - 1, source, spare, dest);

        // Move the remaining disk to the destination peg
        PrintDiskMovement(n, source, dest);
        // Move the disk we just moved to the spare back over to the dest peg.
        Recursive(n - 1, spare, dest, source); 
        
    }
    static void PrintDiskMovement(int n, char source, char dest)
    {
        Console.WriteLine($"Disk {n} moved from ({source}) to ({dest})");
    }

    static void Iterative(int n, char sourceName, char destName, char spareName)
    {
        Peg source  = new(sourceName);
        Peg dest    = new(destName);
        Peg spare   = new(spareName);

        // Initialize the source peg with disks (largest at the bottom, smallest at the top)
        for (int i = n; i > 0 ; i--)
        {
            source.DiskSizes.Push(i);
        }

        // If the total number of disks is even, we swap destination and spare
        if(n % 2 == 0)
        {
            (spare, dest) = (dest, spare);
        }

        int totalMoves = (int)Math.Pow(2, n) - 1;

        for (int i = 1; i <= totalMoves; i++)
        {
            if(i % 3 == 1)
                MoveBetweenPegs(source,dest);
            else if(i % 3 == 2)
                MoveBetweenPegs(source, spare);
            else if(i % 3 == 0)
                MoveBetweenPegs(spare, dest);  
        }
    } 

    static void MoveBetweenPegs(Peg peg1, Peg peg2)
    {
        // If peg 1 is empty, peg 2's top disk must move to peg 1
        if(peg1.DiskSizes.Count == 0)
        {
            int disk = peg2.DiskSizes.Pop();
            peg1.DiskSizes.Push(disk);
            PrintDiskMovement(disk, peg2.Name, peg1.Name);
        }
        // If peg 2 is empty, peg 1's top disk must move to peg 2
        else if(peg2.DiskSizes.Count == 0)
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

            if(topDisk1 < topDisk2)
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
    }

}

public class Peg(char _name)
{
    public char Name { get; set; } = _name;
    public Stack<int> DiskSizes { get; } = new Stack<int>();
}
