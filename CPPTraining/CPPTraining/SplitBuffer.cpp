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
		//memset(&buffer[endPoint], (int) '0',sizeof(char) * diff);
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
		//memset(&buffer[setPoint], (int)'0', sizeof(char) * (endPoint - entryPoint));
	}
}
SplitBuffer::SplitBuffer(int x)
{
	entryPoint = 0;
	endPoint = size = x;
	buffer = new char[x+1];
	newBuffer = NULL;
	memset(&buffer[0],(int) '0', sizeof(char) * size);
	buffer[x] = '\0';
}
void SplitBuffer::Add(char c)
{
	if(entryPoint < endPoint)
	{
		buffer[entryPoint++] = c;
	}
	else
	{
		doubleSize();
		Add(c);
	}
}
void SplitBuffer::Add(string s)
{
	for(unsigned int x = 0; x< s.size(); x++)
	{
		Add(s.at(x));
	}
}
char* SplitBuffer::toString()
{
	if(newBuffer != NULL) delete [] newBuffer;
	newBuffer = new char[size];
	int y =0;
	memcpy(newBuffer, buffer, sizeof(char) * entryPoint);
	memcpy(&newBuffer[entryPoint], &buffer[endPoint], sizeof(char) * (size-endPoint));
	newBuffer[entryPoint + (size - endPoint)] = '\0';
	return newBuffer;
}
SplitBuffer::~SplitBuffer()
{
	delete [] buffer;
	delete [] newBuffer;
}
void SplitBuffer::doubleSize()
{
	int newSize = size*2;
	char * newBuffer = new char[newSize+1];
	newBuffer[newSize] = '\0';
	//memset(newBuffer,'0',sizeof(char) * newSize);
	int newOffSet = newSize - (size - endPoint);
	memcpy(newBuffer, buffer, sizeof(char) * entryPoint);
	memcpy(&newBuffer[newOffSet],&buffer[endPoint],sizeof(char) * (size-endPoint));
	delete [] buffer;
	buffer = newBuffer;
	endPoint = newOffSet;
	size=newSize;
}