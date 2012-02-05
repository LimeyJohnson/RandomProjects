/* 
    File: timer.H

    Author: R. Bettati
            Department of Computer Science
            Texas A&M University
    Date  : 08/03/25

*/

#ifndef _timer_H_                   // include file only once
#define _timer_H_

/*--------------------------------------------------------------------------*/
/* DEFINES */
/*--------------------------------------------------------------------------*/

    /* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* INCLUDES */
/*--------------------------------------------------------------------------*/

    /* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* DATA STRUCTURES */ 
/*--------------------------------------------------------------------------*/

    /* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* FORWARDS */ 
/*--------------------------------------------------------------------------*/

    /* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* CLASS   T i m e r  */
/*--------------------------------------------------------------------------*/

class Timer {
private:
  static long clock_interval; /* in musec */

  static bool compare_timers(const Timer * _t1, const Timer * _t2);
  /* Compares whether timer _t1 is earlier than timer _t2. This function
     may come in handy, for example, whenever timers need to be inserted into
     an ordered queue. */
     
  static void * TimerHandlerThread(void * args);
  /* This function is called by the separate thread that handles the low-level
     timer interrupts and checks whether timers events have expired and
     need to be executed. */

  /* WHEN SHOULD THE TIMER FIRE? */
  timeval tv;

public:

  static int Init(long _clock_interval_musec);
  /* Initialize the timer handling. This should be called by the main 
     thread early on, certainly before it creates the first timers.
     An implementation could look as follows: First, the main thread masks the
     SIGALARM (or SIGVTALARM) signals; this should be done before any 
     tasks are created, in order for the tasks to have the signals masked. 
     Then, method "Init()" would create a new timer thread, which would 
     process the requests in the timer queue. For this thread, the 
     signal would be unblocked. */
 
  Timer();
  /* Construct timer that calles "Event_Handler" whenever it fires. */

  virtual void Set(long _imusec);
  /* Set this timer to fire after time '_imusec' from now. */

  virtual int Clear();
  /* Cancel the timer. */

  virtual void Handle_Event() = 0;
  /* This function is called when the timer expires. */

};

#endif


