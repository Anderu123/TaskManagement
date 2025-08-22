namespace TaskManagement.SharedData
{
    public class Tasks
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? TaskName { get; set; }
        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
