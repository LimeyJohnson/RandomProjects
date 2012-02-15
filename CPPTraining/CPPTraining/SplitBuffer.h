#include <iostream>
#include <string>
using namespace std;
class SplitBuffer
{
	char buffer[20];
	int firstPoint, entryPoint, endPoint;

public:
	SplitBuffer(int x);
	void SetPoint(int setPoint);
	void AddChar(char charIN);
	string toString();
};