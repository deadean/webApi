//-------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Lizenza Development Ltd.">
//     Copyright (c) Lizenza Development Ltd. All rights reserved.
// </copyright>
// <summary>Holds extensions for DelegateCommand</summary>
// <author>Igor Kobylinskyi/author>
//-------------------------------------------------------------------
namespace ViewModelLib.Commands
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq.Expressions;

	using LizenzaDevelopment.ADAICA.Data.GLOB.ctrGLOB.ViewModel.Types;
	using System.Collections.ObjectModel;
    using ViewModelLib.Types;

	/// <summary>
	/// http://stackoverflow.com/questions/1751966/commandmanager-invalidaterequerysuggested-isnt-fast-enough-what-can-i-do
	/// </summary>
	public static class DelegateCommandExtensions
	{
		/// <summary>
		/// Makes DelegateCommnand listen on PropertyChanged events of some object,
		/// so that DelegateCommnand can update its IsEnabled property.
		/// </summary>
		public static DelegateCommand ListenOn<ObservedType, PropertyType>(this DelegateCommand delegateCommand, ObservedType observedObject, Expression<Func<ObservedType, PropertyType>> propertyExpression) where ObservedType : INotifyPropertyChanged
		{
			string propertyName = NotifyPropertyChangedBaseExtensions.GetPropertyName(propertyExpression);

			observedObject.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == propertyName)
				{
					delegateCommand.RaiseCanExecuteChanged();
				}
			};

			return delegateCommand; //chain calling
		}

		/// <summary>
		/// Makes DelegateCommnand listen on PropertyChanged events of some collection,
		/// so that DelegateCommnand can update its IsEnabled property.
		/// </summary>
		public static DelegateCommand ListenOn<ObservedType, PropertyType>(this DelegateCommand delegateCommand, IEnumerable<ObservedType> observedCollection, Expression<Func<ObservedType, PropertyType>> propertyExpression) where ObservedType : INotifyPropertyChanged
		{
			string propertyName = NotifyPropertyChangedBaseExtensions.GetPropertyName(propertyExpression);

			foreach (var item in observedCollection)
			{
				item.PropertyChanged += (sender, e) =>
				{
					if (e.PropertyName == propertyName)
					{
						delegateCommand.RaiseCanExecuteChanged();
					}
				};
			}

			return delegateCommand; //chain calling
		}

		/// <summary>
		/// Makes DelegateCommnand listen on PropertyChanged events of some object,
		/// so that DelegateCommnand can update its IsEnabled property.
		/// </summary>
		public static DelegateCommand<T> ListenOn<T, ObservedType, PropertyType>(this DelegateCommand<T> delegateCommand, ObservedType observedObject, Expression<Func<ObservedType, PropertyType>> propertyExpression) where ObservedType : INotifyPropertyChanged
		{
			string propertyName = NotifyPropertyChangedBaseExtensions.GetPropertyName(propertyExpression);

			observedObject.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == propertyName)
				{
					delegateCommand.RaiseCanExecuteChanged();
				}
			};

			return delegateCommand; //chain calling
		}

		/// <summary>
		/// Makes DelegateCommnand listen on PropertyChanged events of some collection,
		/// so that DelegateCommnand can update its IsEnabled property.
		/// </summary>
		public static DelegateCommand<T> ListenOn<T, ObservedType, PropertyType>(this DelegateCommand<T> delegateCommand, IEnumerable<ObservedType> observedCollection, Expression<Func<ObservedType, PropertyType>> propertyExpression) where ObservedType : INotifyPropertyChanged
		{
			string propertyName = NotifyPropertyChangedBaseExtensions.GetPropertyName(propertyExpression);

			foreach (var item in observedCollection)
			{
				item.PropertyChanged += (sender, e) =>
				{
					if (e.PropertyName == propertyName)
					{
						delegateCommand.RaiseCanExecuteChanged();
					}
				};
			}

			return delegateCommand; //chain calling
		}
	}
}