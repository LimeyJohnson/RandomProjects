#include <iostream>
#include "task.H"

using namespace std;

class RudderController : public Task {
public:
  RudderController(char _name[]) : Task(_name) {}

  void Run() {
    cout << "Rudder Controller [" << name << "] running\n" << flush;
    for (int i = 0; i < 100; i++) {
      cout << name << " waiting for next sensor input i="<<i<<"\n" << flush;
      usleep(1000000);
      cout << name << " issueing rudder control command\n" << flush;
      usleep(10000);
    }
  }
};

class AvionicsTask : public Task {
public:
  AvionicsTask(char _name[]) : Task(_name) {}

  void Run() {
    cout << "Avionics System [" << name << "] running\n" << flush;
    for (int i = 0; i < 100; i++) {
      cout << name << " waiting for next refresh interval i="<<i<<"\n" << flush;
      usleep(700000);
      cout << name << " refreshing avionics screen\n" << flush;
      usleep(12000);
    }
  }
};
class PrintAndrew : public Task {
public:
	PrintAndrew(char _name[]) : Task(_name){}
	void Run() {
		for(int i = 0; i<1000; i++){
		cout<<"Andrew is the greatest and this is running at real time i="<<i<<endl;
		}
	}
};


int main(int argc, char * argv[]) {

  /* -- CREATE TASKS */
  RudderController task1("rudder control");
  AvionicsTask     task2("avionics task");
	PrintAndrew task3("Andrew");
  /* -- LAUNCH TASKS */
  task1.Start();
  task2.Start();
  task3.Start();

  
  Task::GracefullyExitMainThread();
}
