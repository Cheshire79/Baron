using Baron.View;
using System;

namespace Baron.Tools
{
	public interface IInstancesCache
	{
		T Get<T>() where T : TestableView;
		T Get<T>(Predicate<TestableView> comparer) where T : TestableView;
		void Put(TestableView instance);
		void Clear();
	}
}
