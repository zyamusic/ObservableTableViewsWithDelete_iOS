using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Timers;
using System;

namespace ObservableTables.ViewModel
{
	public class TaskListViewModel : ViewModelBase
	{
		public ObservableCollection<TaskModel> TodoTasks { get; private set; }
		private Timer timer;

		public TaskListViewModel()
		{
			TodoTasks = new ObservableCollection<TaskModel>()
			{
			};

			for (int i = 0; i < 50; i++)
			{
			TodoTasks.Add(new TaskModel
				{
						Name = i.ToString(),
					Notes = ""
				});
			}

			AddTaskCommand = new RelayCommand(AddTask);

			timer = new Timer();
			timer.AutoReset = false;
			timer.Interval = 1000;
			timer.Elapsed += (object sender, ElapsedEventArgs e) => {
				var date = DateTime.Now.ToLongTimeString();
				foreach(var task in TodoTasks)
				{
					task.Notes = task.Name + " " + date;
				}

				timer.Start();
			};
			timer.Start();
		}

		public RelayCommand AddTaskCommand { get; set; }

		private void AddTask()
		{
			TodoTasks.Add(new TaskModel
				{
					Name = "New Task",
					Notes = ""
				});
		}
	}
}