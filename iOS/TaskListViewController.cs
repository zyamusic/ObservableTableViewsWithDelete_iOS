using Foundation;
using System;
using UIKit;
using GalaSoft.MvvmLight.Helpers;

using ObservableTables.ViewModel;
using ObservableTables;
using CoreFoundation;

namespace ObservableTables.iOS
{
	partial class TaskListViewController : UIViewController
	{
		private TaskListObservableTableViewController<TaskModel> tableViewController;

		private TaskListViewModel Vm => Application.Locator.TaskList;
		private TaskModel Vm2 = Application.Locator.TaskModel;

		public TaskListViewController (IntPtr handle) : base (handle)
		{
		}

		public UIBarButtonItem AddTaskButton
		{
			get;
			private set;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			AddTaskButton = new UIBarButtonItem(UIBarButtonSystemItem.Add);
			this.NavigationItem.SetLeftBarButtonItem (AddTaskButton, false);

			//note this was needed when deploying to a real iPhone but worked
			//on the simulator without it. The Argument Exception Event not found clicked was thrown
			AddTaskButton.Clicked += (sender, e) => {};

			AddTaskButton.SetCommand("Clicked", Vm.AddTaskCommand);

			tableViewController = Vm.TodoTasks.GetTaskListController(CreateTaskCell, BindTaskCell);
//			TasksTableView.RegisterClassForCellReuse(typeof(UITableViewCell), new NSString("C"));
			tableViewController.TableView = TasksTableView;
		}

		private UITableViewCell CreateTaskCell(NSString cellIdentifier)
		{
			var cell = new MyTableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);
			cell.TextLabel.TextColor = UIColor.FromRGB(55, 63, 255);
			cell.DetailTextLabel.LineBreakMode = UILineBreakMode.TailTruncation;

			return cell;
		}


		private void BindTaskCell(UITableViewCell cell, TaskModel taskModel, NSIndexPath path)
		{
			//cell.DetailTextLabel.Text = taskModel.Notes;

			MyTableViewCell myCell = (MyTableViewCell)cell;

			if (myCell.textFieldBinding != null)
			{
				Console.WriteLine("detach");
				myCell.textFieldBinding.ValueChanged -= DetailTextChanged;
				myCell.textFieldBinding.Detach();
				myCell.textFieldBinding = null;
			}

			myCell.TextLabel.Text = taskModel.Name;

			myCell.taskModel = taskModel;

			try
			{
				myCell.textFieldBinding = new Binding<string, string>(myCell.taskModel, "Notes", myCell.DetailTextLabel, "Text",BindingMode.OneWay);
				myCell.textFieldBinding.ValueChanged += DetailTextChanged;
/*
(object sender, EventArgs e) => 
					
					{
						Console.WriteLine(string.Format("update {0}", myCell.taskModel.Name));
						DispatchQueue.MainQueue.DispatchAsync(() =>
						{
							myCell.DetailTextLabel.Text = myCell.taskModel.Notes;
						});
					};
*/
//textFieldBinding = cell.SetBinding(() => taskModel.Name, () => cell.DetailTextLabel.Text);
			//textFieldBinding = cell.SetBinding(() => taskModel.Notes).WhenSourceChanges(() => cell.DetailTextLabel.Text = taskModel.Notes);
			}
			catch(Exception ex)
			{
			}
		}

		public void DetailTextChanged(object sender, EventArgs e)
		{
			var binding = (Binding<string, string>)sender;

			if (binding == null)
			{
				return;
			};

			UILabel label = (UILabel)binding.Target;
			if (label == null)
			{
				// when this happens we seem to lose the binding
				return;
			}

			//Console.WriteLine(string.Format("update {0}", myCell.taskModel.Name));
			DispatchQueue.MainQueue.DispatchAsync(() =>
			{
				label.Text = binding.Value;
			});
		}
	}
}
