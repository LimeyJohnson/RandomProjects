#include <string.h>
using namespace std;
#pragma once
template <class T>
class Node
{
	T m_value;
	unsigned int m_id;
public:
	string toString();
	T GetValue();
	unsigned int GetID();
	Node(T valueIn);
	Node(unsigned int ID, T valueIn);
	Node<T>* Next = NULL;
	Node<T>* Left = NULL;
	Node<T>* Right = NULL;
	Node<T>* Parent = NULL;

};
template <class T>
Node<T>::Node(T valueIn)
{
	m_value = valueIn;
}

template <class T>
Node<T>::Node(unsigned int id, T valueIn) :Node(valueIn)
{
	m_id = id;
}

template <class T>
T Node<T>::GetValue()
{
	return m_value;
}

template <class T>
unsigned int Node<T>::GetID()
{
	return m_id;
}

template <class T>
string Node<T>::toString(){
	return m_value;
}