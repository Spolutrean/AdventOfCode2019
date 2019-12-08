using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace extra {
    internal class Program {
        static bool[] used = new bool[5] {false, false, false, false, false};
        static List<String> generatedStrings = new List<string>();
        
        static Int32[] originalArr = File.ReadLines("/home/spolutrean/adventofcode2019/day7/extra/extra/in.txt").First().Split(',').Select(i => System.Convert.ToInt32(i)).ToArray();

        static void normalizeArgs(Int32[] arr, int i, out int arg1) {
            int C = (arr[i] / 100) % 10;

            arg1 = arr[i + 1];
            if (C == 0) {
                arg1 = arr[arg1];
            }
        }
        static void normalizeArgs(Int32[] arr, int i, out int arg1, out int arg2) {
            normalizeArgs(arr, i, out arg1);
            
            int B = (arr[i] / 1000) % 10;
            arg2 = arr[i + 2];
            if (B == 0) {
                arg2 = arr[arg2];
            }
        }
        static void normalizeArgs(Int32[] arr, int i, out int arg1, out int arg2, out int arg3) {
            arg3 = arr[i + 3];
            normalizeArgs(arr, i, out arg1, out arg2);
        }
        
        static void rec(String s) {
            if (s.Length == 5) {
                generatedStrings.Add(s);
                return;
            }

            for (int i = 0; i < 5; ++i) {
                if (!used[i]) {
                    used[i] = true;
                    String sTo = s.Clone() as string;
                    sTo += (i + 5).ToString();
                    rec(sTo);
                    used[i] = false;
                }
            }
        }

        static String runIntcode(String input, ref Int32[] arr, ref int i) {
            String output = "";
            String[] inputArray = input.Split(' ');
            int inputI = 0;

            for (; i < arr.Length;) {
                int opcode = arr[i] % 100;

                if (opcode == 1 || opcode == 2) {
                    int arg1, arg2, arg3;
                    normalizeArgs(arr, i, out arg1, out arg2, out arg3);

                    if (opcode == 1) {
                        arr[arg3] = arg1 + arg2;
                    }
                    else {
                        arr[arg3] = arg1 * arg2;
                    }

                    i += 4;
                }
                else if (opcode == 3) {
                    arr[arr[i + 1]] = Convert.ToInt32(inputArray[inputI++]);
                    i += 2;
                }
                else if (opcode == 4) {
                    int arg1;
                    normalizeArgs(arr, i, out arg1);
                    output += arg1;
                    i += 2;
                    return output;
                }
                else if (opcode == 5 || opcode == 6) {
                    int arg1, arg2;
                    normalizeArgs(arr, i, out arg1, out arg2);

                    if (opcode == 5) {
                        if (arg1 != 0) {
                            i = arg2;
                        }
                        else {
                            i += 3;
                        }
                    }
                    else {
                        if (arg1 == 0) {
                            i = arg2;
                        }
                        else {
                            i += 3;
                        }
                    }
                }
                else if (opcode == 7) {
                    int arg1, arg2, arg3;
                    normalizeArgs(arr, i, out arg1, out arg2, out arg3);

                    if (arg1 < arg2) {
                        arr[arg3] = 1;
                    }
                    else {
                        arr[arg3] = 0;
                    }

                    i += 4;
                }
                else if (opcode == 8) {
                    int arg1, arg2, arg3;
                    normalizeArgs(arr, i, out arg1, out arg2, out arg3);

                    if (arg1 == arg2) {
                        arr[arg3] = 1;
                    }
                    else {
                        arr[arg3] = 0;
                    }

                    i += 4;
                }
                else if (opcode == 99) {
                    output += "Ok 99 caught";
                    break;
                }
                else {
                    Console.WriteLine("\nWrong op-code\n");
                    break;
                }
            }

            return output;
        }
        
        public static void Main(string[] args) {
            rec("");
            int ans = 0;
            foreach(String s in generatedStrings) {
                String ss = s;
                Int32[] Aarr = originalArr.Clone() as Int32[],
                    Barr = originalArr.Clone() as Int32[],
                    Carr = originalArr.Clone() as Int32[],
                    Darr = originalArr.Clone() as Int32[],
                    Earr = originalArr.Clone() as Int32[];

                int Ap = 0, Bp = 0, Cp = 0, Dp = 0, Ep = 0;
                String phaseA = ss[0] + " ";
                String phaseB = ss[1] + " ";
                String phaseC = ss[2] + " ";
                String phaseD = ss[3] + " ";
                String phaseE = ss[4] + " ";

                String Ain = "0";
                for (int q = 0; q < 100000; ++q) {
                    //Console.WriteLine("Phase: " + q);
                    String Aout = runIntcode(phaseA + Ain, ref Aarr, ref Ap);
                    //Console.WriteLine(Aout);
                    String Bout = runIntcode(phaseB + Aout, ref Barr, ref Bp);
                    //Console.WriteLine(Bout);
                    String Cout = runIntcode(phaseC + Bout, ref Carr, ref Cp);
                    //Console.WriteLine(Cout);
                    String Dout = runIntcode(phaseD + Cout, ref Darr, ref Dp);
                    //Console.WriteLine(Dout);
                    String Eout = runIntcode(phaseE + Dout, ref Earr, ref Ep);
                    //Console.WriteLine(Eout);
                    if (Eout == "Ok 99 caught") {
                        Console.WriteLine("Real end");
                        break;
                    }
                    phaseA = phaseB = phaseC = phaseD = phaseE = "";
                    ans = Math.Max(ans, Int32.Parse(Eout));
                    Ain = Eout;
                }
            }

            Console.WriteLine(ans);
        }
    }
}