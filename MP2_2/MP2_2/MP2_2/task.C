#include "task.H"

using namespace std;


void * tfunc(void * args){
	Task * task_instance = (Task *) args;
	task_instance->Run();
	return NULL;
}
int Task::Start(){
	pthread_create( &thread_id, NULL, tfunc, this );
	threads[num++] = thread_id;
	cout<<"Thread Started"<<endl;
	return 1;
}
char * Task:: Name(){
	return name;
}
void Task::GracefullyExitMainThread(){
	for(int x = 0; x<num;x++) pthread_join(threads[x],NULL);
	cout<<"Exiting"<<endl;
}
Task::~Task(){};
