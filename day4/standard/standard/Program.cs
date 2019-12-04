using System;

namespace standard {
    internal class Program {
        public static bool check(int x) {
            string s = x.ToString();
            bool okEquals = false, okGreater = true;
            for (int i = 1; i < s.Length; ++i) {
                okEquals |= s[i] == s[i - 1];
                okGreater &= Convert.ToInt32(s[i]) >= Convert.ToInt32(s[i-1]);
            }

            return okEquals && okGreater;
        }
        
        public static void Main(string[] args) {
            int l = 172930, r = 683082;
            int cnt = 0;
            for (int x = l; x <= r; ++x) {
                cnt += (check(x) ? 1 : 0);
            }
            Console.WriteLine(cnt);
        }
    }
}