#include "schedulable.H"


Schedulable::Schedulable(const char _name[], Scheduler * _sched): Task(_name)
{
    sched = _sched;
    block_semaphore = new Semaphore(1);
}
Schedulable::~Schedulable()
{
   delete block_semaphore;
}

int Schedulable::Start()
{

 pthread_create( thread_id, NULL, tfunc, this );
	threads[num++] = thread_id;
	std::cout<<"Thread Started"<<std::endl;
   return 0; 
}
int Schedulable::Block()
{
    
    block_semaphore->P();
}
int Schedulable::Unblock()
{
    block_semaphore->V();
}
void Schedulable::CarrierForRun()
{
    cout<<"CarrierForRun()"<<endl;
    Run();
}
void * tfunc(void * args){
	Task * task_instance = (Task *) args;
	task_instance->CarrierForRun();
	return NULL;
}