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
	sb->AddChar('a');
	sb->AddChar('b');
	sb->AddChar('c');
	sb->AddChar('d');
	sb->AddChar('e');
	sb->AddChar('f');
	sb->SetPoint(2);
	sb->AddChar('g');
	sb->AddChar('h');
	sb->AddChar('i');
	string str = sb->toString();
	cout << str <<endl;
	delete sb;
	return 1;

}