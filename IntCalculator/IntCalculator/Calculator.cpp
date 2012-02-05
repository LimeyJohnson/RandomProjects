#include<iostream>
using namespace std;
int main()
{
	int x = 11;
	int count = 0;
	while(x>0)
	{
		if(x & 0x0001 ) count ++;
		x = x >> 1;
	}
	cout<<count<<endl;
}