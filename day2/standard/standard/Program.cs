using System;
using System.IO;
using System.Linq;

namespace standard
{
  internal class Program
  {
    public static void Main(string[] args) {
      Int32[] arr = File.ReadLines("/home/spolutrean/adventofcode2019/day2/standard/standard/in.txt").First().Split(',').Select(i => System.Convert.ToInt32(i)).ToArray();
      arr[1] = 12;
      arr[2] = 2;
      for (int i = 0; i < arr.Length; i += 4) {
        if (arr[i] == 1) {
          arr[arr[i + 3]] = arr[arr[i + 1]] + arr[arr[i + 2]];
        } else if (arr[i] == 2) {
          arr[arr[i + 3]] = arr[arr[i + 1]] * arr[arr[i + 2]];
        } else if(arr[i] == 99) {
          Console.WriteLine("Ok 99 caught");
          break;
        } else {
          Console.WriteLine("Wrong op-code");
        }
      }

      Console.WriteLine(arr[0]);
    }
  }
}