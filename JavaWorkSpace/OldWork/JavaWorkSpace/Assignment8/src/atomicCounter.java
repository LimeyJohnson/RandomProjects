import java.util.concurrent.atomic.*;
public class atomicCounter {
AtomicLong max = new AtomicLong();

	long max(long l){
		if(l>max.get())max.getAndSet(l);
		return max.get();
	}
}


