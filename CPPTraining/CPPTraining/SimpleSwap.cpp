#include <iostream>
#include <string>
#include "SplitBuffer.h"
using namespace std;


int find(int x){
    int count=0;
    while(x>0){
        if(x&0x1)count++;
        x = x>>1;
    }
    return count;

}
int main(){
    //cout<<find(13)<<endl;
    
	SplitBuffer* sb = new SplitBuffer(20);
	/*sb->Add("abc");
	puts(sb->toString());
	sb->SetPoint(0);
	sb->Add("xyz");
	puts(sb->toString());
	sb->SetPoint(5);
	sb->Add("mmm");
	puts(sb->toString());*/
	sb->Add("I want to add a lot of text here to show that even the new buffer should be overrun creating the need to double the buffer more than once. ");
	sb->SetPoint(0);
	cout<< sb->toString()<<endl ;
	sb->Add("To test this part of the code I am going to add this sentence before the other sentence so that the buffer really has to work hard to figure out what is going on. ");
	sb->SetPoint(0);
	sb->Add("To be or not to be that is the question. I never understood why that was the question, it seemed a bit like a tautology to me. The thing we need to be sure of is that we still manage to overrun the buffer one more time, or this string trying to overrun the other string just isn't going to work.");
	sb->SetPoint(41);
	sb->Add("This setence needs to come right after to be or not to be sentence. ");
	cout<< sb->toString()<<endl ;
	/*sb->Add('y');
	
	sb->SetPoint(4);
	sb->Add('X');
	puts(sb->toString());*/
	delete sb;
	return 1;

}