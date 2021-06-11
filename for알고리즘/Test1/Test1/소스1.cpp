#include <string>
#include <vector>
#include <algorithm>

using namespace std;

int solution(int cacheSize, vector<string> cities) {
    vector<string> cache;

    int answer = 0;

    
    vector<string>::iterator iter, iter2;

    for (iter = cities.begin(); iter != cities.end(); iter++)
    {
        bool check = false;


        for (iter2 = cache.begin(); iter2 != cache.end(); iter2++)
        {
            if (*iter2 == (*iter).)
            {
                answer += 1;
                check = true;
                cache.erase(iter2);
                break;
            }
        }

        if (!check)
        {
            if (cache.size() >= cacheSize)
                cache.erase(cache.begin());
            answer += 5;
        }

        cache.push_back(*iter);

    }
    return answer;
}