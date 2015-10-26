#include <iostream>
#include <string.h>
#include <stdio.h>
#include <WinSock2.h>
#include <WS2tcpip.h>
using namespace std;
class ChatClient
{
public:
	ChatClient(string url, char* port);
	int login(string name);
private:
	struct addrinfo* resolveAddress();
	bool connectToHost();
	string HostUrl;
	char* HostPort;
	int sockfd;
};
