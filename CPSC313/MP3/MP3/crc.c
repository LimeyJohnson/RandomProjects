#include <ctype.h>
#include <netdb.h>
#include <string>
#include <errno.h>
#include <unistd.h>
#include <arpa/inet.h>
#include <signal.h>
#include <netinet/in.h>
#include <netdb.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <sys/select.h>
#include <iostream>
#include <pthread.h>
#include <stdlib.h>
#include <stdio.h>
using namespace std;

#define SERVER_PORT 16925
#define BUFFER_LENGTH 250
#define FALSE 0
#define SERVER_NAME "unix.cs.tamu.edu"
static int u_ignore_sigpipe();
int u_connect(int port, char *hostn);
int name2addr(char *name, in_addr_t *addrp);
void * portListener (void * args)
{//Listens on a the socket for any communication and prints it to the screen
//if the socket is done recieving it closes the socket
	cout<<"Port listener created"<<endl;
	char buffer[BUFFER_LENGTH];
	int rc;	
	int sock = *(int *) args;
	while(true)
	{
		memset(buffer, '\0', sizeof(buffer));
		rc = recv(sock,&buffer,sizeof(buffer),0);
		if(rc==-1)
		{
			cout<<"recv was -1 exiting listenter"<<endl;
			break;
		}
		else if(rc>0)
		{
			cout<<"Message Recieved: "<<buffer<<endl;
			if(buffer[0] == '*')
			{
				close(sock);
				break;
			}
		}
	}
	
}
void openChatRoom(int port, char server[])
{//Initiates communication for the chat room. It adds all the necessary information do the main vector
//connects to the server on the given port and creates a port listener. It then sends anything it reviecs from
// getline across the socket to the server
	int sd=-1, rc;
	char buffer[BUFFER_LENGTH];
	pthread_t th;
	int * socketPointer = new int;
	fd_set read_fd;
	memset(buffer, '\0', sizeof(buffer));

	sd = u_connect(port,server);

	* socketPointer = sd;
	pthread_create(&th,NULL,portListener,(void *) socketPointer);
	for(;;)
	{//Loop until socket closes
		cout<<"Message to send: ";
		cin.getline(buffer,sizeof(buffer));
		string bString(buffer);
		if(bString.compare("QUIT")==0)	break;
		rc = send(sd,buffer,sizeof(buffer),0);
		if(rc<1)
		{
			cout<<"Server has shutdown"<<endl;
			break;
		}

	}
	if (sd != -1)
		close(sd);
	free(socketPointer);
}
int main(int argc, char *argv[]) {
/*
Opens a connection to the server and sends commands. If the command is returned with a '*' appended to the front it starts a chat server
*/
	int sd=-1, rc;
	char buffer[BUFFER_LENGTH];
	char rbuf[BUFFER_LENGTH];
	char server[1000];
	struct sockaddr_in serveraddr;
	struct hostent *hostp;

	if (argc > 1) strcpy(server, argv[1]);
	else strcpy(server, SERVER_NAME);
	

	for(;;)
	{
		sd = u_connect(SERVER_PORT,server);
		memset(buffer, '\0', sizeof(buffer));

		cout<<"command to send: ";
		cin.getline(buffer,sizeof(buffer));
		string bString(buffer);
		
		if(bString.compare("QUIT")==0) break; // Quit the program
		rc = send(sd, buffer, sizeof(buffer), 0);
		cout<<"Sent command (awaiting response) "<<endl;

		rc = recv(sd,&rbuf,sizeof(rbuf),0);
		if(rbuf[0] == '*') //open a chat room
		{
			string s(rbuf);
			int port = atoi(s.substr(1).c_str());
			cout<<"Opening chat room on port "<<port<<endl;
			openChatRoom(port,server);
		}
		else cout<<"response recieved: "<<rbuf<<endl;

	}
	if (sd != -1)
		close(sd);
	cout<<"Exiting"<<endl;
	return 1;
}
//Connect to a given port and hostname
int u_connect(int port, char *hostn) {
	int error;
	int retval;
	struct sockaddr_in server;
	int sock;
	fd_set sockset;

	if (name2addr(hostn,&(server.sin_addr.s_addr)) == -1) {
		errno = EINVAL;
		return -1;
	}
	server.sin_port = htons((short)port);
	server.sin_family = AF_INET;

	if ((u_ignore_sigpipe() == -1) ||
		((sock = socket(AF_INET, SOCK_STREAM, 0)) == -1))
		return -1;

	if (((retval =
		connect(sock, (struct sockaddr *)&server, sizeof(server))) == -1) &&
		((errno == EINTR) || (errno == EALREADY))) {          /* asynchronous */
			FD_ZERO(&sockset);
			FD_SET(sock, &sockset);
			while (((retval = select(sock+1, NULL, &sockset, NULL, NULL)) == -1)
				&& (errno == EINTR)) {
					FD_ZERO(&sockset);
					FD_SET(sock, &sockset);
			}
	}
	if (retval == -1) {
		error = errno;
		while ((close(sock) == -1) && (errno == EINTR));
		errno = error;
		return -1;
	}
	return sock;
}
static int u_ignore_sigpipe() {
	struct sigaction act;

	if (sigaction(SIGPIPE, (struct sigaction *)NULL, &act) == -1)
		return -1;
	if (act.sa_handler == SIG_DFL) {
		act.sa_handler = SIG_IGN;
		if (sigaction(SIGPIPE, &act, (struct sigaction *)NULL) == -1)
			return -1;
	}   
	return 0;
}

int name2addr(char *name, in_addr_t *addrp) {
//get address from a name
	struct hostent *hp;

	if (isdigit((int)(*name)))
		*addrp = inet_addr(name);
	else {
		hp = gethostbyname(name);
		if (hp == NULL)
			return -1;
		memcpy((char *)addrp, hp->h_addr_list[0], hp->h_length);
	}
	return 0;
}
