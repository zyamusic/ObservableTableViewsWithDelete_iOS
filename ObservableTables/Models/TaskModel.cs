using GalaSoft.MvvmLight;
using System.Timers;
using System;

namespace ObservableTables
{
	public class TaskModel : ObservableObject
	{
		private string _notes;

		public TaskModel ()
		{
		}

		public int ID { get; set; }
		public string Name { get; set; }
		public string Notes 
		{
			get { return _notes; } 
			set { Set<string>(() => Notes, ref _notes, value); }
		}

		// new property
		public bool Done { get; set; }
	}
}

