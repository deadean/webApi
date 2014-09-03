using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace LizenzaDevelopment.ADAICA.Data.GLOB.ctrGLOB.ViewModel.Types
{
	public class CheckItemsCollection<T> : ObservableCollection<T>, ICheckItemsCollection<T> where T : class, ICheckItem
	{
		public CheckItemsCollection()
		{
			mvCheckedItems = new ObservableCollection<T>();

			this.CollectionChanged += collection_CollectionChanged;
		}

		private ObservableCollection<T> mvCheckedItems;

		public IList<T> CheckedItems
		{
			get { return mvCheckedItems; }
		}

		private void collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Reset)
				mvCheckedItems.Clear();

			if (e.OldItems != null)
			{
				foreach (T p in e.OldItems)
				{
					p.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(p_PropertyChanged);
				}
			}

			if (e.NewItems != null)
			{
				foreach (T p in e.NewItems)
				{
					p.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(p_PropertyChanged);
				}
			}
		}

		private void p_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{			
			T item = sender as T;

			if (e.PropertyName == "IsChecked")
			{
				if (mvCheckedItems.Contains(item))
				{
					if (!item.IsChecked)
					{
						mvCheckedItems.Remove(item);
					}
				}
				else
				{
					if (item.IsChecked)
					{
						mvCheckedItems.Add(item);
					}
				}
			}
		}

		#region ICheckItemsCollection<T> Members

		public event NotifyCollectionChangedEventHandler CheckedItemsChanged
		{
			add
			{
				mvCheckedItems.CollectionChanged += value;
			}
			remove
			{
				mvCheckedItems.CollectionChanged -= value;
			}
		}


		#endregion
	}
}
