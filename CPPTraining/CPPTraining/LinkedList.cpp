#include <string.h>
#include "LinkedList.h"
using namespace std;

LinkedList::LinkedList(char firstChar)
{
	node * firstNode = new node;
	firstNode->data = firstChar;
	firstNode->next = NULL;
	head = firstNode;
	tail = firstNode;
}
void LinkedList::add(char charIN)
{
	node* newNode = new node;
	newNode->data = charIN;
	newNode->next = NULL;
	tail->next = newNode;
	tail = newNode;
}
char LinkedList::at(int x)
{
	node* n = head;
	for(int i = 0; i< x && (n != tail); i++)
	{
		n = n->next;
	}
	return n->data;
}
void LinkedList::remove(int x)
{
	node * n = head;
	for(int i = 0; i < x-1; i++)
	{
		n = n->next;
	}
	node * toDelete = n->next;
	n->next = n->next->next;
	delete toDelete;
	 
}
int LinkedList::size()
{
	int i;
	node* n = head;
	for( i = 1; (n != tail); i++)
	{
		n = n->next;
	}
	return i;
}
char* LinkedList::toString()
{
	int arraySize = size();
	char * returnChar = new char[arraySize+1];
	returnChar[arraySize] = '\0';
	node * n = head;
	int x = 0;
	while(x< arraySize)
	{
		returnChar[x++] = n->data;
		n = n->next;
	}
	return returnChar;
}
LinkedList::~LinkedList()
{
	while(head != tail)
	{
		node * toDelete = head;
		head = head->next;
		delete toDelete;

	}
	delete head;
}
