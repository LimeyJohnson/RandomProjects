#include <iostream>
#include <string.h>
#include <stdio.h>
#include <WinSock2.h>
#include <WS2tcpip.h>
using namespace std;
class NetMethods
{
public:
	string printAddress(string address);
	int a_accept(char* port);
};


