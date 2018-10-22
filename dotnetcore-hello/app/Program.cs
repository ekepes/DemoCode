using static System.Console;
using library;

// source: https://docs.microsoft.com/en-us/dotnet/core/tutorials/using-on-macos
namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine($"The answer is {new Thing().Get(19, 23)}");
        }
    }
}
