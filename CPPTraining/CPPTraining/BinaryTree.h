#include <string.h>
#include "Node.h"
using namespace std;
#pragma once

class BinaryTree
{
	Node<string>* head = NULL;
	
public:
	BinaryTree();
	~BinaryTree();
	void Add(unsigned int id, string valuein);
	string Search(int id);

private:
	void Initialize(Node<string>* node);
	void Add(Node<string>* base, Node<string>* newNode);
	void Rebalance(Node<string>* parent, Node<string>* start);
	int CountChildren(Node<string>* node);
	string Search(int id, Node<string>* base);
	void EvaluatePre(Node<string>* start, void(*func)(Node<string>*));
	
};