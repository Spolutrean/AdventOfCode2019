using System;
using System.IO;
using System.Linq;

namespace standard
{
    internal class Program
    {
        public static void Main(string[] args) {
            Int32[] arrSave = File.ReadLines("/home/spolutrean/adventofcode2019/day2/extra/extra/in.txt").First().Split(',').Select(i => System.Convert.ToInt32(i)).ToArray();
            for (int ii = 0; ii <= 99; ++ii) {
                for (int jj = 0; jj <= 99; ++jj) {
                    Int32[] arr = (int[]) arrSave.Clone();
                    arr[1] = ii;
                    arr[2] = jj;
                    for (int i = 0; i < arr.Length; i += 4) {
                        if (arr[i] == 1) {
                            arr[arr[i + 3]] = arr[arr[i + 1]] + arr[arr[i + 2]];
                        } else if (arr[i] == 2) {
                            arr[arr[i + 3]] = arr[arr[i + 1]] * arr[arr[i + 2]];
                        } else if(arr[i] == 99) {
                            //Console.WriteLine("Ok 99 caught");
                            break;
                        } else {
                            //Console.WriteLine("Wrong op-code");
                            return;
                        }
                    }

                    if (arr[0] == 19690720) {
                        Console.WriteLine(100 * ii + jj);
                    }
                }
            }
        }
    }
}