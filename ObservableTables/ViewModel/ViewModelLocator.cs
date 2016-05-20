using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ObservableTables.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<TaskListViewModel>();
			SimpleIoc.Default.Register<TaskModel>();
        }

		public TaskListViewModel TaskList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TaskListViewModel>();
            }
        }

		public TaskModel TaskModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TaskModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}