# makefile

all: task_test_step3

task.o: task.H task.C
	g++ -c -g task.C

timer.o: timer.H timer.C
	g++ -c -g timer.C

semaphore.o: semaphore.H semaphore.C
	g++ -c -g semaphore.C

schedulable.o: schedulable.H scheduler.H task.H semaphore.H schedulable.C
	g++ -c -g schedulable.C

scheduler.o: scheduler.H task.H schedulable.H scheduler.C
	g++ -c -g scheduler.C

rrscheduler.o: rrscheduler.H task.H scheduler.H schedulable.H rrscheduler.C
	g++ -c -g rrscheduler.C

task_test_step3: task_test_step3.C task.o timer.o schedulable.o scheduler.o \
                 semaphore.o rrscheduler.o utils.H
	g++ -o task_test_step3 task_test_step3.C task.o timer.o schedulable.o \
               scheduler.o semaphore.o rrscheduler.o -lpthread 
