using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFold
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> list = new List<int> { 1, 2, 3, 4, 5 };

            var left = LeftFold(list, SampleFunction);
            Console.WriteLine("left={0}\r\n\r\n", left);

            var right = RightFold(list, SampleFunction);
            Console.WriteLine("right={0}", right);

            Console.ReadLine();
        }

        private static int SampleFunction(int accumulator, int next)
        {
            Console.WriteLine("accumulator={0}, next={1}", accumulator, next);
            return accumulator - next;
        }

        private static int LeftFold(IEnumerable<int> list, Func<int, int, int> operation)
        {
            return list.Aggregate((accumulator, next) => operation(accumulator, next));
        }

        private static int RightFold(IEnumerable<int> list, Func<int, int, int> operation)
        {
            return list.Reverse().Aggregate((accumulator, next) => operation(accumulator, next));
        }
    }
}
