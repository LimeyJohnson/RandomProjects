#include <vector>
#include <iostream>
using namespace std;

void main(){
	vector<int> hello;
	hello.push_back(1);

	while(hello.empty()){
		cout<<hello.pop_back();
	}
}