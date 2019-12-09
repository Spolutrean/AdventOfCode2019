using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace standard_extra {
    internal class Program {
        static Int64[] arr = File.ReadLines("/home/spolutrean/adventofcode2019/day9/standard_extra/standard_extra/in.txt").First()
            .Split(',').Select(i => Convert.ToInt64(i)).ToArray();

        static Dictionary<Int64, Int64> dict = new Dictionary<long, long>();
        static long relativeBase;

        static void setValue(long key, long value) {
            if (dict.ContainsKey(key)) { 
                dict[key] = value;
            } else {
                dict.Add(key, value);  
            }
        }

        static long getValue(long key) {
            if (!dict.ContainsKey(key)) {
                dict.Add(key, 0);
            }

            return dict[key];
        }
        
        static void normalizePositions(long i, out long pos1) {
            normalizeSinglePosition(i, 1, out pos1);
        }

        static void normalizePositions(long i, out long pos1, out long pos2) {
            normalizeSinglePosition(i, 1, out pos1);
            normalizeSinglePosition(i, 2, out pos2);
        }

        static void normalizePositions(long i, out long pos1, out long pos2, out long pos3) {
            normalizeSinglePosition(i, 1, out pos1);
            normalizeSinglePosition(i, 2, out pos2);
            normalizeSinglePosition(i, 3, out pos3);
        }

        static void normalizeSinglePosition(long i, int posNum, out long pos) {
            long X = (long) (dict[i] / Math.Pow(10, posNum + 1) % 10);
            long argPos = i + posNum;
            if (X == 0) {
                pos = getValue(argPos);
            } else if (X == 1) {
                pos = argPos;
            } else {
                pos = getValue(argPos) + relativeBase;
            }
        }

        public static void Main(string[] args) {
            for (int i = 0; i < arr.Length; ++i) {
                setValue(i, arr[i]);
            }
            setValue(arr.Length, 0);
            
            for (long i = 0;;) {
                long opcode = dict[i] % 100;

                if (opcode == 1 || opcode == 2) {
                    long pos1, pos2, pos3;
                    normalizePositions(i, out pos1, out pos2, out pos3);

                    if (opcode == 1) {
                        setValue(pos3, getValue(pos1) + getValue(pos2));
                    }
                    else {
                        setValue(pos3, getValue(pos1) * getValue(pos2));
                    }

                    i += 4;
                }
                else if (opcode == 3) {
                    long pos1;
                    normalizePositions(i, out pos1);
                    setValue(pos1, Convert.ToInt64(Console.ReadLine()));
                    i += 2;
                }
                else if (opcode == 4) {
                    long pos1;
                    normalizePositions(i, out pos1);
                    Console.Write(getValue(pos1) + "\n");
                    i += 2;
                }
                else if (opcode == 5 || opcode == 6) {
                    long pos1, pos2;
                    normalizePositions(i, out pos1, out pos2);

                    if (opcode == 5) {
                        if (getValue(pos1) != 0) {
                            i = getValue(pos2);
                        }
                        else {
                            i += 3;
                        }
                    }
                    else {
                        if (getValue(pos1) == 0) {
                            i = getValue(pos2);
                        }
                        else {
                            i += 3;
                        }
                    }
                }
                else if (opcode == 7) {
                    long pos1, pos2, pos3;
                    normalizePositions(i, out pos1, out pos2, out pos3);

                    if (getValue(pos1) < getValue(pos2)) {
                        setValue(pos3, 1);
                    }
                    else {
                        setValue(pos3, 0);
                    }

                    i += 4;
                }
                else if (opcode == 8) {
                    long pos1, pos2, pos3;
                    normalizePositions(i, out pos1, out pos2, out pos3);

                    if (getValue(pos1) == getValue(pos2)) {
                        setValue(pos3, 1);
                    }
                    else {
                        setValue(pos3, 0);
                    }

                    i += 4;
                }
                else if (opcode == 9) {
                    long pos1;
                    normalizePositions(i, out pos1);
                    relativeBase += getValue(pos1);
                    i += 2;
                }
                else if (opcode == 99) {
                    Console.WriteLine("\nOk 99 caught");
                    break;
                }
                else {
                    Console.WriteLine("\nWrong op-code");
                    break;
                }
            }

            //Console.WriteLine(dict[0]);
        }
    }
}