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
	sb->Add("abc");
	puts(sb->toString());
	sb->SetPoint(0);
	sb->Add("xyz");
	puts(sb->toString());
	sb->SetPoint(5);
	sb->Add("mmm");
	puts(sb->toString());
	/*sb->Add('y');
	
	sb->SetPoint(4);
	sb->Add('X');
	puts(sb->toString());*/
	delete sb;
	return 1;

}