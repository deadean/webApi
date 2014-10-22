using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace LizenzaDevelopment.ADAICA.Data.GLOB.ctrGLOB.ViewModel.Types
{
	public class PositionedItemsCollection<T> : ObservableCollection<T> where T : PositionedItem
	{
		public PositionedItemsCollection()
		{
			this.CollectionChanged += PositionedItemsCollection_CollectionChanged;
		}

		void PositionedItemsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Move)
			{
				foreach (T xItem in this)
				{
					xItem.Position = IndexOf(xItem);
				}
			}
		}		
	}
}
