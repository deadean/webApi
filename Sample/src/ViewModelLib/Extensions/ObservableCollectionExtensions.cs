using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace LizenzaDevelopment.ADAICA.Data.GLOB.ADGLOB.Extensions
{
	/// <summary>
	/// Extension class for ObservableCollection
	/// </summary>
	public static class ObservableCollectionExtensions
	{
		/// <summary>
		/// Swaps the specified collection.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="obj1">The obj1.</param>
		/// <param name="obj2">The obj2.</param>
		public static void Swap<T>(this ObservableCollection<T> collection, T obj1, T obj2)
		{
			if (!(collection.Contains(obj1) && collection.Contains(obj2))) return;
			var indexes = new List<int> { collection.IndexOf(obj1), collection.IndexOf(obj2) };
			if (indexes[0] == indexes[1]) return;
			indexes.Sort();
			var values = new List<T> { collection[indexes[0]], collection[indexes[1]] };
			collection.RemoveAt(indexes[1]);
			collection.RemoveAt(indexes[0]);
			collection.Insert(indexes[0], values[1]);
			collection.Insert(indexes[1], values[0]);
		}

		/// <summary>
		/// Fors the each.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="action">The action.</param>
		public static void ForEach<T>(this ObservableCollection<T> collection, Action<T> action)
		{
			foreach (T item in collection)
				action(item);
		}

		/// <summary>
		/// Removes all.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		public static bool RemoveAll<T>(this ObservableCollection<T> collection, Func<T, bool> predicate)
		{
			if (predicate == null)
				return false;

			for (int i = 0; i < collection.Count(); i++)
			{
				if(predicate.Invoke(collection[i]))
				{
					collection.Remove(collection[i]);
					i--;
				}
			}

			return collection.Count > 0;
		}

	}
}
