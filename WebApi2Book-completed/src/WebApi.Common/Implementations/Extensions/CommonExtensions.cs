namespace WebApi.Common.Implementations.Extensions
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Diagnostics;
	using System.Globalization;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using WebApi.Common.Implementations.Logging;
	using WebApi.Common.Interfaces.Logging;

	/// <summary>
	/// Common Extensions
	/// </summary>
	public static class CommonExtensions
	{
		/// <summary>
		/// Adds element to linked list before first entry that satisfy predicate
		/// </summary>
		/// <typeparam name="TSource">Souse list element type</typeparam>
		/// <param name="source">Source list</param>
		/// <param name="predicate">Predicate to determine entry to insert before</param>
		/// <param name="item">Item to add to list</param>
		public static void AddBeforeFirst<TSource>(this LinkedList<TSource> source, Func<TSource, bool> predicate, TSource item)
		{
			if (source == null || predicate == null)
				return;

			var node = source.FindFirst(predicate);

			if (node == null)
			{
				source.AddFirst(item);
			}
			else
			{
				source.AddBefore(node, item);
			}
		}

		[DebuggerStepThrough]
		public static TValue As<TValue>(this object value)
			where TValue : class
		{
			if (value == null)
				return default(TValue);

			return value as TValue;
		}

		/// <summary>
		/// Converts single element to enumerable
		/// </summary>
		/// <typeparam name="TValue">Element type</typeparam>
		/// <param name="value">Value to convert</param>
		/// <returns>Enumerable with single item</returns>
		[DebuggerStepThrough]
		public static IEnumerable<TValue> AsEnumerable<TValue>(this object value)
			where TValue : class
		{
			if (value == null)
				yield break;

			var converterValue = value as TValue;

			if (converterValue == null)
				yield break;

			yield return converterValue;
		}

		/// <summary>
		/// Converts single element to enumerable
		/// </summary>
		/// <typeparam name="TValue">Element type</typeparam>
		/// <param name="value">Value to convert</param>
		/// <returns>Enumerable with single item</returns>
		[DebuggerStepThrough]
		public static IEnumerable<TValue> AsEnumerable<TValue>(this TValue value)
			where TValue : class
		{
			if (value == null)
				yield break;

			yield return value;
		}

		/// <summary>
		/// Converts single element to list
		/// </summary>
		/// <typeparam name="TValue">Element type</typeparam>
		/// <param name="value">Value to convert</param>
		/// <returns>if element has value - list with single item: otherwise - empty list</returns>
		[DebuggerStepThrough]
		public static IList<TValue> AsList<TValue>(this TValue value)
			where TValue : class
		{
			return value == null
				? new List<TValue>()
				: new List<TValue> { value };
		}

		/// <summary>
		/// Seeks first item in source collection that satisfies predicate
		/// </summary>
		/// <typeparam name="TSource">Source collection element type</typeparam>
		/// <param name="source">Source collection</param>
		/// <param name="predicate">Predicate to check</param>
		/// <returns>Element if source containts any element that satisfies predicate; otherwise - <c><see cref="T:TSource"/> default value</c></returns>
		public static TSource Find<TSource>(this LinkedList<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null)
				return default(TSource);

			for (var node = source.First; node != null; node = node.Next)
			{
				if (predicate(node.Value))
					return node.Value;
			}

			return default(TSource);
		}

		/// <summary>
		/// Seeks first item in source collection that satisfies predicate
		/// </summary>
		/// <typeparam name="TSource">Source collection element type</typeparam>
		/// <param name="source">Source collection</param>
		/// <param name="predicate">Predicate to check</param>
		/// <returns>Element if source containts any element that satisfies predicate; otherwise - <c><see cref="T:TSource"/> default value</c></returns>
		public static LinkedListNode<TSource> FindFirst<TSource>(this LinkedList<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null)
				return null;

			for (var node = source.First; node != null; node = node.Next)
			{
				if (predicate(node.Value))
					return node;
			}

			return null;
		}

		/// <summary>
		/// Gets property name from simple LINQ expression
		/// </summary>
		/// <typeparam name="TSource">Source object type</typeparam>
		/// <typeparam name="TKey">Source object property value type</typeparam>
		/// <param name="expression">Expression to retrieve property name from</param>
		/// <returns>if expression valid - property name; otherwise <c>string.Empty</c></returns>
		[DebuggerStepThrough]
		public static string GetPropertyName<TSource, TKey>(this Expression<Func<TSource, TKey>> expression)
		{
			if (expression == null)
				return string.Empty;

			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression == null)
				return string.Empty;

			return memberExpression.Member.Name;
		}

		/// <summary>
		/// Gets value from object by specified property name
		/// </summary>
		/// <typeparam name="TResult">Property value type</typeparam>
		/// <param name="source">Source object to get value from</param>
		/// <param name="propertyName">Property name to retrieve value for</param>
		/// <returns>if property name exist in object - property value; otherwise - <c><see cref="T:TResult"/> default value</c></returns>
		public static TResult GetValue<TResult>(this object source, string propertyName)
		{
			if (source == null || string.IsNullOrEmpty(propertyName))
				return default(TResult);

			var property = source.GetType().GetProperty(propertyName);

			if (property == null)
				return default(TResult);

			return (TResult)property.GetValue(source, BindingFlags.GetProperty, null, null, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Checks if specified object has a reference to some data
		/// </summary>
		/// <param name="sourceObject">Source object to check</param>
		/// <returns><c>true</c> if source object has reference to some data; otherwise - <c>false</c></returns>
		[DebuggerStepThrough]
		public static bool HasValue(this object sourceObject)
		{
			return sourceObject != null;
		}

		/// <summary>
		/// Sets property value to specified target
		/// </summary>
		/// <param name="target">Target object to set value to</param>
		/// <param name="propertyName">Property name to set value to</param>
		/// <param name="value">Value to set</param>
		public static void SetValue(this object target, string propertyName, object value)
		{
			if (string.IsNullOrEmpty(propertyName))
				return;

			var property = target.GetType().GetProperty(propertyName);

			if (property == null)
				return;

			property.SetValue(target, value, BindingFlags.SetProperty, null, null, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Removes all entries from source list that satisfies predicate
		/// </summary>
		/// <typeparam name="TSource">Element type</typeparam>
		/// <param name="source">Source collection to remove from</param>
		/// <param name="predicate">Predicate to delete values</param>
		public static void Remove<TSource>(this LinkedList<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null)
				return;

			for (var node = source.Last; node != null; node = node.Previous)
			{
				if (predicate(node.Value))
				{
					source.Remove(node);
				}
			}
		}

		/// <summary>
		/// Checks the collection is null or has no elements
		/// </summary>
		/// <param name="source">Source collection to check</param>
		/// <returns><c>true</c> if collection is null or has no elements; otherwise - <c>false</c></returns>
		[DebuggerStepThrough]
		public static bool IsNullOrEmpty(this ICollection source)
		{
			return source == null || source.Count == 0;
		}

		/// <summary>
		/// Checks the collection is null or has no elements
		/// </summary>
		/// <typeparam name="TSource">Collection element type</typeparam>
		/// <param name="source">Source collection to check</param>
		/// <returns><c>true</c> if collection is null or has no elements; otherwise - <c>false</c></returns>
		[DebuggerStepThrough]
		public static bool IsNullOrEmpty<TSource>(this ICollection<TSource> source)
		{
			return source == null || source.Count == 0;
		}

		/// <summary>
		/// Checks the collection is null or has no elements
		/// </summary>
		/// <typeparam name="TSource">Collection element type</typeparam>
		/// <param name="source">Source collection to check</param>
		/// <returns><c>true</c> if collection is null or has no elements; otherwise - <c>false</c></returns>
		[DebuggerStepThrough]
		public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> source)
		{
			return source == null || source.Count == 0;
		}

		/// <summary>
		/// Checks the list is null or has no elements
		/// </summary>
		/// <typeparam name="TSource">List element type</typeparam>
		/// <param name="source">Source list to check</param>
		/// <returns><c>true</c> if list is null or has no elements; otherwise - <c>false</c></returns>
		[DebuggerStepThrough]
		public static bool IsNullOrEmpty<TSource>(this IList<TSource> source)
		{
			return source == null || source.Count == 0;
		}

		[DebuggerStepThrough]
		public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return source == null || source.Count() == 0;
		}

		/// <summary>
		/// Checks the array is null or has no elements
		/// </summary>
		/// <typeparam name="TSource">Array element type</typeparam>
		/// <param name="source">Source array to check</param>
		/// <returns><c>true</c> if array is null or has no elements; otherwise - <c>false</c></returns>
		[DebuggerStepThrough]
		public static bool IsNullOrEmpty<TSource>(this TSource[] source)
		{
			return source == null || source.Length == 0;
		}

		/// <summary>
		/// Checks the list is null or has no elements
		/// </summary>
		/// <typeparam name="TSource">List element type</typeparam>
		/// <param name="source">Source list to check</param>
		/// <returns><c>true</c> if list is null or has no elements; otherwise - <c>false</c></returns>
		[DebuggerStepThrough]
		public static bool IsNullOrEmpty<TSource>(this List<TSource> source)
		{
			return source == null || source.Count == 0;
		}

		/// <summary>
		/// Checks the collection is null or has no elements
		/// </summary>
		/// <typeparam name="TSource">Collection element type</typeparam>
		/// <param name="source">collection list to check</param>
		/// <returns><c>true</c> if collection is null or has no elements; otherwise - <c>false</c></returns>
		[DebuggerStepThrough]
		public static bool IsNullOrEmpty<TSource>(this ReadOnlyCollection<TSource> source)
		{
			return source == null || source.Count == 0;
		}



		/// <summary>
		/// Withes the specified automatic.
		/// </summary>
		/// <typeparam name="TSource">The type of the input.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="o">The automatic.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static TResult With<TSource, TResult>(this TSource o, Func<TSource, TResult> evaluator)
			where TSource : class
		{
			if (Equals(o, null) || evaluator == null)
				return default(TResult);

			return evaluator(o);
		}

		[DebuggerStepThrough]
		public static TResult With<TSource, TResult>(this TSource source, TResult defaultValue, Func<TSource, TResult> predicate)
			where TSource : class
		{
			if (source == null || predicate == null)
				return defaultValue;

			return predicate(source);
		}

		/// <summary>
		/// Withes the specified automatic.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <param name="o">The automatic.</param>
		/// <param name="evaluator">The evaluator.</param>
		[DebuggerStepThrough]
		public static TInput With<TInput>(this TInput o, Action<TInput> evaluator)
			where TInput : class
		{
			if (o == null)
				return null;

			evaluator(o);

			return o;
		}

		[DebuggerStepThrough]
		public static void With(this IEnumerable source, int count, Action<IEnumerable> action)
		{
			With(source, count, action, action);
		}

		[DebuggerStepThrough]
		public static void With<TSource>(this IEnumerable<TSource> source, int count, Action<IEnumerable<TSource>> action)
		{
			With(source, count, action, action);
		}

		[DebuggerStepThrough]
		public static void With(this IEnumerable source, int count, Action<object> predicateTrueAction, Action<IEnumerable> predicateFalseAction)
		{
			if (source == null || predicateTrueAction == null || predicateFalseAction == null)
				return;

			var counter = 0;

			With(source, x => counter++ <= count, predicateTrueAction, predicateFalseAction);
		}

		[DebuggerStepThrough]
		public static void With<TSource>(this IEnumerable<TSource> source, int count, Action<TSource> predicateTrueAction, Action<IEnumerable<TSource>> predicateFalseAction)
		{
			if (source == null || predicateTrueAction == null || predicateFalseAction == null)
				return;

			var counter = 0;

			With(source, x => counter++ <= count, predicateTrueAction, predicateFalseAction);
		}

		[DebuggerStepThrough]
		public static void With(this IEnumerable source, Func<object, bool> predicate, Action<object> predicateTrueAction, Action<IEnumerable> predicateFalseAction)
		{
			if (source == null || predicate == null || predicateTrueAction == null || predicateFalseAction == null)
				return;

			var enumerator = source.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (predicate(enumerator.Current))
				{
					predicateTrueAction(enumerator.Current);
				}
				else
				{
					break;
				}
			}

			predicateFalseAction(IterateToEnd(enumerator));
		}

		[DebuggerStepThrough]
		public static void With<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, Action<TSource> predicateTrueAction, Action<IEnumerable<TSource>> predicateFalseAction)
		{
			if (source == null || predicate == null || predicateTrueAction == null || predicateFalseAction == null)
				return;

			var enumerator = source.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (predicate(enumerator.Current))
				{
					predicateTrueAction(enumerator.Current);
				}
				else
				{
					break;
				}
			}

			predicateFalseAction(IterateToEnd(enumerator));
		}

		[DebuggerStepThrough]
		public static void With(this IEnumerable source, int count, Action<IEnumerable> predicateTrueAction, Action<IEnumerable> predicateFalseAction)
		{
			if (source == null || predicateTrueAction == null || predicateFalseAction == null)
				return;

			var counter = 0;

			With(source, x => counter++ <= count, predicateTrueAction, predicateFalseAction);
		}

		[DebuggerStepThrough]
		public static void With<TSource>(this IEnumerable<TSource> source, int count, Action<IEnumerable<TSource>> predicateTrueAction, Action<IEnumerable<TSource>> predicateFalseAction)
		{
			if (source == null || predicateTrueAction == null || predicateFalseAction == null)
				return;

			var counter = 0;

			With(source, x => counter++ <= count, predicateTrueAction, predicateFalseAction);
		}

		[DebuggerStepThrough]
		public static void With(this IEnumerable source, Func<object, bool> predicate, Action<IEnumerable> predicateTrueAction, Action<IEnumerable> predicateFalseAction)
		{
			if (source == null || predicate == null || predicateTrueAction == null || predicateFalseAction == null)
				return;

			var enumerator = source.GetEnumerator();

			predicateTrueAction(Iterate(enumerator, predicate));
			predicateFalseAction(IterateToEnd(enumerator));
		}

		[DebuggerStepThrough]
		public static void With<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, Action<IEnumerable<TSource>> predicateTrueAction, Action<IEnumerable<TSource>> predicateFalseAction)
		{
			if (source == null || predicate == null || predicateTrueAction == null || predicateFalseAction == null)
				return;

			var enumerator = source.GetEnumerator();

			predicateTrueAction(Iterate(enumerator, predicate));
			predicateFalseAction(IterateToEnd(enumerator));
		}

		private static IEnumerable Iterate(IEnumerator enumerator, Func<object, bool> takePredicate)
		{
			if (enumerator == null || takePredicate == null)
				yield break;

			if (takePredicate(enumerator.Current))
			{
				yield return enumerator.Current;
			}

			while (enumerator.MoveNext())
			{
				if (takePredicate(enumerator.Current))
				{
					yield return enumerator.Current;
				}
				else
				{
					break;
				}
			}
		}

		private static IEnumerable<TSource> Iterate<TSource>(IEnumerator<TSource> enumerator, Func<TSource, bool> takePredicate)
		{
			if (enumerator == null || takePredicate == null)
				yield break;

			if (takePredicate(enumerator.Current))
			{
				yield return enumerator.Current;
			}

			while (enumerator.MoveNext())
			{
				if (takePredicate(enumerator.Current))
				{
					yield return enumerator.Current;
				}
				else
				{
					break;
				}
			}
		}

		private static IEnumerable IterateToEnd(IEnumerator enumerator)
		{
			if (enumerator == null)
				yield break;

			yield return enumerator.Current;

			while (enumerator.MoveNext())
			{
				yield return enumerator.Current;
			}
		}

		private static IEnumerable<TSource> IterateToEnd<TSource>(IEnumerator<TSource> enumerator)
		{
			if (enumerator == null)
				yield break;

			yield return enumerator.Current;

			while (enumerator.MoveNext())
			{
				yield return enumerator.Current;
			}

			enumerator.Dispose();
		}

		public static IEnumerable WithCancellationSupport(this IEnumerable source, Func<bool> cancellationFunc)
		{
			if (source == null)
				yield break;

			var enumerator = source.GetEnumerator();

			if (cancellationFunc())
				yield break;

			while (enumerator.MoveNext())
			{
				if (cancellationFunc())
					yield break;

				yield return enumerator.Current;

				if (cancellationFunc())
					yield break;
			}
		}

		public static IEnumerable<TSource> WithCancellationSupport<TSource>(this IEnumerable<TSource> source, Func<bool> cancellationFunc)
		{
			if (source == null)
				yield break;

			var enumerator = source.GetEnumerator();

			if (cancellationFunc())
				yield break;

			while (enumerator.MoveNext())
			{
				if (cancellationFunc())
					yield break;

				yield return enumerator.Current;

				if (cancellationFunc())
					yield break;
			}

			enumerator.Dispose();
		}

		/// <summary>
		///   Enumerates collection skipping predicate accept values
		/// </summary>
		/// <param name="source">Source collection</param>
		/// <param name="predicate">Value predicate</param>
		/// <returns>Collection without items with specified value</returns>
		public static IEnumerable Skip(this IEnumerable source, Func<object, bool> predicate)
		{
			if (source == null || predicate == null)
				yield break;

			var enumerator = source.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (predicate(enumerator.Current))
					continue;

				yield return enumerator.Current;
			}
		}

		/// <summary>
		///   Enumerates collection skipping predicate accept values
		/// </summary>
		/// <typeparam name="TSource">Type of item</typeparam>
		/// <param name="source">Source collection</param>
		/// <param name="predicate">Value predicate</param>
		/// <returns>Collection without items with specified value</returns>
		public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
				return null;
			if (predicate == null)
				return source;

			return source.Where(x => !predicate(x));
		}

		public static IEnumerable<TSource> TakeUntil<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
				return Enumerable.Empty<TSource>();
			if (predicate == null)
				return source;

			return source.TakeWhile(x => !predicate(x));
		}

		/// <summary>
		/// Ases the type.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <param name="obj">The obj.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <returns></returns>
		public static object AsType<TInput>(this object obj, Action<TInput> evaluator)
			where TInput : class
		{
			var castedObj = obj as TInput;

			if (castedObj == null)
				return obj;

			evaluator.With(e => e(castedObj));

			return obj;
		}

		/// <summary>
		/// Withes the type.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <param name="obj">The obj.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static TInput WithType<TInput>(this object obj, Action<TInput> evaluator)
			where TInput : class
		{
			var castedObj = obj as TInput;

			if (castedObj == null)
				return null;

			evaluator.With(e => e(castedObj));

			return castedObj;
		}

		/// <summary>
		/// Withes the type.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="obj">The object.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static TResult WithType<TInput, TResult>(this object obj, Func<TInput, TResult> evaluator)
			where TInput : class
			where TResult : class
		{
			var castedObj = obj as TInput;

			if (castedObj == null)
				return null;

			return evaluator.With(e => e(castedObj));
		}

		/// <summary>
		/// Withes the type.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <param name="obj">The obj.</param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static TInput WithType<TInput>(this object obj)
			where TInput : class
		{
			return obj.WithType<TInput>(null);
		}

		/// <summary>
		/// Withes the out empty.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <param name="o">The automatic.</param>
		/// <param name="evaluator">The evaluator.</param>
		[DebuggerStepThrough]
		public static void WithOutEmpty<TInput>(this IEnumerable<TInput> o, Action<IEnumerable<TInput>> evaluator)
			where TInput : class
		{
			if (o == null || !o.Any())
				return;

			evaluator(o);
		}

		/// <summary>
		/// Withes the out empty.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <param name="o">The automatic.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static IEnumerable<TInput> WithOutEmpty<TInput>(this IEnumerable<TInput> o, Func<IEnumerable<TInput>, IEnumerable<TInput>> evaluator)
			where TInput : class
		{
			if (o == null || !o.Any())
				return null;

			return evaluator(o);
		}

		/// <summary>
		/// Differences the specified source.
		/// </summary>
		/// <typeparam name="TSource">
		/// The type of the source.
		/// </typeparam>
		/// <param name="source">
		/// The source.
		/// </param>
		/// <param name="condition">
		/// The condition.
		/// </param>
		/// <param name="evaluator">
		/// The evaluator.
		/// </param>
		/// <returns>
		/// The if. TSource
		/// </returns>
		[DebuggerStepThrough]
		public static TSource If<TSource>(this TSource source, Func<TSource, bool> condition, Action<TSource> evaluator)
												where TSource : class
		{
			return source.With(x =>
			{
				if (condition != null && condition(x) && evaluator != null)
				{
					evaluator(source);
				}
				return source;
			});
		}

		/// <summary>
		/// Differences the specified source.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="condition">The condition.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static TResult If<TSource, TResult>(this TSource source, Func<TSource, bool> condition, Func<TSource, TResult> evaluator)
			where TSource : class
			where TResult : class
		{
			return source.With(x =>
			{
				if (condition != null && condition(x) && evaluator != null)
				{
					return evaluator.Invoke(source);
				}

				return null;
			});
		}

		/// <summary>
		/// Differences the not.
		/// </summary>
		/// <typeparam name="TSource">
		/// The type of the source.
		/// </typeparam>
		/// <param name="source">
		/// The source.
		/// </param>
		/// <param name="condition">
		/// The condition.
		/// </param>
		/// <param name="evaluator">
		/// The evaluator.
		/// </param>
		/// <returns>The if. TSource
		/// </returns>
		public static TSource IfNot<TSource>(this TSource source, Func<TSource, bool> condition, Action<TSource> evaluator)
			where TSource : class
		{
			return source.With(x =>
			{
				if (condition != null && !condition(x) && evaluator != null)
				{
					evaluator.Invoke(source);
					return source;
				}

				return null;
			});
		}

		/// <summary>
		/// Tries the catch.
		/// </summary>
		/// <typeparam name="T">
		/// parameter must be not empty
		/// </typeparam>
		/// <param name="source">
		/// The source.
		/// </param>
		/// <param name="evaluator">
		/// The evaluator.
		/// </param>
		/// <param name="defaultValue">
		/// The default value.
		/// </param>
		/// <param name="exceptionMessage">
		/// The exception message.
		/// </param>
		/// <returns>
		/// result of the evaluator invoke or default value
		/// </returns>
		[DebuggerStepThrough]
		public static R TryCatch<R, T>(this T source, Func<R> evaluator, R defaultValue, string exceptionMessage)
			where T : class
			where R : class
		{
			try
			{
				return source.With(x => evaluator());
			}
			catch (Exception ex)
			{
				CLog.Log(exceptionMessage, ex);
			}

			return defaultValue;
		}

		/// <summary>
		/// Tries the catch.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <param name="exceptionMessage">The exception message.</param>
		[DebuggerStepThrough]
		public static void TryCatch<T>(this T source, Action evaluator, string exceptionMessage)
			where T : class
		{
			try
			{
				source.With(x => evaluator());
			}
			catch (Exception ex)
			{
				CLog.Log(exceptionMessage, ex);
			}
		}

		/// <summary>
		/// Tries the catch.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <param name="catchAction">The catch action.</param>
		/// <param name="exceptionMessage">The exception message.</param>
		[DebuggerStepThrough]
		public static void TryCatch<T>(this T source, Action evaluator, Action catchAction, string exceptionMessage)
			where T : class
		{
			try
			{
				source.With(x => evaluator());
			}
			catch (Exception)
			{
				catchAction.With(xAction => xAction());
			}
		}

		/// <summary>
		/// Tries the catch.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <param name="exceptionMessage">The exception message.</param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static T TryCatch<T>(this T source, Action<T> evaluator, string exceptionMessage)
			where T : class
		{
			try
			{
				source.With(x => evaluator(source));
			}
			catch (Exception ex)
			{
				CLog.Log(exceptionMessage, ex);
			}

			return source;
		}
	}
}