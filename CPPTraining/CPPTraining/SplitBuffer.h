#include <iostream>
#include <string>
using namespace std;
class SplitBuffer
{
	char * buffer;
	int firstPoint, entryPoint, endPoint, size;

public:
	SplitBuffer(int x);
	void SetPoint(int setPoint);
	void AddChar(char charIN);
	char* toString();
	~SplitBuffer();
};