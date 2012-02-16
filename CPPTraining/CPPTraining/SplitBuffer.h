#include <iostream>
#include <string>
using namespace std;
class SplitBuffer
{
	char * buffer;
	char * newBuffer;
	int entryPoint, endPoint, spotAtEndPoint, size;

public:
	SplitBuffer(int x);
	void SetPoint(int setPoint);
	void Add(char charIN);
	void Add(string stringIN);
	char* toString();
	~SplitBuffer();
};