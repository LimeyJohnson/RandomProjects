#include "netmethod.h"
#include <string>
using namespace std;
string NetMethods::printAddress(string address) {
	WSADATA wsaData;
	struct addrinfo hints, *res, *p;
	int status;
	char ipstr[INET6_ADDRSTRLEN];

	WSAStartup(MAKEWORD(2,0), &wsaData);

	memset(&hints, 0, sizeof hints);
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	if((status = getaddrinfo(address.c_str(), NULL, &hints, &res)) != 0){
		cout << "an error has occured";
		return "FAILED";
	}
	for(p = res; p != NULL; p = p->ai_next){
		void *addr;
		string ipver;
		struct sockaddr_in *ipv4 = (struct sockaddr_in *)p->ai_addr;
		addr = &(ipv4->sin_addr);
		ipver = "IPv4";
		inet_ntop(p->ai_family, addr, ipstr, sizeof ipstr);
	}
	WSACleanup();
	return ipstr;
}
int NetMethods::a_accept(char* port)
{
	struct sockaddr_storage their_addr;
	socklen_t addr_size;
	struct addrinfo hints, *res;
	int sockfd, new_fd, status;
	char* response = "<html><head><title>This Worked</title></head><body>This is working</body></html>";
	char * acceptance = new char[10000];
	memset(&hints,0,sizeof hints);

	WSADATA wsaData;
	WSAStartup(MAKEWORD(2,0), &wsaData);

	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_flags = AI_PASSIVE;
	if((status = getaddrinfo(NULL, port, &hints, &res)) != 0){
		cout << "an error has occured"<<endl;
		cout<< WSAGetLastError() <<endl;
		return -1;
	}


	sockfd = socket(res->ai_family, res->ai_socktype, res->ai_protocol);
	bind(sockfd, res->ai_addr, res->ai_addrlen);
	listen(sockfd, 10);
	while(1)
	{
		addr_size = sizeof their_addr;
		new_fd = accept(sockfd, (struct sockaddr *)&their_addr, &addr_size);
		recv(sockfd, acceptance, sizeof acceptance,0);
		cout<<"recv message"<<endl;
		cout<<acceptance<<endl;
		send(sockfd, response, sizeof response, 0);
		cout<<"sent message"<<endl;
	}
	WSACleanup();
	return 1;
}