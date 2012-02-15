#include <string.h>
#include <iostream>
#include "SplitBuffer.h"

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
	size=x;
	buffer = new char[x];
	for(int x = 0; x < 20; x++)
	{
		buffer[x] = '0';
	}
}
void SplitBuffer::AddChar(char c)
{
	if(entryPoint < endPoint)
	{
		buffer[entryPoint++] = c;
	}
	else
	{
		cerr<<"Buffer is overrun"<<endl;
	}
}
char* SplitBuffer::toString()
{
	char * newBuffer;
	newBuffer = new char[size];
	int y =0;
	for(int x = 0; x< size; x++)
	{
		if(buffer[x] !='0')
		{
			newBuffer[y++] = buffer[x];
		}
	}
	newBuffer[y] = '\0';
	return newBuffer;
}
SplitBuffer::~SplitBuffer()
{
	delete [] buffer;
}