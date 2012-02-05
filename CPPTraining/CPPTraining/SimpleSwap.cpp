#include <iostream>
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
    cout<<find(13)<<endl;
    return 1;
}