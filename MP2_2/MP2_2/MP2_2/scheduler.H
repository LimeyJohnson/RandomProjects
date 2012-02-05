/* 
    File: scheduler.H

    Author: R. Bettati
            Department of Computer Science
            Texas A&M University
    Date  : 08/02/11

    Definition of the system scheduler.
                                     
*/

#ifndef _scheduler_H_                   // include file only once
#define _scheduler_H_

/*--------------------------------------------------------------------------*/
/* DEFINES */
/*--------------------------------------------------------------------------*/

/* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* INCLUDES */
/*--------------------------------------------------------------------------*/

#include <list>

#include "semaphore.H"
#include "utils.H"
#include "schedulable.H"




/*--------------------------------------------------------------------------*/
/* DATA STRUCTURES */ 
/*--------------------------------------------------------------------------*/

/* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* FORWARDS */ 
/*--------------------------------------------------------------------------*/

/* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* CLASS   S c h e d u l e r  */
/*--------------------------------------------------------------------------*/

class Scheduler  {
private:
  Semaphore      * scheduler_mutex;
  static const int MAX_NAME_LEN = 15;
  char             name[MAX_NAME_LEN]; 
protected:

  Schedulable * current_task;

  /* -- READY QUEUE MANAGEMENT */
  list<Schedulable*> ready_queue;
  /* This is a simple example of a ready queue that could be used for 
     a FIFO scheduler. */

  virtual int           enqueue(Schedulable * _task);
  virtual Schedulable * dequeue();

public:

  /* -- CONSTRUCTOR/DESTRUCTOR */

  Scheduler(char _name[]);

  ~Scheduler();

  /* -- THE CURRENTLY RUNNING TASK */

  Schedulable * Current_Task();
  /* Return a pointer to the currently running task. */
 

  /* -- START SCHEDULING THE TASKS */
  virtual int Kick_Off();

  /* -- SCHEDULING OPERATIONS */

  virtual int Start(Schedulable * _task);
  /* Start scheduling this task. This method is called in method
     Schedulable::Start() that starts execution of the task, more
     specifically, inside method Schedulable::CarrierForRun().
     If return value is not zero, task could not be successfully started. */

  virtual int Stop(Schedulable * _task) {}
  /* Stop scheduling this task. (We don't need to implement it for now.) */

  virtual int Yield();
  /* The calling task gives up the CPU. */

  virtual int Resume(Schedulable * _task);
  /* Resume the given _task. */

};


#endif


