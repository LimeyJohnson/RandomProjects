#include <stdio.h>
#include "my_allocator.h"



void fnExit(void){
	release_allocator();
}

int main(int argc, char ** argv){
	atexit(fnExit);
	int c;
	int blockSize = 128;
	int memorySize = 512000;
	while((c = getopt(argc,argv,"b:s:"))!=-1){
		switch(c){
	case 's':
		memorySize = atoi(optarg);
		break;
	case 'b':
		blockSize = atoi(optarg);
		break;
	case '?':
		if (optopt == 'c')
			fprintf (stderr, "Option -%c requires an argument.\n", optopt);
		else if (isprint (optopt))
			fprintf (stderr, "Unknown option `-%c'.\n", optopt);
		else
			fprintf (stderr,
			"Unknown option character `\\x%x'.\n",
			optopt);
		return 1;
	default:
		abort ();
		break;
		}
	}

	printf("initiating with -b = %d and -s = %d",blockSize,memorySize);
	init_allocator(blockSize,memorySize);

	ackerman_main();
	
	return 0;
}
