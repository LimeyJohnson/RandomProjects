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
    current_task = dequeue();
    current_task->Unblock();
    return 1;
}
Schedulable * Scheduler::Current_Task()
{
    return current_task;
}
int Scheduler::Start(Schedulable *_task)
{
    scheduler_mutex->P();
        enqueue(_task);
    _task->Block();
    scheduler_mutex->V();

    return 1;
}
int Scheduler::Resume(Schedulable *_task)
{
    scheduler_mutex->P();
    enqueue(_task);
    scheduler_mutex->V();
    return 1;
}
int Scheduler::Yield()
{

    scheduler_mutex->P();
    current_task->Block();
    if(ready_queue.size()>0)
    { 
        current_task = dequeue();
        current_task->Unblock();
    }
    scheduler_mutex->V();

}

