#include <string>
#include "StringNode.h"
using namespace std;

StringNode::StringNode(string str)
{
	s=str;
}

string StringNode::toString()
{
	return s;
}