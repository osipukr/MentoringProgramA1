using System;
using ClassLibrary1;

namespace task1
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var timeService = new TimeService();

            Console.WriteLine(timeService.GetCurrentTime());
        }
    }
}