

namespace TasksManager.SharedDataServices
{
    public class SharedDataService
    {
        public bool MaximizeSideBar { get; set; } = true;

        private string? _previousUrl;

        public string PreviousvUrl
        {
            get => _previousUrl!;
            set
            {
                _previousUrl = value;
                NotifyStateChanged();
            }
        }
        public bool IsLogOut { get; set; } = false;

        public event Action? OnChange;
        public void NotifyStateChanged() => OnChange?.Invoke();
    }

}
