using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Maui.Dispatching;

namespace TodoList.ViewModels
{
    public class TaskItem : INotifyPropertyChanged
    {
        public string? Title { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private string _newTaskText;
        public string NewTaskText
        {
            get => _newTaskText;
            set
            {
                _newTaskText = value;
                OnPropertyChanged(nameof(NewTaskText));
            }
        }

        public ObservableCollection<TaskItem> NewTasks { get; set; } = new();
        public ObservableCollection<TaskItem> ArchivedTasks { get; set; } = new();
        public ObservableCollection<TaskItem> CompletedTasks { get; set; } = new();

        public ICommand AddTaskCommand { get; }
        public ICommand ArchiveTaskCommand { get; }
        public ICommand CompleteTaskCommand { get; }

        public MainViewModel()
        {
            AddTaskCommand = new Command(AddTask);
            ArchiveTaskCommand = new Command<TaskItem>(ArchiveTask);
            CompleteTaskCommand = new Command<TaskItem>(CompleteTask);
        }

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskText))
            {
                NewTasks.Add(new TaskItem { Title = NewTaskText });
                NewTaskText = string.Empty;
            }
        }

        public void ArchiveTask(TaskItem task)
        {
            if (task != null)
            {
                NewTasks.Remove(task);
                ArchivedTasks.Add(task);
                OnPropertyChanged(nameof(ArchivedTasks));
            }
        }

        public void CompleteTask(TaskItem task)
        {
            if (task != null)
            {
                NewTasks.Remove(task);
                CompletedTasks.Add(task);
                OnPropertyChanged(nameof(CompletedTasks));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
