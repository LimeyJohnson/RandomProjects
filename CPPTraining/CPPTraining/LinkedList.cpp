#include <string.h>
#include "LinkedList.h"
using namespace std;

template <class T>
LinkedList<T>::LinkedList(T dataIN)
{
	node * firstNode = new node;
	firstNode->data = dataIN;
	firstNode->next = NULL;
	head = firstNode;
	tail = firstNode;
}
template <class T>
void LinkedList<T>::add(T dataIN)
{
	node* newNode = new node;
	newNode->data = dataIN;
	newNode->next = NULL;
	tail->next = newNode;
	tail = newNode;
}
template <class T> 
T LinkedList<T>::at(int x)
{
	node* n = head;
	for(int i = 0; i< x && (n != tail); i++)
	{
		n = n->next;
	}
	return n->data;
}
template <class T>
void LinkedList<T>::remove(int x)
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
template <class T>
int LinkedList<T>::size()
{
	int i;
	node* n = head;
	for( i = 1; (n != tail); i++)
	{
		n = n->next;
	}
	return i;
}
template <class T>
T* LinkedList<T>::toString()
{
	int arraySize = size();
	T * returnChar = new T[arraySize+1];
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
template <class T>
LinkedList<T>::~LinkedList()
{
	while(head != tail)
	{
		node * toDelete = head;
		head = head->next;
		delete toDelete;

	}
	delete head;
}
