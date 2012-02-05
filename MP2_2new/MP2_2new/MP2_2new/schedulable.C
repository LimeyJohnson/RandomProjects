#include "schedulable.H"


void * tfuncc(void * args){
	Task * task_instance = (Task *) args;
	task_instance->CarrierForRun();
	return NULL;
}
Schedulable::Schedulable(const char _name[], Scheduler * _sched): Task(_name)
{
    sched = _sched;
    Semaphore sem(1);
    block_semaphore = &sem;
}
Schedulable::~Schedulable()
{
   delete block_semaphore;
}

int Schedulable::Start()
{

 pthread_create( &thread_id, NULL, tfuncc, this );
	
	
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
    std::cout<<"CarrierForRun()"<<std::endl;
    Run();
}

