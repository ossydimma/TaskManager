

namespace TasksManager.SharedDataServices
{
    public class SharedDataService
    {
        public bool MaximizeSideBar {get; set;} = true;
        public Action? Onchange;
        public void NotifyStateChanged () => Onchange?.Invoke();
    }
}
