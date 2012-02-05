#include <iostream>
#include <sys/types.h>
#include <unistd.h>
#include <sys/wait.h>

using namespace std;

main(int argc, char* argv[]){
	string s = argv[1];
	int count = s.size()-1;
	pid_t parentPID;
	pid_t pID;
	while(count>=0){
		pID = fork();
		if(pID == 0){
			cout<<s[count];
			break;
		}
		else{
			count --;
			wait(NULL);
		}
	}
	if(pID != 0)cout<<endl;
}
