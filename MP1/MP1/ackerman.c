/* 
File: ackerman.c

Author: R. Bettati
Department of Computer Science
Texas A&M University
Date  : 08/02/09

This file implements the function "ackerman(n,m)", which is used 
by the "memtest" program in MP1. 

*/

/*--------------------------------------------------------------------------*/
/* DEFINES */
/*--------------------------------------------------------------------------*/

/* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* INCLUDES */
/*--------------------------------------------------------------------------*/

#include<stdlib.h>
#include<stdio.h>
#include <system32/time.h>
#include<assert.h>

#include "my_allocator.h"

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

int ackerman(int a, int b);
/* used in "ackerman_main" */

void print_time_diff(struct timeval * tp1, struct timeval * tp2);
/* used in "ackerman" */

/*--------------------------------------------------------------------------*/
/* LOCAL VARIABLES */
/*--------------------------------------------------------------------------*/


unsigned long int num_allocations;


/*--------------------------------------------------------------------------*/
/* EXPORTED FUNCTIONS */
/*--------------------------------------------------------------------------*/

extern void ackerman_main() {
    /* This is function repeatedly asks the user for the two parameters
    "n" and "m" to pass to the ackerman function, and invokes the function.
    Before and after the invocation of the ackerman function, the 
    value of the wallclock is taken, and the elapsed time for the computation
    of the ackerman function is output.
    */

    int n, m; /* Parameter for the invocation of the Ackerman function. */ 

    struct timeval tp_start; /* Used to compute elapsed time. */
    struct timeval tp_end;

    for (;;) {

        num_allocations = 0;

        printf("\n");
        printf("Please enter parameters n and m to ackerman function.\n");
        printf("Note that this function takes a long time to compute,\n");
        printf("even for small values. Keep n at or below 3, and mat or\n");
        printf("below 8. Otherwise, the function takes seemingly forever.\n");
        printf("Enter 0 for either n or m in order to exit.\n\n");

        printf("  n = "); scanf("%d", &n);
        if (!n) break;
        printf("  m = "); scanf("%d", &m);
        if (!m) break;

        printf("      n = %d, m = %d\n", n, m);

        assert(gettimeofday(&tp_start, 0) == 0);
        /* Assert aborts if there is a problem with gettimeofday.
        We rather die of a horrible death rather than returning
        invalid timing information! 
        */

        int result = ackerman(n, m);

        assert(gettimeofday(&tp_end, 0) == 0);
        /* (see above) */

        printf("Result of ackerman(%d, %d): %d\n", n, m, result); 

        printf("Time taken for computation : "); 
        print_time_diff(&tp_start, &tp_end);
        printf("\n");

        printf("Number of allocate/free cycles: %d\n\n\n", num_allocations); 
    }

    printf("Reached end of Ackerman program. Thank you for using it.\n");

}

/*--------------------------------------------------------------------------*/
/* LOCAL FUNCTIONS */
/*--------------------------------------------------------------------------*/

void print_time_diff(struct timeval * tp1, struct timeval * tp2) {
    /* Prints to stdout the difference, in seconds and museconds, between two
    timevals. */

    long sec = tp2->tv_sec - tp1->tv_sec;
    long musec = tp2->tv_usec - tp1->tv_usec;
    if (musec < 0) {
        musec += 1000000;
        sec--;
    }
    printf(" [sec = %ld, musec = %ld] ", sec, musec);

}



int ackerman(int a, int b) {
    /* This is the implementation of the Ackerman function. The function itself is very
    function is very simple (just two recursive calls). We use it to exercise the
    memory allocator (see "my_alloc" and "my_free"). 
    For this, there are additional calls to "gettimeofday" to measure the elapsed
    time.
    */

    void * mem;

    /* The size "to_alloc" of the region to allocate is computed randomly: */
    int to_alloc =  ((2 << (rand() % 19)) * (rand() % 100)) / 100;
    if  (to_alloc < 4) to_alloc = 4;

    int result = 0;

    mem = my_malloc(to_alloc * sizeof(char));

    num_allocations++;

    if (mem != NULL) {

        if (a == 0)
            result = b + 1;
        else if (b == 0)
            result = ackerman(a - 1, 1);
        else
            result = ackerman(a - 1, ackerman(a, b - 1) );

        my_free(mem);
    }

    return result;
}

