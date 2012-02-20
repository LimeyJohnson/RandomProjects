#include <iostream>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netdb.h>
#include <arpa/inet.h>
#include <stdio.h>

using namespace std;
class Lookup
{
public:
	string printAddress(string address);
};
string Lookup::printAddress(string address) {
	struct addrinfo hints, *res, *p;
	int status;
	char ipstr[INET6_ADDRSTRLEN];
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
		cout<<ipver<<ipstr<<endl;
	}
	freeaddrinfo(res);
	return ipstr;
}
