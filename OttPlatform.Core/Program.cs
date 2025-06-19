using OttPlatform.Core;

namespace OttPlatform.Core;

public class Program
{
    public static void Main()
    {
        // Create the content manager (subject)
        var contentManager = new ContentManager();

        // Create notification services for different users (observers)
        var user1Notifications = new ContentNotificationService("user1");
        var user2Notifications = new ContentNotificationService("user2");

        // Register observers
        contentManager.RegisterObserver(user1Notifications);
        contentManager.RegisterObserver(user2Notifications);

        // Create and add new content
        var movie = new Content
        {
            Title = "The Matrix",
            Description = "A computer hacker learns about the true nature of reality",
            Duration = 136,
            Genre = "Sci-Fi",
            ReleaseDate = new DateTime(1999, 3, 31)
        };

        contentManager.AddContent(movie);

        // Update content
        movie.Title = "The Matrix (Remastered)";
        contentManager.UpdateContent(movie);

        // Update streaming quality
        contentManager.UpdateStreamingQuality(movie.Id, "4K");

        // Remove an observer
        contentManager.RemoveObserver(user2Notifications);

        // Add another movie (only user1 will be notified)
        var movie2 = new Content
        {
            Title = "Inception",
            Description = "A thief who steals corporate secrets through dream-sharing technology",
            Duration = 148,
            Genre = "Sci-Fi",
            ReleaseDate = new DateTime(2010, 7, 16)
        };

        contentManager.AddContent(movie2);

        // Display notifications for user1
        Console.WriteLine("Notifications for User 1:");
        foreach (var notification in user1Notifications.Notifications)
        {
            Console.WriteLine($"[{notification.Timestamp}] {notification.Message}");
        }

        // Display notifications for user2
        Console.WriteLine("\nNotifications for User 2:");
        foreach (var notification in user2Notifications.Notifications)
        {
            Console.WriteLine($"[{notification.Timestamp}] {notification.Message}");
        }
    }
} 