using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishBlazor
{
  class Program
  {
    static void Main(string[] arguments)
    {
      Action<string> display = Console.WriteLine;
      display("Publication d'une application blazor");

      display("Press any key to exit:");
      Console.ReadKey();
    }
  }
}
