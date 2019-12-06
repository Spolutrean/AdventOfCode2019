#include <iostream>
#include <vector>
#include <string>
#include <map>

using namespace std;

map<string, vector<string>> g;
map<string, int> depth;

void dfs(string s, int d = 0) {
    depth[s] = d;
    for(int i = 0; i < g[s].size(); ++i) {
        dfs(g[s][i], d + 1);
    }
}

int main() {
    freopen("in.txt", "r", stdin);
    string s;
    while(cin >> s) {
        bool left = true;
        string u = "", v = "";
        for(int i = 0; i < s.size(); ++i) {
            if(s[i] == ')') {
                left = false;
            } else {
                if(left) {
                    u += s[i];
                } else {
                    v += s[i];
                }
            }
        }
        g[u].push_back(v);
    }
    dfs("COM");
    int ans = 0;
    for(pair<string, int> v : depth) {
        //cout << v.first << " " << v.second << "\n";
        ans += v.second;
    }
    cout << ans;
    return 0;
}
