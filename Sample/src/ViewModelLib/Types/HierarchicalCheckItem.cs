namespace LizenzaDevelopment.ADAICA.Data.GLOB.ctrGLOB.ViewModel.Types
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.ComponentModel;
    using ViewModelLib.Types;

	/// <summary>
	/// Use it for create hierarchical CheckBoxes
	/// </summary>
	public class HierarchicalCheckItem : ViewModelBase
	{
		#region Private Fields

		private bool? mvIsChecked = false;
		private HierarchicalCheckItem modParent;
		private ObservableCollection<HierarchicalCheckItem> mvItems;

		#endregion Private Fields

		#region Properties

		/// <summary>
		/// Get or set IsChecked state
		/// </summary>
		public bool? IsChecked
		{
			get
			{
				return mvIsChecked;
			}
			set
			{
				this.SetIsChecked(value, true, true);
			}
		}

		/// <summary>
		/// Get or set child items
		/// </summary>
		public IList<HierarchicalCheckItem> Items
		{
			get
			{
				return mvItems;
			}
		}

		#endregion Properties

		#region Ctor

		/// <summary>
		/// Creates new instance of <see cref="HierarchicalCheckItem"/> class
		/// </summary>
		public HierarchicalCheckItem()
		{
			mvItems = new ObservableCollection<HierarchicalCheckItem>();
			mvItems.CollectionChanged += Items_CollectionChanged;
		}

		private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			foreach (HierarchicalCheckItem item in e.NewItems)
			{
				if (item.modParent != null && item.modParent.Items.Contains(item))
					item.modParent.Items.Remove(item);

				item.modParent = this;
			}

			foreach (HierarchicalCheckItem item in e.OldItems)
			{
				item.modParent = null;
			}
		}

		#endregion Ctor

		#region Private Methods

		private void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
		{
			if (value != mvIsChecked)
			{
				mvIsChecked = value;

				if (updateChildren && mvIsChecked.HasValue)
				{
					foreach (var child in mvItems)
					{
						child.SetIsChecked(mvIsChecked, true, false);
					}
				}

				if (updateParent && modParent != null)
				{
					modParent.VerifyCheckState();
				}

				OnPropertyChanged("IsChecked");
			}
		}

		private void VerifyCheckState()
		{
			bool? state = null;

			for (int i = 0; i < this.Items.Count; ++i)
			{
				bool? current = this.Items[i].IsChecked;

				if (i == 0)
				{
					state = current;
				}
				else if (state != current)
				{
					state = null;
					break;
				}
			}

			this.SetIsChecked(state, false, true);
		}

		#endregion Private Methods
	}
}
