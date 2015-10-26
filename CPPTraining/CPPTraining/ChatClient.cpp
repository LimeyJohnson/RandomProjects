#include "ChatClient.h"
#include <string>
using namespace std;

ChatClient::ChatClient(string url, char* port)
{
	HostUrl = url;
	HostPort = port;
	WSADATA wsaData;
	WSAStartup(MAKEWORD(2, 0), &wsaData);
	connectToHost();
}
bool ChatClient::connectToHost()
{
	if (HostUrl.empty())
	{
		cout << "HostURL is empty" << endl;
		return 0;
	}

	struct addrinfo *res = resolveAddress();
	sockfd = socket(res->ai_family, res->ai_socktype, res->ai_protocol);
	if (connect(sockfd, res->ai_addr, res->ai_addrlen) == -1)
	{
		cout << WSAGetLastError() << endl;
	}
}
struct addrinfo* ChatClient::resolveAddress()
{
	struct sockaddr_storage their_addr;
	socklen_t addr_size;
	struct addrinfo hints, *res;
	int status;
	memset(&hints, 0, sizeof hints);

	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_flags = AI_PASSIVE;
	if ((status = getaddrinfo(HostUrl.c_str(), HostPort, &hints, &res)) != 0)
	{
		cout << "an error has occured" << endl;
		cout << WSAGetLastError() << endl;
		return NULL;
	}
	return res;
}
int ChatClient::login(string name)
{
	cout << "logging in" << endl;
	string message = "42['add user':'" + name + "']";
	const char *msg = message.c_str();
	int len = strlen(msg);
	int bytes_sent = send(sockfd, msg, len, 0);
	int error = errno;
	cout << "bytes sent" << bytes_sent << endl;
	return 1;
}
	