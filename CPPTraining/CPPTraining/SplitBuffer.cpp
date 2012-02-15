#include <string.h>
#include <iostream>
#include "SplitBuffer.h"
#define BUFFERSIZE = 20

using namespace std;

void SplitBuffer::SetPoint(int setPoint)
{
	if(setPoint > entryPoint)
	{

	}
	if(setPoint < entryPoint)
	{
		int diff = entryPoint - setPoint;
		endPoint = endPoint - diff;
		memcpy(&buffer[endPoint],&buffer[setPoint],sizeof(char) * diff);	

	}
	entryPoint = setPoint;
}
SplitBuffer::SplitBuffer(int x)
{
	firstPoint = entryPoint = 0;
	endPoint = 20;
	for(int x = 0; x < 20; x++)
	{
		buffer[x] = '0';
	}
}
void SplitBuffer::AddChar(char c)
{
	if(entryPoint < 20)
	{
		buffer[entryPoint++] = c;
	}
	else
	{
		cerr<<"Buffer is overrun"<<endl;
	}
}
string SplitBuffer::toString()
{
	string str = buffer;
	return str;
}