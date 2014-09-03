namespace LizenzaDevelopment.ADAICA.Data.GLOB.ctrGLOB.ViewModel.Types
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
    using ViewModelLib.Types;

	/// <summary>
	/// 
	/// </summary>
	public class Progress : ViewModelBase
	{
		private bool mvIsIndeterminate;
		private int mvValue;
		private int mvMinimum;
		private int mvMaximum;

		/// <summary>
		/// 
		/// </summary>
		public bool IsIndeterminate
		{
			get
			{
				return mvIsIndeterminate;
			}
			set
			{
				if (value != mvIsIndeterminate)
				{
					mvIsIndeterminate = value;
					OnPropertyChanged("IsIndeterminate");
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int Value
		{
			get
			{
				return mvValue;
			}
			set
			{
				if (value != mvValue)
				{
					mvValue = value;
					OnPropertyChanged("Value");
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int Minimum
		{
			get
			{
				return mvMinimum;
			}
			set
			{
				if (value != mvMinimum)
				{
					mvMinimum = value;
					OnPropertyChanged("Minimum");
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int Maximum
		{
			get
			{
				return mvMaximum;
			}
			set
			{
				if (value != mvMaximum)
				{
					mvMaximum = value;
					OnPropertyChanged("Maximum");
				}
			}
		}
	}
}
