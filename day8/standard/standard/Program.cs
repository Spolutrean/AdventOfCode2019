using System;
using System.IO;

namespace standard {
    internal class Program {
        public static void Main(string[] args) {
            String s = File.ReadAllText("/home/spolutrean/adventofcode2019/day8/standard/standard/in.txt");
            //9117554426
            int ans = 0;
            int mn = 26;
            int perLayer = 6 * 25;
            int layersCount = s.Length / perLayer;
            for (int i = 0; i < layersCount; ++i) {
                int[] cnt = new int[3] {0, 0, 0};
                for (int j = 0; j < perLayer; ++j) {
                    cnt[s[i * perLayer + j] - '0']++;
                }

                if (cnt[0] < mn) {
                    mn = cnt[0];
                    ans = cnt[1] * cnt[2];
                }
            }
            Console.WriteLine(ans);
        }
    }
}