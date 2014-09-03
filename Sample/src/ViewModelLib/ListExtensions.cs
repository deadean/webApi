using System.Windows;
using System.Windows.Threading;

namespace LizenzaDevelopment.ADAICA.Data.GLOB.ADGLOB.Extensions
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;

	public static class ListExtensions
	{
		private static Action EmptyDelegate = delegate() { };

		public static void AddRange<T>(this IList<T> source, IEnumerable<T> collection)
		{
			List<T> list = source as List<T>;
			if (list != null)
			{
				list.AddRange(collection);
			}
			else
			{
				foreach (T item in collection)
				{
					source.Add(item);
				}
			}
		}

		/// <summary>
		/// Refreshes the specified UI element.
		/// </summary>
		/// <param name="uiElement">The UI element.</param>
		public static void Refresh(this UIElement uiElement)
		{
			uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
		}
	}
}
