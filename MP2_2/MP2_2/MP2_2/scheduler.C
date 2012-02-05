#include "scheduler.H"

Scheduler::Scheduler(char _name[])
{
    for(int x = 0; x<MAX_NAME_LEN;x++)
    {
        name[x] = _name[x];
    }
    scheduler_mutex = new Semaphore(1);

}
int Scheduler::enqueue(Schedulable *_task)
{
    scheduler_mutex->P();
    ready_queue.push_back(_task);
    scheduler_mutex->V();
    return 0;
}
Schedulable * Scheduler::dequeue()
{
    Schedulable * result;
    
    scheduler_mutex->P();
    result = ready_queue.front();
    ready_queue.pop_front();
    scheduler_mutex->V();

    return result;
}
int Scheduler::Kick_Off()
{
    current_task=dequeue;
    current_task->Unblock();
    return 1;
}
Schedulable * Scheduler::Current_Task()
{
    return current_task;
}
int Scheduler::Start(Schedulable *_task)
{
   enqueue(_task);
   _task->Block();
	return 1;
}
int Scheduler::Resume(Schedulable *_task)
{
    enqueue(_task);
    return 1;
}
int Scheduler::Yield()
{
   current_task->Block();
   while(ready_queue.empty());
   current_task = dequeue();
   current_task->Unblock();
   
   
}

