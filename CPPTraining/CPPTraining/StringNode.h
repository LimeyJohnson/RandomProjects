#include <string>
#include "Node.h"
using namespace std;
class StringNode: Node
{
	string s;
public:
	StringNode(string s);
	string toString();
};