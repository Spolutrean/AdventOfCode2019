using System;
using System.IO;
using System.Linq;

namespace standard {
    internal class Program {
        public static void Main(string[] args) {
            Int32[] arr = File.ReadLines("/home/spolutrean/adventofcode2019/day5/standard/standard/in.txt").First().Split(',').Select(i => System.Convert.ToInt32(i)).ToArray();
            for (int i = 0; i < arr.Length;) {
                int x = arr[i];
                int opcode = x % 100;
                int C = (x / 100) % 10;
                int B = (x / 1000) % 10;
                int A = (x / 10000) % 10;
                
                if (opcode == 1 || opcode == 2) {
                    int arg1 = arr[i + 1], arg2 = arr[i + 2];
                    if (C == 0) {
                        arg1 = arr[arg1];
                    }

                    if (B == 0) {
                        arg2 = arr[arg2];
                    }
                    
                    if (opcode == 1) {
                        arr[arr[i + 3]] = arg1 + arg2;    
                    }
                    else {
                        arr[arr[i + 3]] = arg1 * arg2;
                    }
                    i += 4;
                } else if (opcode == 3) {
                    arr[arr[i + 1]] = Convert.ToInt32(Console.ReadLine());
                    i += 2;
                } else if (opcode == 4) {
                    int res = arr[i + 1];
                    if (C == 0) {
                        res = arr[res];
                    }
                    Console.Write(res);
                    i += 2;
                } else if(opcode == 99) {
                    Console.WriteLine("\nOk 99 caught");
                    break;
                } else {
                    Console.WriteLine("\nWrong op-code");
                    break;
                }
            }

            //Console.WriteLine(arr[0]);
        }
    }
}