#include <string.h>
using namespace std;

template <class T>
class Node
{
	T m_value;
	unsigned int m_id;
public:
	string toString();
	T GetValue();
	unsigned int GetID();
	Node(T);
	Node(int ID, T);

};
template <class T>
Node<T>::Node(T valueIn)
{
	m_value = valueIn;
}

template <class T>
Node<T>::Node(int ID, T valueIn) :Node<T>(valueIn)
{
	m_id = ID;
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