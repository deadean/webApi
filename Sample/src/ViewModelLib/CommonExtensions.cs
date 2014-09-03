using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;

namespace ViewModelLib.Commands
{
	/// <summary>
	/// Common Extensions
	/// </summary>
	public static class CommonExtensions
	{

		/// <summary>
		/// Withes the specified automatic.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="o">The automatic.</param>
		/// <param name="evaluator">The evaluator.</param>
		/// <returns></returns>
		public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
			where TResult : class
			where TInput : class
		{
			if (o == null) return null;
			return evaluator(o);
		}

		/// <summary>
		/// Withes the specified automatic.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <param name="o">The automatic.</param>
		/// <param name="evaluator">The evaluator.</param>
		public static void With<TInput>(this TInput o, Action<TInput> evaluator)
			where TInput : class
		{
			if (o == null) 
				return;

			evaluator(o);
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
		public static TResult WithType<TInput,TResult>(this object obj, Func<TInput, TResult> evaluator)
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
		public static TSource If<TSource>(this TSource source, Func<TSource, bool> condition, Action<TSource> evaluator)
												where TSource : class
		{
			return source.With(x =>
			{
				if (condition != null && condition(x) && evaluator != null)
				{
					evaluator.Invoke(source);
				}
				return source;;
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
		public static TResult If<TSource,TResult>(this TSource source, Func<TSource, bool> condition, Func<TSource,TResult> evaluator)
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
		public static R TryCatch<R,T>(this T source, Func<R> evaluator, R defaultValue, string exceptionMessage)
			where T : class
			where R : class 
		{
			try
			{
				return source.With(x => evaluator());
			}
			catch (Exception ex)
			{
                MessageBox.Show(String.Format("{0} : {1}, Inner : {2}", exceptionMessage, ex.Message, ex.InnerException == null ? String.Empty : ex.InnerException.ToString()));
				//CLog.Log(exceptionMessage, ex);
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
		public static void TryCatch<T>(this T source, Action evaluator, string exceptionMessage)
			where T : class
		{
			try
			{
				source.With(x => evaluator());
			}
			catch (Exception ex)
			{
                MessageBox.Show(String.Format("{0} : {1}, Inner : {2}", exceptionMessage, ex.Message, ex.InnerException == null ? String.Empty : ex.InnerException.ToString()));
				//CLog.Log(exceptionMessage, ex);
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
		public static T TryCatch<T>(this T source, Action<T> evaluator, string exceptionMessage)
			where T : class
		{
			try
			{
				source.With(x => evaluator(source));
			}
			catch (Exception ex)
			{
                MessageBox.Show(String.Format("{0} : {1}, Inner : {2}", exceptionMessage, ex.Message, ex.InnerException == null ? String.Empty : ex.InnerException.ToString()));
				//CLog.Log(exceptionMessage, ex);
			}

			return source;
		}
	}
}
