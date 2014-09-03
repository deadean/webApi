using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace LizenzaDevelopment.ADAICA.Data.GLOB.ctrGLOB.ViewModel.Types
{
	public interface ICheckItemsCollection<T> : IList<T> where T : ICheckItem
	{
		event NotifyCollectionChangedEventHandler CheckedItemsChanged;

		IList<T> CheckedItems { get; }
	}
}
