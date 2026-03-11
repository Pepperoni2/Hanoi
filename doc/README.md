# Towers of Hanoi - .NET 10

A terminal-based visualization of the classic mathematical puzzle, implemented in C# using both recursive and iterative logic.

## About The Project
This project demonstrates the **Towers of Hanoi** algorithm using modern .NET 10 features. It features a custom stack implementation to manage disk states and provides a real-time ASCII animation to visualize the solution steps.

### Key Features:
* **Recursive Methodology**
* **Iterative Methodology**
* **Stack Implementation**
* **ASCII Animation**

## Getting Started 

### Prerequisites
* **.NET 10 SDK** (Ensure this is installed via `dotnet --version`)
* **Visual Studio Code** (or any C# compatible IDE)

### Installation
1. Download the `zip` folder.
2. Extract and open the `Hanoi` folder in your terminal or IDE.
3. Ensure you are inside the project root where the `.csproj` file is located.

### Running the Program
First, build the project to ensure all dependencies are resolved:
```bash
dotnet build
```
To run the animation, use the following syntax:
```Bash 
dotnet run -[Method] [numberOfDisks]
```
**Example:**
```Bash
dotnet run -Recursive 3
```
| Command | Description |
| :--- | :--- |
| `dotnet run -- -Recursive 5` | Solves a 5-disk puzzle using recursion. |
| `dotnet run -- -Iterative 4` | Solves a 4-disk puzzle using an iterative loop. |
| `dotnet run` | Displays the usage help message. |

> [!NOTE]
> Higher disk counts (e.g., 10+) will result in a significantly longer animation time as the number of moves required is $2^n - 1$.

## Roadmap 
- [x] **Recursive Methodology**: Implementation of the classic $2^n - 1$ move algorithm.
- [x] **Iterative Methodology**: Binary/Modulo based solution.
- [x] **Stack implementation**: Custom stack logic to track disk positions on Pegs A, B, and C.
- [x] **ASCII Animation**: Frame-by-frame terminal rendering of disk movement.

## Contributing 
If you want to contribute to the project, feel free to do so:
1. Fork the Project.
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`).
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the Branch (`git push origin feature/AmazingFeature`).
5. Open a Pull Request.

##  License 
Distributed under the **CC0 1.0 Universal** License. See `LICENSE` for more information.

## Contact 
**Project Link:** [https://github.com/Pepperoni2/Hanoi](https://github.com/Pepperoni2/Hanoi)

## Acknowledgments
* Inspired by classic computer science algorithmic challenges.
