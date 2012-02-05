/* 
    File: ackerman.h

    Author: R. Bettati
            Department of Computer Science
            Texas A&M University
    Date  : 08/02/08

    Header file for the ackerman function.
    This function is to be called as part of the "memtest" program
    in MP1 for CPSC 313.
    (All this header business is rather silly for this single function...)
*/

#ifndef _ackerman_h_                              /* include file only once */
#define _ackerman_h_

/*--------------------------------------------------------------------------*/
/* DATA STRUCTURES */ 
/*--------------------------------------------------------------------------*/

    /* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* CONSTANTS */
/*--------------------------------------------------------------------------*/

    /* -- (none) -- */


/*--------------------------------------------------------------------------*/
/* FORWARDS */
/*--------------------------------------------------------------------------*/

    /* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* MODULE ackerman */
/*--------------------------------------------------------------------------*/

extern void ackerman_main();
/* Asks user for parameters n and m and computes the result of the
   (highly recursive!) ackerman function. During every recursion step,
   it allocates and de-allocates a portion of memory with the use of the
   memory allocator defined in module "my_allocator.H".
*/ 

#endif
