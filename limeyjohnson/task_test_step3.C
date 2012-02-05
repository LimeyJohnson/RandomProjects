/* TEST PROGRAM FOR STEP 3 of MP2 */

#include <iostream>


#include "task.H"
#include "schedulable.H"
#include "rrscheduler.H"

#include "timer.H"

using namespace std;

/* --------------------------------------------------------------------------*/
/* Class DISK */
/* --------------------------------------------------------------------------*/

class Disk {
  /* This class simulates the behavior of a disk device. 
     The program issues a disk operation (the class supports only
     read() operations), then self suspends (just like with a real disk,) and 
     waits for a interrupt to know that operation completed. 
     In this implementation, we emulate the I/O hardware interrupt by
     a timer interrupt, which fires at the end of the "disk operation".
     When the timer fires, the task is resumed, and the disk operation returns.
  */
private:

  class DiskOperationTimer : public Timer {
  private:
    Disk        * disk;
    Schedulable * task;
  public:
    DiskOperationTimer(Disk * _disk, Schedulable * _task) : Timer() {
      disk = _disk;
      task = _task;
    }
    virtual void Handle_Event() {
      printf("END-OF-EVENT TIMER to indicate END OF DISK OPERATION (disk %s)\n", 
	     disk->name);
      /* -- DISK OPERATION COMPLETED. RESUME TASK. */
      disk->sched->Resume(task);
    }
  };

  int          latency;   /* latency of disk operation in usec.          */
  Scheduler  * sched;     /* need a pointer to scheduler to resume task. */
  char         name[80];  /* give the disk a name!                       */

public:

  Disk(char _name[], int _latency, Scheduler * _sched) {
    strncpy(name, _name, 80);
    latency = _latency;
    sched   = _sched;
  }

  void read() {
    DiskOperationTimer t(this, sched->Current_Task());
    /* -- HERE WE WOULD SEND THE COMMANDS TO THE DISK, AND THE DISK WOULD
          RETURN WITH AN INTERRUPT WHEN IT IS DONE. WE SIMULATE THIS BY 
          SETTING A TIMER. */
    t.Set(latency);
    /* -- GIVE UP THE CPU */
    sched->Yield();
    /* -- THE TASK WILL BE RESUMED HERE AFTER THE TIMER FIRES. */
  }

};

/* --------------------------------------------------------------------------*/
/* HERE COME THE TASKS */
/* --------------------------------------------------------------------------*/

/* -- THIS TASK DOES A LOT OF DISK ACCESSES. */
class IOBoundTask : public Schedulable {
private:
  Disk disk;
public:
  IOBoundTask(char _name[], Scheduler * _sched) : 
    Schedulable(_name, _sched), disk("disk", 100000, _sched) {}

  /* This task has a very slow disk, at 100msec latency. */

  void Run() {
    cout << "IOBoundTask [" << name << "] running\n" << flush;
    for (int i = 0; i < 1000; i++) {
      if (i % 20 == 1) {
	cout << name << " reading data from disk\n" << flush;
        disk.read();
      }
      cout << name << " doing some computation " << i << "\n" << flush;
      usleep(10000);
    }
    cout << "IOBoundTask [" << name << "] done!\n" << flush;
  }
};

/* -- WE KNOW THIS TASK FROM EARLIER. */
class AvionicsTask : public Schedulable {
public:
  AvionicsTask(char _name[], Scheduler * _sched) : 
    Schedulable(_name, _sched) {}

  void Run() {
    cout << "Avionics System [" << name << "] running\n" << flush;
    cout << "STOP WITH ^C\n" << flush;

    for (int i = 0; i < 100000; i++) {
      cout << name << " waiting for next refresh interval\n" << flush;
      if (i % 10 == 1) {
	sched->Resume(this);
	sched->Yield();
      }
      cout << name << " refreshing avionics screen\n" << flush;
      usleep(12000);
    }
    cout << "Avionics System [" << name << "] done!\n" << flush;
  }
};

/* --------------------------------------------------------------------------*/
/* MAIN PROGRAM */
/* --------------------------------------------------------------------------*/

int main(int argc, char * argv[]) {

  /* -- INITIALIZE THE TIMER "CLOCK" TO WHATEVER INTERVAL IS APPROPRIATE. */
  Timer::Init(500000);

  /* -- CREATE SCHEDULER */
  RRScheduler * system_scheduler = new RRScheduler("scheduler", 4000000);

  /* -- CREATE TASKS */
  IOBoundTask  task1("io task",       system_scheduler);
  AvionicsTask task2("avionics task", system_scheduler);

  /* -- LAUNCH TASKS */
  task1.Start();
  task2.Start();

  cout << "end of launch" << flush;

  /* -- MAKE SURE THAT EVERYBODY IS SAFELY STARTED (CLUDGE!) */
  usleep(100000);

  /* -- START SCHEDULING */
  system_scheduler->Kick_Off();

  //  for(;;)
  //    pause();

  /* -- Have the parent thread get out of the way. */
  Task::GracefullyExitMainThread();
}
