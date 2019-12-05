using System;
using System.IO;
using System.Linq;

namespace extra {
    internal class Program {
        static Int32[] arr = File.ReadLines("/home/spolutrean/adventofcode2019/day5/extra/extra/in.txt").First().Split(',').Select(i => System.Convert.ToInt32(i)).ToArray();

        static void normalizeArgs(int i, out int arg1) {
            int C = (arr[i] / 100) % 10;

            arg1 = arr[i + 1];
            if (C == 0) {
                arg1 = arr[arg1];
            }
        }
        
        static void normalizeArgs(int i, out int arg1, out int arg2) {
            normalizeArgs(i, out arg1);
            
            int B = (arr[i] / 1000) % 10;
            arg2 = arr[i + 2];
            if (B == 0) {
                arg2 = arr[arg2];
            }
        }

        static void normalizeArgs(int i, out int arg1, out int arg2, out int arg3) {
            arg3 = arr[i + 3];
            normalizeArgs(i, out arg1, out arg2);
        }

        public static void Main(string[] args) {
            for (int i = 0; i < arr.Length;) {
                int opcode = arr[i] % 100;
                
                if (opcode == 1 || opcode == 2) {
                    int arg1, arg2, arg3;
                    normalizeArgs(i, out arg1, out arg2, out arg3);
                    
                    if (opcode == 1) {
                        arr[arg3] = arg1 + arg2;    
                    }
                    else {
                        arr[arg3] = arg1 * arg2;
                    }
                    
                    i += 4;
                } else if (opcode == 3) {
                    arr[arr[i + 1]] = Convert.ToInt32(Console.ReadLine());
                    i += 2;
                } else if (opcode == 4) {
                    int arg1;
                    normalizeArgs(i, out arg1);
                    Console.Write(arg1);
                    i += 2;
                } else if (opcode == 5 || opcode == 6) {
                    int arg1, arg2;
                    normalizeArgs(i, out arg1, out arg2);

                    if (opcode == 5) {
                        if (arg1 != 0) {
                            i = arg2;
                        }
                        else {
                            i += 3;
                        }
                    } else {
                        if (arg1 == 0) {
                            i = arg2;
                        } else {
                            i += 3;
                        }
                    }
                } else if (opcode == 7) {
                    int arg1, arg2, arg3;
                    normalizeArgs(i, out arg1, out arg2, out arg3);

                    if (arg1 < arg2) {
                        arr[arg3] = 1;
                    } else {
                        arr[arg3] = 0;
                    }

                    i += 4;
                } else if (opcode == 8) {
                    int arg1, arg2, arg3;
                    normalizeArgs(i, out arg1, out arg2, out arg3);
                    
                    if (arg1 == arg2) {
                        arr[arg3] = 1;
                    } else {
                        arr[arg3] = 0;
                    }

                    i += 4;
                }                
                else if(opcode == 99) {
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