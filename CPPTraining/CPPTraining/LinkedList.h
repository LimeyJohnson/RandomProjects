#include <iostream>
#include <string>
using namespace std;

template <class T>
 struct node
	{
		T data;
		node * next;
	} ;

template <class T>
class LinkedList
{
	
	node<T> * head;
	node<T> * tail;

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
	node<T> * firstNode = new node<T>;
	firstNode->data = dataIN;
	firstNode->next = NULL;
	head = firstNode;
	tail = firstNode;
}
template <class T>
void LinkedList<T>::add(T dataIN)
{
	node<T>* newNode = new node<T>;
	newNode->data = dataIN;
	newNode->next = NULL;
	tail->next = newNode;
	tail = newNode;
}
template <class T> 
T LinkedList<T>::at(int x)
{
	node<T>* n = head;
	for(int i = 0; i< x && (n != tail); i++)
	{
		n = n->next;
	}
	return n->data;
}
template <class T>
void LinkedList<T>::remove(int x)
{
	node<T> * n = head;
	for(int i = 0; i < x-1; i++)
	{
		n = n->next;
	}
	node<T> * toDelete = n->next;
	n->next = n->next->next;
	delete toDelete;
	 
}
template <class T>
int LinkedList<T>::size()
{
	int i;
	node<T>* n = head;
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
	node<T> * n = head;
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
		node<T> * toDelete = head;
		head = head->next;
		delete toDelete;

	}
	delete head;
}
