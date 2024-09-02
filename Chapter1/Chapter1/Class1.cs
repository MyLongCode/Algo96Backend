using System.Diagnostics;

namespace Chapter1
{
    public class Class1
    {
        static async Task DoSomethingAsync()
        {
            int value = 13;
            // Асинхронно ожидать 1 секунду.
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(true);
            value *= 2;
            // Асинхронно ожидать 1 секунду.
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(true);
            Console.WriteLine(value);
        }
        public static void Main()
        {
            DoSomethingAsync();
            Console.WriteLine(1);
        }
    }

}