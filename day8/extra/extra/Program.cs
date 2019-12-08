using System;
using System.IO;

namespace extra {
    internal class Program {
        public static void Main(string[] args) {
            String s = File.ReadAllText("/home/spolutrean/adventofcode2019/day8/extra/extra/in.txt");
            int w = 25, h = 6;
            int[,] result = new int[h,w];
            for (int i = 0; i < h; ++i) {
                for (int j = 0; j < w; ++j) {
                    result[i, j] = 2;
                }
            }
            //9117554426
            int perLayer = h * w;
            int layersCount = s.Length / perLayer;
            for (int l = 0; l < layersCount; ++l) {
                for (int i = 0; i < h; ++i) {
                    for (int j = 0; j < w; ++j) {
                        int c = s[l * perLayer + i * w + j] - '0';
                        if (result[i, j] == 2 && c != 2) {
                            result[i, j] = c;
                        }
                    }
                }
            }

            for (int i = 0; i < h; ++i) {
                for (int j = 0; j < w; ++j) {
                    Console.Write(result[i,j] == 1 ? '█' : ' ');
                }
                Console.Write("\n");
            }
        }
    }
}