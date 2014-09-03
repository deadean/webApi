//-------------------------------------------------------------------
// <summary>http://marlongrech.wordpress.com/2008/12/04/attachedcommandbehavior-aka-acb/</summary>
//-------------------------------------------------------------------

namespace ViewModelLib.Commands
{
	using System;
	using System.Collections.Specialized;
	using System.Windows;

	public class CommandBehaviorCollection
	{
		#region Behaviors

		/// <summary>
		///   Behaviors Read-Only Dependency Property
		///   As you can see the Attached readonly property has a name registered different (BehaviorsInternal) than the property name, this is a tricks os that we can construct the collection as we want
		///   Read more about this here http://wekempf.spaces.live.com/blog/cns!D18C3EC06EA971CF!468.entry
		/// </summary>
		private static readonly DependencyPropertyKey BehaviorsPropertyKey = DependencyProperty.RegisterAttachedReadOnly
            ("BehaviorsInternal", typeof(BehaviorBindingCollection), typeof(CommandBehaviorCollection), new FrameworkPropertyMetadata((BehaviorBindingCollection)null));

		public static readonly DependencyProperty BehaviorsProperty = BehaviorsPropertyKey.DependencyProperty;

		/// <summary>
		///   Gets the Behaviors property
		///   Here we initialze the collection and set the Owner property
		/// </summary>
		public static BehaviorBindingCollection GetBehaviors(DependencyObject dependencyObject)
		{
			if (dependencyObject == null)
				throw new InvalidOperationException("The dependency object trying to attach to is set to null");

			var collection = dependencyObject.GetValue(BehaviorsProperty) as BehaviorBindingCollection;

			if (collection == null)
			{
				collection = new BehaviorBindingCollection { Owner = dependencyObject };

				SetBehaviors(dependencyObject, collection);
			}

			return collection;
		}

		/// <summary>
		///   Provides a secure method for setting the Behaviors property.
		///   This dependency property indicates ....
		/// </summary>
		private static void SetBehaviors(DependencyObject dependencyObject, BehaviorBindingCollection value)
		{
			dependencyObject.SetValue(BehaviorsPropertyKey, value);

			INotifyCollectionChanged collection = value;

			collection.CollectionChanged += CollectionChanged;
		}

		private static void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			var sourceCollection = (BehaviorBindingCollection)sender;

			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (BehaviorBinding item in e.NewItems)
					{
						item.Owner = sourceCollection.Owner;
					}

					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (BehaviorBinding item in e.OldItems)
					{
						item.Behavior.Dispose();
					}

					break;
				case NotifyCollectionChangedAction.Replace:
					foreach (BehaviorBinding item in e.NewItems)
					{
						item.Owner = sourceCollection.Owner;
					}

					foreach (BehaviorBinding item in e.OldItems)
					{
						item.Behavior.Dispose();
					}

					break;
				case NotifyCollectionChangedAction.Reset:
					foreach (BehaviorBinding item in e.OldItems)
					{
						item.Behavior.Dispose();
					}

					break;
			}
		}

		#endregion
	}

	/// <summary>
	///   Collection to store the list of behaviors. This is done so that you can intiniate it from XAML
	///   This inherits from freezable so that it gets inheritance context for DataBinding to work
	/// </summary>
	public class BehaviorBindingCollection : FreezableCollection<BehaviorBinding>
	{
		/// <summary>
		///   Gets or sets the Owner of the binding
		/// </summary>
		public DependencyObject Owner { get; set; }
	}
}