#include "task.H"
#include <iostream>
using namespace std;

 static std::vector<pthread_t> threads;
void * tfunc(void * args){
	Task * task_instance = (Task *) args;
	task_instance->Run();
	return NULL;
}
void Task::Start(){
	pthread_create( &thread_id, NULL, tfunc, this );
	threads.push_back(thread_id);
	cout<<"Thread Started"<<endl;
}
char * Task:: Name(){
	return name;
}
void Task::GracefullyExitMainThread(){
	for(int x = 0; x<threads.size();x++) pthread_join(threads[x],NULL);
	cout<<"Exiting"<<endl;
}
Task::~Task(){};
