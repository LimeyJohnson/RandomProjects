/* 
    File: schedulable.H

    Author: R. Bettati
            Department of Computer Science
            Texas A&M University
    Date  : 08/02/11

    Definition of Schedulable Tasks. 
                                     
*/

#ifndef _schedulable_H_                   // include file only once
#define _schedulable_H_

/*--------------------------------------------------------------------------*/
/* DEFINES */
/*--------------------------------------------------------------------------*/

/* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* INCLUDES */
/*--------------------------------------------------------------------------*/
#include <iostream>


#include "semaphore.H"
#include "task.H"



/*--------------------------------------------------------------------------*/
/* DATA STRUCTURES */ 
/*--------------------------------------------------------------------------*/

/* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* FORWARDS */ 
/*--------------------------------------------------------------------------*/

/* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* CLASS   S c h e d u l a b l e */
/*--------------------------------------------------------------------------*/

class Schedulable : public Task {
private:
  Semaphore * block_semaphore;

protected:

  friend class Scheduler;
  Scheduler * sched;

  
  /* Get the task ready to run call the "Run()" method. For example, 
     put the task into the scheduler ready queue before running, and
     give up the CPU after the "Run()" method returns. */

  /* -- SUSPEND EXECUTION */
  virtual int Block();
  virtual int Unblock();
 /* These two methods simulate the suspension and resumption of execution
     of the task. In a real system, the scheduler would move off the task
     from the CPU. We cannot do this here, so we force the task to suspend
     execution by having it "Block". The task resumes execution with "Unblock". */

public:
virtual void CarrierForRun();
  /* -- CONSTRUCTOR/DESTRUCTOR */

  Schedulable(const char _name[], Scheduler * _sched);
  /* Create and initialize the new schedulable task. */

  ~Schedulable();

  /* -- TASK OPERATIONS */
  
  virtual int Start(); 
  /* Start the execution of the task. Calls "Scheduler::Start()" to hand over
     execution of the task to the scheduler.to start execution of task.
     This method is typically called shortly after the task has been created. */

  virtual void Run() = 0;
  /* The method that is executed when the task object is started.
     When the method returns, the thread can be terminated.
     The method returns 0 if no error. */
    
};


#endif


