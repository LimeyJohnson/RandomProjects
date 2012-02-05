#include "task.H"

  static std::list<pthread_t *>  threads;
void * tfunc(void * args){
	Task * task_instance = (Task *) args;
	task_instance->Run();
	
	return NULL;
	
}
int Task::Start(){
	pthread_create( &thread_id, NULL, tfunc, this );
	
	cout<<"Thread Started"<<endl;
	return 1;
}
char * Task:: Name(){
threads.push_back(&thread_id);
	return name;
}
void Task::GracefullyExitMainThread(){
	while(threads.size()>0){
	pthread_t temp = *threads.front();
	 pthread_join(temp,NULL);
	 threads.pop_front();
	 }
	cout<<"Exiting"<<endl;
}
Task::~Task(){};

void Task::CarrierForRun()
{
  Run();
 }