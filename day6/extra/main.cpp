#include <iostream>
#include <vector>
#include <string>
#include <map>

using namespace std;

map<string, vector<string>> g;
map<string, int> depth;
map<string, bool> used;

void dfs(string s, int d = 0) {
    used[s] = true;
    depth[s] = d;
    for(int i = 0; i < g[s].size(); ++i) {
        string to = g[s][i];
        if(!used[to]) {
            dfs(to, d + 1);
        }
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
        g[v].push_back(u);
    }
    dfs("YOU");
    cout << depth["SAN"] - 2;
    return 0;
}
