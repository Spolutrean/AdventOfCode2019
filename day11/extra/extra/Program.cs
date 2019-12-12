using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace extra {
    internal class Program {
        static Int64[] arr = File
            .ReadLines("/home/spolutrean/adventofcode2019/day11/standard/standard/in.txt").First()
            .Split(',').Select(i => Convert.ToInt64(i)).ToArray();


        private static List<Point> changed = new List<Point>();
        static long programPosition = 0;
        static int inputPosition = 0, outputPosition = 0;
        static List<String> input = new List<string>(), output = new List<string>();
        
        static int x = 1, y = 1;
        static int w = 44, h = 8;
        static char[,] matrix;
        static int[] dx = {-1, 0, 1, 0}, dy = {0, 1, 0, -1};
        static int direction = 0;

        static void turnLeft() {
            direction = (direction + 7) % 4;
        }

        static void turnRight() {
            direction = (direction + 1) % 4;
        }
        
        static Dictionary<Int64, Int64> dict = new Dictionary<long, long>();
        static long relativeBase;

        static void setValue(long key, long value) {
            if (dict.ContainsKey(key)) {
                dict[key] = value;
            }
            else {
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
            }
            else if (X == 1) {
                pos = argPos;
            }
            else {
                pos = getValue(argPos) + relativeBase;
            }
        }

        static void runIntcode() {
            for (int i = 0; i < arr.Length; ++i) {
                setValue(i, arr[i]);
            }

            setValue(arr.Length, 0);

            for (;;) {
                long opcode = dict[programPosition] % 100;

                if (opcode == 1 || opcode == 2) {
                    long pos1, pos2, pos3;
                    normalizePositions(programPosition, out pos1, out pos2, out pos3);

                    if (opcode == 1) {
                        setValue(pos3, getValue(pos1) + getValue(pos2));
                    }
                    else {
                        setValue(pos3, getValue(pos1) * getValue(pos2));
                    }

                    programPosition += 4;
                }
                else if (opcode == 3) {
                    long pos1;
                    normalizePositions(programPosition, out pos1);
                    setValue(pos1, Convert.ToInt64(input[inputPosition++]));
                    programPosition += 2;
                }
                else if (opcode == 4) {
                    long pos1;
                    normalizePositions(programPosition, out pos1);
                    //Console.Write(getValue(pos1) + "\n");
                    output.Add(getValue(pos1).ToString());
                    onOutputChanged();
                    programPosition += 2;
                }
                else if (opcode == 5 || opcode == 6) {
                    long pos1, pos2;
                    normalizePositions(programPosition, out pos1, out pos2);

                    if (opcode == 5) {
                        if (getValue(pos1) != 0) {
                            programPosition = getValue(pos2);
                        }
                        else {
                            programPosition += 3;
                        }
                    }
                    else {
                        if (getValue(pos1) == 0) {
                            programPosition = getValue(pos2);
                        }
                        else {
                            programPosition += 3;
                        }
                    }
                }
                else if (opcode == 7) {
                    long pos1, pos2, pos3;
                    normalizePositions(programPosition, out pos1, out pos2, out pos3);

                    if (getValue(pos1) < getValue(pos2)) {
                        setValue(pos3, 1);
                    }
                    else {
                        setValue(pos3, 0);
                    }

                    programPosition += 4;
                }
                else if (opcode == 8) {
                    long pos1, pos2, pos3;
                    normalizePositions(programPosition, out pos1, out pos2, out pos3);

                    if (getValue(pos1) == getValue(pos2)) {
                        setValue(pos3, 1);
                    }
                    else {
                        setValue(pos3, 0);
                    }

                    programPosition += 4;
                }
                else if (opcode == 9) {
                    long pos1;
                    normalizePositions(programPosition, out pos1);
                    relativeBase += getValue(pos1);
                    programPosition += 2;
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
        }
        
        public static void Main(string[] args) {
            matrix = new char[h,w];
            for (int i = 0; i < h; ++i) {
                for (int j = 0; j < w; ++j) {
                    matrix[i, j] = '.';
                }
            }

            matrix[x, y] = '#';
            input.Add(1.ToString());
            
            runIntcode();

            for (int i = 0; i < h; ++i) {
                String line = "";
                for (int j = 0; j < w; ++j) {
                    line += matrix[i, j];
                }

                Console.WriteLine(line);
            }
        }

        static void onOutputChanged() {
            if (output.Count - outputPosition != 2) {
                return;
            }

            int color = Convert.ToInt32(output[outputPosition++]);
            int turn = Convert.ToInt32(output[outputPosition++]);
            changed.Add(new Point(x, y));
            matrix[x, y] = color == 1 ? '#' : '.';
            if (turn == 0) {
                turnLeft();
            } else {
                turnRight();  
            }

            x += dx[direction];
            y += dy[direction];
            
            input.Add(matrix[x,y] == '.' ? "0" : "1");
        }

        class Point {
            public int x, y;

            public Point(int x, int y) {
                this.x = x;
                this.y = y;
            }

            public override bool Equals(object obj) {
                Point other = (Point) obj;
                return x == other.x && y == other.y;
            }

            public override int GetHashCode() {
                return (int) (1000) * x + y;
            }
        }
    }
}