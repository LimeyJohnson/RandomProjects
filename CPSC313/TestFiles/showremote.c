#include <vector>
#include <iostream>

using namespace std;
vector<int> *v;
int main()
{
	
	v = new vector<int>;
	v->push_back(2);
	cout<<v->size()<<endl;
}