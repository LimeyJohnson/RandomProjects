#include <iostream>
#include <string>
using namespace std;
typedef struct node
	{
		char data;
		node * next;
	} node;
class LinkedList
{
	node * head;
	node * tail;

public:
	LinkedList(char firstChar);
	void add(char charIN);
	char at(int x);
	void remove(int x);
	int size();
	char* toString();
	~LinkedList();

	
};