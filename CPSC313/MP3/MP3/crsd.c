#include <ctype.h>
#include <netdb.h>
#include <string>
#include <errno.h>
#include <unistd.h>
#include <arpa/inet.h>
#include <netinet/in.h>
#include <sys/socket.h>
#include <sys/select.h>
#include <sys/types.h>
#include <signal.h>
#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>
#include <sstream>
#include <vector>

using namespace std;


#define SERVER_PORT 16925
#define BUFFER_LENGTH 250
#define MAXBACKLOG 50
#define FALSE 0
int u_open(int port);
int u_accept(int fd);
static int u_ignore_sigpipe();
int PORT = SERVER_PORT;
typedef struct client
{ //Holds information on a given client, inlcuding they thread ID
	string Rname;
	int sock;
	pthread_t tid;
} client;
typedef struct room
{//Holds information on a given room
	int mSock;
	string name;
	vector<client> * clients;
	pthread_t threadId;
}room;
static vector<room> rooms; // MAIN data holder of the entire program

int getClientIndex(room r, int sock)
{// Find the index of a given client in a given room
	for(int x =0 ;x<r.clients->size();x++)
	{
		if(r.clients->at(x).sock == sock) return x;
	}
	return -1;
}
room exists(string _name)
{//Check if the room already exists, if it does return it, If not create a room and return it
	int size = rooms.size();
	for(int x = 0; x<size;x++)
	{
		if(_name.compare(rooms.at(x).name)==0) return rooms.at(x);
	}

	room r;
	return r;
}
void sendMessage(int sock, string msg)
{// Send the given message across the socket
	char buffer[BUFFER_LENGTH];
	memset(buffer, '\0', sizeof(buffer));
	strcpy(buffer,msg.c_str());
	cout<<"Message send "<<buffer<<endl;
	send(sock,buffer,sizeof(buffer),0);
}
string itos(int i)	// convert int to string
{//convert an int to a string
	stringstream s;
	s << i;
	return s.str();
}
void * handleClient(void * args)
{//Handles an individual client. It listens on the socket for its client for messages
// when it recieves on it sends it out to everyone in the room
//If the socket is closed then removes the client from the room and terminates
	int rc = 1;
	cout<<"Thread created for client"<<endl;
	client *c = (client *) args;
	char buffer[BUFFER_LENGTH];
	c->tid = pthread_self();
	room r = exists(c->Rname);
	r.clients->push_back(*c);
	cout<<"Client created on thread "<<c->tid<<endl;
	
	string welcomeMessage("Welcome to chatroom "+r.name+"\nThere are "+itos(r.clients->size())+" users connected");
	sendMessage(c->sock,welcomeMessage);
	while(rc>0)
	{
		rc = recv(c->sock,&buffer,sizeof(buffer),0);
		{
			for(int x=0; x<r.clients->size();x++)
			{
				if(rc>0)
				{
					if(r.clients->at(x).sock!=c->sock)
					{
						send(r.clients->at(x).sock,buffer,sizeof(buffer),0);
					}
				}
				else if(r.clients->at(x).sock == c->sock)
				{
					cout<<"removing client from chat room "<<r.name<<endl;
					
					close(r.clients->at(x).sock);
					pthread_cancel(r.clients->at(x).tid);
					r.clients->erase(r.clients->begin()+x);
				}
			}
		}
	}
}
void * handleEvent(void * args)
{// Handles the Commands. If the command is to create a Chat room, this becomes the chat room

	int sd=-1, sd2=-1;
	int rc, length, on=1;
	char buffer[BUFFER_LENGTH];
	memset(buffer, '\0', sizeof(buffer));
	struct timeval timeout;

	struct sockaddr_in serveraddr;
	fd_set read_fd;
	int sock = *(int *) args;

	recv(sock, &buffer, sizeof(buffer), 0);
	string command(buffer);

	if(command.find("CREATE")!=string::npos)
	{//Create a chat room 
		if(command.length()>7)
		{
			pthread_t th;
			cout<<"Thread command recieved successfully"<<endl;
			string rName = command.substr(7);
			room r = exists(rName);
			if(r.name.compare(rName)==0)// Room already exists
			{
				string response("Room already Exists on port: "+itos(r.mSock));
				sendMessage(sock,response);
			}
			else
			{//Create the server
				//sleep(10);
				r.name=rName;r.mSock=++PORT; r.clients = new vector<client>; r.threadId = pthread_self();
				rooms.push_back(r);

				if((sd = u_open(r.mSock))==-1) cout<<"Chat room failed open"<<endl;
                //Open new port
				string response("Room "+command.substr(7)+" Created sucessfully on port "+itos(r.mSock));
				cout<<response<<endl;
				sendMessage(sock,response);
				cout<<"Room "<<command.substr(7)<<" on thread id " << pthread_self()<<endl;
				while(true)
				{//Accept new clients and give them a thread
					cout<<"room waiting for messages"<<endl; 
					sd2 = u_accept(sd);
					client c; c.Rname=r.name; c.sock=sd2;
					client * clientPointer = new client;
					* clientPointer = c;
					pthread_create(&th,NULL,handleClient,(void *)clientPointer);
					
				}
			}
		}
		else sendMessage(sock,"CREATE command does not have enough arguments");
	}

	else if( command.find("JOIN")!=string::npos)
	{//Send the "8" command to the client along with the port number
		if(command.length()>5)
		{
			string rName = command.substr(5);
			room r = exists(rName);
			if(r.name.compare(rName)==0) //room exists
			{

				sendMessage(sock,"*"+itos(r.mSock));

			}
			else sendMessage(sock,"Room does not exist, please CREATE first");
		}
		else sendMessage(sock,"Command not correct");
	}
	else if( command.find("DELETE")!=string::npos)
	{//Delete a chat room
		if(command.length()>7)
		{
			string rName = command.substr(7);
			room r = exists(rName);
			if(r.name.compare(rName)==0)
			{//room exists
				for(int y = 0; y<r.clients->size();y++)
				{//Kill all client listening threads and close sockets
					sendMessage(r.clients->at(y).sock, "***Chat server is being deleted, shutting down server**");
					pthread_cancel(r.clients->at(y).tid);
					cout<<"Just killed "<<r.clients->at(y).tid<<endl;
					close(r.clients->at(y).sock);
				}
				pthread_cancel(r.threadId);
				close(r.mSock);
				cout<<"Just killed "<<r.threadId<<endl;
				for(int x= 0; x<rooms.size();x++)
				{//free memory and delete from vector
					if(rooms.at(x).name.compare(rName)==0)
					{
						free(rooms.at(x).clients);
						rooms.erase(rooms.begin()+x);
					}
				} 
				sendMessage(sock,"Room deleted");
			}else sendMessage(sock,"Room does not exists");
		}else sendMessage(sock, "Delete command incorrect");
	}else sendMessage(sock, "Command "+command+" not understood");
	close (sock);
}

int main() {
    //Open the master port and listen for command. Spawn new threads for each command recieved
    //
	int sd=-1, sd2=-1;
	int rc, length, on=1;
	int * socketPointer = new int;
	fd_set read_fd;
	struct timeval timeout;
	struct sockaddr_in serveraddr;
	pthread_t th; pthread_attr_t ta;
	pthread_attr_init(&ta);
	pthread_attr_setdetachstate(&ta, PTHREAD_CREATE_DETACHED);

	if ((sd = u_open(SERVER_PORT)) ==-1){
		cout<<"Failed to connect to port"<<endl;
		return 1;
	}
	printf("Ready for client connect().\n");



	while(true)
	{
		sd2 = u_accept(sd);
		
		* socketPointer = sd2;
		pthread_create(&th,&ta,handleEvent,(void *)socketPointer);
	}

	free(socketPointer);
	//free(rooms);
	if (sd != -1)
		close(sd);
	if (sd2 != -1)
		close(sd2);

	return 1;
}
int u_open(int port) {// Open port
	int error;  
	struct sockaddr_in server;
	int sock;
	int truef = 1;

	if ((u_ignore_sigpipe() == -1) ||
		((sock = socket(AF_INET, SOCK_STREAM, 0)) == -1))
		return -1; 

	server.sin_family = AF_INET;
	server.sin_addr.s_addr = htonl(INADDR_ANY);
	server.sin_port = htons(port);
	if ((bind(sock, (struct sockaddr *)&server, sizeof(server)) == -1) ||
		(listen(sock, MAXBACKLOG) == -1)) {
			error = errno;
			while ((close(sock) == -1) && (errno == EINTR)); 
			errno = error;
			return -1;
	}
	return sock;
}
int u_accept(int fd) {//accept incoming connection
	
	int retval;
	while (((retval = accept(fd, NULL,NULL)) == -1) &&  (errno == EINTR));

	return retval;
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
