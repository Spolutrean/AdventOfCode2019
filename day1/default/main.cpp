#include <iostream>

using namespace std;

int main() {
    freopen("in.txt", "r", stdin);
    long long ans = 0;
    int n = 100;
    for(int i = 0; i < n; ++i) {
        int a;
        cin >> a;
        ans += (a / 3) - 2;
    }

    cout << ans;
}
