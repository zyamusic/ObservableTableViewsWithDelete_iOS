using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace ObservableTables.iOS
{
	public static class ExtensionsMvvmLight
	{
		/// <summary>
		/// Creates a new <see cref="ObservableTableViewController{T}"/> for a given <see cref="IList{T}"/>.
		/// </summary>
		/// <typeparam name="T">The type of the items contained in the list.</typeparam>
		/// <param name="list">The list that the adapter will be created for.</param>
		/// <param name="createCellDelegate">A delegate to a method creating or reusing a <see cref="UITableViewCell"/>.
		/// The cell will then be passed to the bindCellDelegate
		/// delegate to set the elements' properties.</param>
		/// <param name="bindCellDelegate">A delegate to a method taking a <see cref="UITableViewCell"/>
		/// and setting its elements' properties according to the item
		/// passed as second parameter.
		/// The cell must be created first in the createCellDelegate
		/// delegate.</param>
		/// <returns>A controller adapted to the collection passed in parameter.</returns>
		public static TaskListObservableTableViewController<T> GetTaskListController<T>(
			this IList<T> list,
			Func<NSString, UITableViewCell> createCellDelegate,
			Action<UITableViewCell, T, NSIndexPath> bindCellDelegate)
		{
			return new TaskListObservableTableViewController<T>
			{
				DataSource = list,
				CreateCellDelegate = createCellDelegate,
				BindCellDelegate = bindCellDelegate
			};

			/*
			return new ObservableTableViewController<T>
			{
				DataSource = list,
				CreateCellDelegate = createCellDelegate,
				BindCellDelegate = bindCellDelegate
			};
			*/
		}

	}
}

