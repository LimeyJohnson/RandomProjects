#include <iostream>
#include <string>
#include "splitbuffer.h"
#include "linkedlist.h"
#include "netmethod.h"
#include "Node.h"
#include "StringNode.h"
using namespace std;


int find(int x){
    int count=0;
    while(x>0){
        if(x&0x1)count++;
        x = x>>1;
    }
    return count;

}
void testSplitBuffer()
{
	SplitBuffer* sb = new SplitBuffer(20);
	sb->Add("I want to add a lot of text here to show that even the new buffer should be overrun creating the need to double the buffer more than once. ");
	sb->SetPoint(0);
	cout<< sb->toString()<<endl;
	sb->Add("To test this part of the code I am going to add this sentence before the other sentence so that the buffer really has to work hard to figure out what is going on. ");
	sb->SetPoint(0);
	sb->Add("To be or not to be that is the question. I never understood why that was the question, it seemed a bit like a tautology to me. The thing we need to be sure of is that we still manage to overrun the buffer one more time, or this string trying to overrun the other string just isn't going to work.");
	sb->SetPoint(41);
	sb->Add("This setence needs to come right after to be or not to be sentence. ");
	cout<< sb->toString()<<endl ;
	delete sb;
}
void testLinkedList()
{
//	LinkedList<char> ll('A');
//	ll.add('n');
//	ll.add('d');
//	ll.add('z');
//	ll.add('r');
//	ll.add('e');
//	ll.add('w');
//	ll.remove(3);
//	cout<<ll.size()<<endl;
//	cout<<ll.at(3)<<endl;
//	cout<<ll.toString()<<endl;
//Lookup LU
//cout<<LU.printAddress("nwrpca.org")<< " is the address for " << "nwrpca.org" <<endl;
//cout<<LU.printAddress("learn.nwrpca.org")<< " is the address for "<< "learn.nwrpca.org" <<endl;
	LinkedList<Node> list(StringNode("batman"));
}
int testNetMethods()
{
	NetMethods nm;
	nm.a_accept("2000");
	return 1;
}
int main(){
    //find(20);
    testLinkedList();
	
	return 1;

}
