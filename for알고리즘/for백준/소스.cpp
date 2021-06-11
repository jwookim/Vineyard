#include <iostream>
#include <string>
using namespace std;


string* Draw_Star(int num)
{
	string* star = new string[num];

	if (num == 1)
	{
		star[0] += "*";
		return star;
	}

	string* temp;


	temp = Draw_Star(num / 3);

	for (int i = 0; i < num; i++)
	{
		star[i] += temp[i % 3];

		star[i] += temp[i % 3];

		star[i] += temp[i % 3];
	}
}


int main()
{
	int num;
	string* star;
	cin >> num;

	star = Draw_Star(num);

	for (int i = 0; i < _msize(star) / sizeof(star); i++)
		cout << star[i] << endl;
	return 0;
}