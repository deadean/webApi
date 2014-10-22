using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModelLib.Types;

namespace LizenzaDevelopment.ADAICA.Data.GLOB.ctrGLOB.ViewModel.Types
{
	public class CheckItem : ViewModelBase, ICheckItem
	{
		private bool mvIsChecked;
		private string mvHeader = string.Empty;

		public bool IsChecked
		{
			get { return mvIsChecked; }
			set
			{
				if (mvIsChecked != value)
				{
					mvIsChecked = value;
					OnPropertyChanged("IsChecked");
				}
			}
		}

		public virtual string Header
		{
			get { return mvHeader; }
			set
			{
				if (value != mvHeader)
				{
					mvHeader = value ?? string.Empty;
					OnPropertyChanged("Header");
				}
			}
		}
	}
}
