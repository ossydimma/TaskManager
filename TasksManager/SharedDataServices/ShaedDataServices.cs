

namespace TasksManager.SharedDataServices
{
    public class SharedDataService
    {
        private bool _showCreateTask;
        public bool MaximizeSideBar { get; set; } = true;
        public bool ShowCreateTask
        {
            get => _showCreateTask;
            set
            {
                if (_showCreateTask != value)
                {
                    _showCreateTask = value;
                    NotifyStateChanged();
                }
            }
        }

        public event Action? OnChange;
        public void NotifyStateChanged() => OnChange?.Invoke();
    }

}
