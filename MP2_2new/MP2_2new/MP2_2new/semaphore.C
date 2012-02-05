#include "semaphore.H"
Semaphore::Semaphore(int _val)
{
	value = _val;
	pthread_mutex_init(&m,NULL);
	pthread_cond_init(&c,NULL);
	
}
int Semaphore::P() 
{
	pthread_mutex_lock(&m);
	while(value<1) pthread_cond_wait(&c,&m);
	value--;
	pthread_mutex_unlock(&m);
}
int Semaphore::V()
{
	pthread_mutex_lock(&m);
	value++;
	pthread_mutex_unlock(&m);
}
Semaphore::~Semaphore()
{
}


