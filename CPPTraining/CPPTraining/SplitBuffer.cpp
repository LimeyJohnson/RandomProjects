#include <string.h>
#include <iostream>
#include "SplitBuffer.h"

using namespace std;

void SplitBuffer::SetPoint(int setPoint)
{
	if(setPoint > entryPoint)
	{
		int diff = setPoint - entryPoint;
		memcpy(&buffer[entryPoint], &buffer[endPoint], sizeof(char) * diff);
		memset(&buffer[endPoint], (int) '0',sizeof(char) * diff);
		entryPoint = setPoint;
		endPoint = endPoint + diff;
	}
	if(setPoint < entryPoint)
	{
		int diff = entryPoint - setPoint;
		spotAtEndPoint = setPoint;
		endPoint = endPoint - diff;
		entryPoint = setPoint;
		memcpy(&buffer[endPoint],&buffer[setPoint],sizeof(char) * diff);	
		memset(&buffer[setPoint], (int)'0', sizeof(char) * diff);
	}
	
}
SplitBuffer::SplitBuffer(int x)
{
	entryPoint = 0;
	endPoint = 20;
	size=x;
	buffer = new char[x];
	memset(&buffer[0],(int) '0', sizeof(char) * size);
}
void SplitBuffer::Add(char c)
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
void SplitBuffer::Add(string s)
{
	for(int x = 0; x< s.size(); x++)
	{
		Add(s.at(x));
	}
}
char* SplitBuffer::toString()
{
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
	delete [] newBuffer;
}