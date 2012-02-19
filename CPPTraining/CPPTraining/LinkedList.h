#include <iostream>
#include <string>
using namespace std;

template <class T>
class LinkedList
{
	typedef struct node
	{
		T data;
		node * next;
	} node;
	node * head;
	node * tail;

public:
	LinkedList(T data);
	void add(T data);
	T at(int x);
	void remove(int x);
	int size();
	T* toString();
	~LinkedList();

	
};