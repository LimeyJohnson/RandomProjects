#include <string>
#include "BinaryTree.h"
#include <functional>
using namespace std;

BinaryTree::BinaryTree()
{

}
void BinaryTree::Initialize(Node<string>* node)
{
	head = node;
}
void BinaryTree::Add(unsigned int id, string valueIn)
{
	Node<string>* newNode = new Node<string>(id, valueIn);
	if (head == NULL)
	{
		newNode->Parent = head;
		Initialize(newNode);
		return;
	}
	Add(head, newNode);

}
void BinaryTree::Add(Node<string>* base, Node<string>* newNode)
{

	if (base->GetID() > newNode->GetID())
	{
		//new node will be left of current node
		if (base->Left == NULL)
		{
			newNode->Parent = base;
			base->Left = newNode;
		}
		else
		{
			Add(base->Left, newNode);
		}
	}
	else if (base->GetID() < newNode->GetID())
	{
		//new node will be right of current node
		if (base->Right == NULL)
		{
			newNode->Parent = base;
			base->Right = newNode;
		}
		else
		{
			Add(base->Right, newNode);
		}
	}
	else
	{
		throw;
	}

}
string BinaryTree::Search(int id)
{
	return Search(id, head);

}
string BinaryTree::Search(int id, Node<string>* base)
{
	if (base != NULL)
	{
		if (base->GetID() == id)
		{
			return base->GetValue();
		}
		Node<string>* nextSearchNode = base->GetID() > id ? base->Left: base->Right;
		return Search(id, nextSearchNode);
	}
	else
	{
		// We have hit the end of the tree and we have not found it. It is not in the tree. Return NULL
		return NULL;
	}
}
void BinaryTree::Rebalance(Node<string>* parent, Node<string>* node)
{
	int leftChildren = CountChildren(node->Left);
	int rightChildren = CountChildren(node->Right);
	if (leftChildren > rightChildren + 1)
	{
		//Left Node will become right node
		if (parent == NULL)
		{
			node->Left->Parent = NULL;
		}
		else
		{
			node->Left->Parent = parent;
			parent->Left = node;
		}
		Add(node->Left, node);
	}
}
int BinaryTree::CountChildren(Node<string>* node)
{
	if (node == NULL)
	{
		return 0;
	}
	return 1 + CountChildren(node->Left) + CountChildren(node->Right);
}
BinaryTree::~BinaryTree()
{
	auto deleteNode = [](Node<string>* node)
	{
		delete node; 
	};
	EvaluatePre(head, deleteNode);
}
void BinaryTree::EvaluatePre(Node<string>* start, void(*func)(Node<string>*))
{
	if (start == NULL) return;
	
	EvaluatePre(start->Left, func);
	EvaluatePre(start->Right, func);
	func(start);
}