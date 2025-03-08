namespace Notifications
{
    
    // Определение уровней приоритета
public enum PriorityLevel { Low, Medium, High }

    // Определение типов уведомлений
    public enum NotificationType { Info, Warning, Error }

    // Класс пользователя
    public class User
    {
        public string Name { get; }
        public PriorityLevel Priority { get; }

        public User(string name, PriorityLevel priority)
        {
            Name = name;
            Priority = priority;
        }
    }

    // Система уведомлений
    public class NotificationSystem
    {
        private Dictionary<NotificationType, Action<string, PriorityLevel>> notifications = new();
        private List<User> users = new();

        public void RegisterUser(User user)
        {
            users.Add(user);
        }

        public void Subscribe(NotificationType type, Action<string, PriorityLevel> handler)
        {
            if (!notifications.ContainsKey(type))
            {
                notifications[type] = null;
            }
            notifications[type] += handler;
        }

        public void Notify(NotificationType type, string message, PriorityLevel priority)
        {
            if (notifications.ContainsKey(type))
            {
                foreach (var user in users)
                {
                    if (user.Priority >= priority)
                    {
                        notifications[type]?.Invoke(message, priority);
                    }
                }
            }
        }
    }
}
