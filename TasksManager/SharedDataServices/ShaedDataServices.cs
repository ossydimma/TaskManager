

namespace TasksManager.SharedDataServices
{
    public class SharedDataService
    {
        private bool _showCreateTsk;
        public bool MaximizeSideBar {get; set;} = true;
        public bool ShowCreateTask {
            get => _showCreateTsk;
             set 
             {
                if (_showCreateTsk != value)
                {
                    _showCreateTsk = value;
                    NotifyStateChanged();
                }
             }}

        public event Action? OnChange;
        private void NotifyStateChanged () => OnChange?.Invoke();
    }
}
