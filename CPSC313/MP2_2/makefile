# makefile

all: task.o main

task.o: task.H task.C
	g++ -c task.C

main: task_test_step1.C task.o utils.H
	g++ -o task_test_step1 task_test_step1.C task.o -lpthread 
