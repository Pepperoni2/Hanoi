using System;
using System.Collections.Generic;
using System.Threading;

namespace Hanoi
{
    class Program
    {
        // Global pegs so both algorithms can update and draw the same state
        static Peg pegL = new('L');
        static Peg pegM = new('M');
        static Peg pegR = new('R');
        static int totalDisks;

        static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("-Recursive", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 < args.Length && int.TryParse(args[i + 1], out int n))
                    {
                        InitializePegs(n);
                        Recursive(n, pegL, pegR, pegM);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Error: Please provide the number of disks after -Recursive.");
                        return;
                    }
                }
                if (args[i].Equals("-Iterative", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 < args.Length && int.TryParse(args[i + 1], out int n))
                    {
                        InitializePegs(n);
                        Iterative(n, pegL, pegR, pegM);
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

        static void InitializePegs(int n)
        {
            totalDisks = n;
            pegL.DiskSizes.Clear();
            pegM.DiskSizes.Clear();
            pegR.DiskSizes.Clear();

            // Initialize the source peg with disks (largest at the bottom, smallest at the top)
            for (int i = n; i > 0; i--)
            {
                pegL.DiskSizes.Push(i);
            }
            
            // Draw the initial starting state
            DrawBoard();
        }

        static void Recursive(int n, Peg source, Peg dest, Peg spare)
        {
            if (n == 0)
                return;

            // Move all disks smaller than this one over to the spare.
            Recursive(n - 1, source, spare, dest);

            // Move the remaining disk to the destination peg
            int disk = source.DiskSizes.Pop();
            dest.DiskSizes.Push(disk);
            PrintDiskMovement(disk, source.Name, dest.Name);
            DrawBoard(); // Draw the board after the move

            // Move the disk we just moved to the spare back over to the dest peg.
            Recursive(n - 1, spare, dest, source);
        }

        static void Iterative(int n, Peg source, Peg dest, Peg spare)
        {
            // If the total number of disks is even, we swap destination and spare
            if (n % 2 == 0)
            {
                (spare, dest) = (dest, spare);
            }

            int totalMoves = (int)Math.Pow(2, n) - 1;

            for (int i = 1; i <= totalMoves; i++)
            {
                if (i % 3 == 1)
                    MoveBetweenPegs(source, dest);
                else if (i % 3 == 2)
                    MoveBetweenPegs(source, spare);
                else if (i % 3 == 0)
                    MoveBetweenPegs(spare, dest);
            }
        }

        static void MoveBetweenPegs(Peg peg1, Peg peg2)
        {
            // If peg 1 is empty, peg 2's top disk must move to peg 1
            if (peg1.DiskSizes.Count == 0)
            {
                int disk = peg2.DiskSizes.Pop();
                peg1.DiskSizes.Push(disk);
                PrintDiskMovement(disk, peg2.Name, peg1.Name);
            }
            // If peg 2 is empty, peg 1's top disk must move to peg 2
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
            
            // Draw the board after any iterative move completes
            DrawBoard();
        }

        static void PrintDiskMovement(int n, char source, char dest)
        {
            Console.WriteLine($"\nDisk {n} moved from ({source}) to ({dest})");
        }

        // --- ASCII ART METHODS ---
        static void DrawBoard()
        {
            Console.Clear();
            // The width of the largest disk dictates the column width
            int colWidth = totalDisks * 2 + 3; 

            // Convert stacks to arrays (top to bottom) and reverse them so index 0 is the bottom disk
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
                
                // Print the row with a gap between pegs
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

        static string PadCenter(string text, int width)
        {
            int padTotal = width - text.Length;
            int padLeft = padTotal / 2;
            int padRight = padTotal - padLeft;
            return new string(' ', padLeft) + text + new string(' ', padRight);
        }
    }

    public class Peg(char name)
    {
        public char Name { get; set; } = name;
        public Stack<int> DiskSizes { get; } = new Stack<int>();
    }
}