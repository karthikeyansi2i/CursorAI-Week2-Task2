namespace OttPlatform.Core;

/// <summary>
/// Service that handles content notifications for users
/// </summary>
public class ContentNotificationService : IObserver
{
    private readonly string _userId;
    private readonly List<ContentNotification> _notifications = new();

    /// <summary>
    /// Initializes a new instance of the ContentNotificationService
    /// </summary>
    /// <param name="userId">The ID of the user to receive notifications</param>
    public ContentNotificationService(string userId)
    {
        _userId = userId ?? throw new ArgumentNullException(nameof(userId));
    }

    /// <summary>
    /// Gets the list of notifications for the user
    /// </summary>
    public IReadOnlyList<ContentNotification> Notifications => _notifications.AsReadOnly();

    /// <summary>
    /// Updates the observer with new content information
    /// </summary>
    public void Update(string contentId, ContentUpdateType updateType, string message)
    {
        var notification = new ContentNotification
        {
            ContentId = contentId,
            UpdateType = updateType,
            Message = message,
            Timestamp = DateTime.UtcNow
        };

        _notifications.Add(notification);
    }

    /// <summary>
    /// Clears all notifications for the user
    /// </summary>
    public void ClearNotifications()
    {
        _notifications.Clear();
    }
}

/// <summary>
/// Represents a content notification
/// </summary>
public class ContentNotification
{
    /// <summary>
    /// Gets or sets the ID of the content
    /// </summary>
    public string ContentId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of update
    /// </summary>
    public ContentUpdateType UpdateType { get; set; }

    /// <summary>
    /// Gets or sets the notification message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets when the notification was created
    /// </summary>
    public DateTime Timestamp { get; set; }
} 