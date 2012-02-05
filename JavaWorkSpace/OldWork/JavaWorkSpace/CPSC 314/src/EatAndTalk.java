
interface Mouth<T> {
	void eat(T t);
}
class EatAndTalk implements Mouth<Object> {
	public void eat(Object s) {
		System.out.println("I ate " + s);
	}
}

