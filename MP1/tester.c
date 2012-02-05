#include <stdio.h>
#include "my_allocator.h"




int main(){

    int * begin;
    int * go;
    int * go2;
    int * go3;
    init_allocator(128,19000);
    begin = my_malloc(300);
    printf("\n");
    go = my_malloc(300);
    printf("\n");

    go2 = my_malloc(300);
    printf("\n");

    go3 = my_malloc(300);
    printf("memeory allocated\n");
    my_free(begin);
    my_free(go);
    my_free(go2);
    my_free(go3);
    release_allocator();
    return 0;
}
