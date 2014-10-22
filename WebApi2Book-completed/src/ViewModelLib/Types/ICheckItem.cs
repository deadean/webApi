using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LizenzaDevelopment.ADAICA.Data.GLOB.ctrGLOB.ViewModel.Types
{
	public interface ICheckItem : INotifyPropertyChanged
	{
		bool IsChecked { get; set; }
	}
}
