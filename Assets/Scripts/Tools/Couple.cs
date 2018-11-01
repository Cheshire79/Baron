
namespace Baron.Tools
{
	public class Couple<T, K>
	{
		public T First { get; private set; }
		public K Second { get; private set; }
		public Couple(T first, K second)
		{
			First = first;
			Second = second;
		}
	}
}
