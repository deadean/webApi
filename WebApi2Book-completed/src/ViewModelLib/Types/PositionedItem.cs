using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModelLib.Types;

namespace LizenzaDevelopment.ADAICA.Data.GLOB.ctrGLOB.ViewModel.Types
{
	public class PositionedItem : ViewModelBase
	{
		private int mvPosition;

		public int Position
		{
			get { return mvPosition; }
			set
			{
				if (mvPosition != value)
				{
					mvPosition = value;
					OnPropertyChanged("Position");
				}
			}
		}
	}
}
