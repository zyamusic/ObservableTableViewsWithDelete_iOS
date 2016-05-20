using System;
using UIKit;
using GalaSoft.MvvmLight.Helpers;

namespace ObservableTables.iOS
{
	public class MyTableViewCell : UITableViewCell
	{
		public Binding<string, string> textFieldBinding;
		public TaskModel taskModel;

		public MyTableViewCell (UITableViewCellStyle style, string reuseIdentifier) : base (style, reuseIdentifier)
		{
			textFieldBinding = null;
		}
	}
}

