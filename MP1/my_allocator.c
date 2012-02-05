/* 
File: my_allocator.c

Author: <your name>
Department of Computer Science
Texas A&M University
Date  : <date>

Modified: 

This file contains the implementation of the module "MY_ALLOCATOR".

*/

/*--------------------------------------------------------------------------*/
/* DEFINES */
/*--------------------------------------------------------------------------*/

/* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* INCLUDES */
/*--------------------------------------------------------------------------*/

#include<stdlib.h>

#include "my_allocator.h"

/*--------------------------------------------------------------------------*/
/* DATA STRUCTURES */ 
/*--------------------------------------------------------------------------*/

/* -- (none) -- */

typedef struct list node;
struct list{
    node* prev;
    node* next;
    int size;
    char inher;
    char lr;
} list;
/*--------------------------------------------------------------------------*/
/* CONSTANTS */
/*--------------------------------------------------------------------------*/

/* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* FORWARDS */
/*--------------------------------------------------------------------------*/

/* -- (none) -- */

/*--------------------------------------------------------------------------*/
/* FUNCTIONS FOR MODULE MY_ALLOCATOR */
/*--------------------------------------------------------------------------*/

/* Don't forget to implement "init_allocator" and "release_allocator"! */
unsigned int blockSize;
node* front[100];
node* last[20];
Addr base;
/*
int reverseFib(int a){ //returns what number fibonacci number it is
int current = 1, last = 0, temp, count = 1;
while(current<=a){
temp = current;
current = last + current;
last = temp;
count ++;
}
return count;
}
*/
int getFib(int a){//returns the number of the accosiated fibnumber
    int current = 1, last = 0, temp, count = 1;
    while(count< a){
        temp = current;
        current = current+last;
        last = temp;
        count++;
    }
    return current;
}
void append(int index, Addr addr, int size, char inher, char lr){

    if(index > 0){
        if(front[index] == NULL){//there are no elemets at the inde
            //printf("\nAppend index %d\n\tfront[index] = %p &front[index] = %p\n\taddr = %p &addr = %p",index,front[index],&front[index],addr,&addr);
            node * n = addr;
            n->next = NULL;
            n->prev = NULL;
            n->size = size;
            n->inher = inher;
            n->lr = lr;
            front[index] = n;
        }
        else{
            //printf("\nAppend index %d\n\tfront[index] = %p &front[index] = %p\n\taddr = %p &addr = %p",index,front[index],&front[index],addr,&addr);
            node * n = addr;
            n->next = front[index];
            n->prev = NULL;
            n->size = size;
            n->inher = inher;
            n->lr = lr;
            front[index]->prev = n;
            front[index] = n;


        }
    }


}
void remove(int index){


    if(front[index]->next == NULL){//only one element in the list
        front[index] = NULL;

    }
    else {

        node *n = front[index]->next;
        n->prev == NULL;
        front[index] = n;
    }

}


int getReturnIndex(int a){//fix to not have to do 3
    int current = 1, last = 0, temp, count = 1;
    while(!((current*blockSize)>a)){
        temp = current;
        current = last + current;
        last = temp;
        count ++;
    }
    return count;
}

int getFib2(int a){
    int current = 1, last = 0, temp;
    while((current+last)<=a){
        temp = current;
        current = current+last;
        last = temp;
    }
    return current;
}

extern Addr my_malloc(unsigned int _length) {
    /* This preliminary implementation simply hands the call over the 
    the C standard library! 
    Of course this needs to be replaced by your implementation.
    */
    int index;
    int x;
    Addr returnReference;
    char * increment; //change in unix version
    printf("length being assigned = %d\n",_length);

    index = getReturnIndex(_length+16);
    printf("started looking in (index) = %d\n",index);
    if(index<2) index = 2;
    while(front[index]==NULL){
        int sizeSmall, sizeBig;
        int indexTemp = index;
        node* n;
        char inher, lr;
        //do one split down
        while(front[indexTemp]==NULL) indexTemp++;
        if(indexTemp < 2) printf("Memory allocation error (memory too small)");

        n = front[indexTemp];
        remove(indexTemp);
        sizeSmall = getFib(indexTemp-2)*blockSize;
        sizeBig = getFib(indexTemp-1)*blockSize;
        increment = n;
        increment += sizeSmall;

        inher = n->inher;
        lr = n->lr;
        //the beginning of the bloc is going to be the smaller of the two sections
        append(indexTemp-2,n,sizeSmall,lr,'l');

        //Set the larger of the children the base pointer + the size of the small child
        append(indexTemp-1,increment, sizeBig,inher,'r');

        printf("%d = %p %d = %p %d= %p\n",indexTemp,front[indexTemp],indexTemp-1,front[indexTemp-1],indexTemp-2,front[indexTemp-2]);

    }
    returnReference = ((char *) front[index])+16;
    remove(index);

    printf("Memory found at %d\n",index);
    //return malloc((size_t)_length);
    return returnReference;
}
void merge(node * l, node *r){
    int size = l->size+r->size;
    int index = getReturnIndex(size)-1;
    append(index,l,size,r->inher,l->inher);

}
int my_freeImpl(Addr a,int num){
    int x, searchLevel;
    char lr;
    node *n = (char *) a - 16;
    node * search;
    node * look;
    lr = n->lr;
    if(n->lr=='l'){
        search = (char *) n + n->size;
        searchLevel = getReturnIndex(n->size);
    }
    else{
        int returnIndex = getReturnIndex(n->size)-2;
        int offset = getFib(returnIndex)*blockSize;
        search = (char *) n - offset;
        searchLevel = returnIndex;
    }

    look = front[searchLevel];
    if(look != NULL){
        do{
            if(look == search){


                if(num>0){
                    if(n->prev!=NULL)n->prev->next = n->next;
                    else front[getReturnIndex(n->size)-1] = n->next;
                    if(n->next!=NULL)n->next->prev = n->prev;
                }
                if(look->prev!=NULL)look->prev->next = look->next;
                else front[searchLevel] = look->next;
                if(look->next!=NULL)look->next->prev = look->prev;
                if(lr=='l'){
                    merge(n,look);
                    return my_freeImpl(((char *) n) + 16,1);
                }
                else{
                    merge(look,n);

                    return my_freeImpl(((char * ) look) + 16,1);
                }
            }
            else{
                look = look->next;

            }
        }
        while(look != NULL);
    }
    //test to see if we should add it to the list
    
    x = getReturnIndex(n->size)-1;
    look = front[x];
    if(look != NULL){
        do{
            if(look == n) return 1;
            else look = look->next;
        }
        while(look!=NULL);
    }
    
    append(x,n,n->size,n->inher,n->lr);

    printf("size of return %d\n",n->size);
    //free(a);

    return 1;
}
extern int my_free(Addr a) {
    /* Same here! */
    return my_freeImpl(a,0);
}
extern unsigned int init_allocator(unsigned int basic_block_size, unsigned int length){
    int indexnum;
    node * n;

    blockSize = basic_block_size;
    base = malloc(length);

    indexnum = getReturnIndex(length)-1;
    n = base;
    n->size = getFib(indexnum)*blockSize;
    n->prev = NULL;
    n->next = NULL;
    n->lr = 'l';
    n->inher = 'l';
    front[indexnum] = n;

    printf("initiated allocator\n");
    printf("size of void %d\n", sizeof(Addr));
    printf("Block Size = %d\n",blockSize);
    printf("initial position of space = %d\n",indexnum);   
    return length;
}
int release_allocator(){
    int x = 0;
	printf("Releasing allocator\n");
	for(x = 0; x<100;x++){
		if(front[x] != NULL) printf("%d\n",x);
	}
    free(base);
    return 0;   
}
