using Baron.View;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Baron.Tools
{
	public class InstancesCache : IInstancesCache
	{
		private const string CacheGOName = "Cache";
		private readonly LinkedList<TestableView> _items = new LinkedList<TestableView>();
		private readonly AbstractViewFactory _viewFactory;
		private GameObject _cacheGO;

		public InstancesCache(AbstractViewFactory viewFactory)
		{
			_viewFactory = viewFactory;
		}

		private GameObject CacheGO
		{
			get { return _cacheGO ?? (_cacheGO = GameObject.Find(CacheGOName) ?? new GameObject(CacheGOName)); }
		}

		/// <summary>
		///     Get instance of given prefab from cache
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="prefab"></param>
		/// <returns></returns>
		public T Get<T>() where T : TestableView
		{
			return Get<T>(go => true);
		}

		public T Get<T>(Predicate<TestableView> comparer) where T : TestableView
		{
			if (comparer == null) throw new ArgumentNullException("comparer");

			TestableView item = _items.Where(go => go is T).FirstOrDefault(go => comparer(go));
			if (item == null)
			{
				item = _viewFactory.CreateView<T>();
			}
			else
				_items.Remove(item);

			item.transform.parent = null;
			return (T)item;
		}

		/// <summary>
		///     Put instance to cache
		/// </summary>
		/// <param name="instance"></param>
		public void Put(TestableView instance)
		{
			if (instance == null) throw new ArgumentNullException("instance");

			_items.AddFirst(instance);
			instance.transform.parent = CacheGO.transform;
		}

		public void Clear()
		{
			_items.Clear();
		}

		public static GameObject GetCacheHolder()
		{
			return GameObject.Find(CacheGOName) ?? new GameObject(CacheGOName);
		}
	}
}
