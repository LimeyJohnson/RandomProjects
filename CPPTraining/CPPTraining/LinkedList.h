#include <iostream>
#include <string>
#include "Node.h"
using namespace std;

template <class T>
class LinkedList
{
	
	Node<T> * head;
	Node<T> * tail;

public:
	LinkedList(T data);
	void add(T data);
	T at(int x);
	void remove(int x);
	int size();
	T* toString();
	~LinkedList();

	
};
template <class T>
LinkedList<T>::LinkedList(T dataIN)
{
	Node<T> * firstNode = new Node<T>(dataIN);
	firstNode->Next = NULL;
	head = firstNode;
	tail = firstNode;
}
template <class T>
void LinkedList<T>::add(T dataIN)
{
	Node<T>* newNode = new Node<T>(dataIN);
	newNode->Next = NULL;
	tail->Next = newNode;
	tail = newNode;
}
template <class T> 
T LinkedList<T>::at(int x)
{
	Node<T>* n = head;
	for(int i = 0; i< x && (n != tail); i++)
	{
		n = n->Next;
	}
	return n->GetValue();
}
template <class T>
void LinkedList<T>::remove(int x)
{
	Node<T> * n = head;
	for(int i = 0; i < x-1; i++)
	{
		n = n->Next;
	}
	Node<T> * toDelete = n->Next;
	n->Next = n->Next->Next;
	delete toDelete;
	 
}
template <class T>
int LinkedList<T>::size()
{
	int i;
	Node<T>* n = head;
	for( i = 1; (n != tail); i++)
	{
		n = n->Next;
	}
	return i;
}
template <class T>
T* LinkedList<T>::toString()
{
	int arraySize = size();
	T * returnChar = new T[arraySize+1];
	returnChar[arraySize] = '\0';
	Node<T> * n = head;
	int x = 0;
	while(x< arraySize)
	{
		returnChar[x++] = n->data;
		n = n->Next;
	}
	return returnChar;
}
template <class T>
LinkedList<T>::~LinkedList()
{
	while(head != tail)
	{
		Node<T> * toDelete = head;
		head = head->Next;
		delete toDelete;

	}
	delete head;
}
