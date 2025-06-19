using Xunit;
using OttPlatform.Core;

namespace OttPlatform.Tests;

public class ContentManagerTests
{
    private readonly ContentManager _contentManager;
    private readonly ContentNotificationService _notificationService;
    private readonly Content _testContent;

    public ContentManagerTests()
    {
        _contentManager = new ContentManager();
        _notificationService = new ContentNotificationService("user1");
        _contentManager.RegisterObserver(_notificationService);

        _testContent = new Content
        {
            Title = "Test Movie",
            Description = "A test movie",
            Duration = 120,
            Genre = "Action",
            ReleaseDate = DateTime.UtcNow
        };
    }

    [Fact]
    public void AddContent_ShouldNotifyObservers()
    {
        // Act
        _contentManager.AddContent(_testContent);

        // Assert
        var notification = _notificationService.Notifications.First();
        Assert.Equal(_testContent.Id, notification.ContentId);
        Assert.Equal(ContentUpdateType.NewContent, notification.UpdateType);
        Assert.Contains(_testContent.Title, notification.Message);
    }

    [Fact]
    public void UpdateContent_ShouldNotifyObservers()
    {
        // Arrange
        _contentManager.AddContent(_testContent);
        _testContent.Title = "Updated Title";

        // Act
        _contentManager.UpdateContent(_testContent);

        // Assert
        var notification = _notificationService.Notifications.Last();
        Assert.Equal(_testContent.Id, notification.ContentId);
        Assert.Equal(ContentUpdateType.ContentUpdated, notification.UpdateType);
        Assert.Contains("Updated Title", notification.Message);
    }

    [Fact]
    public void RemoveContent_ShouldNotifyObservers()
    {
        // Arrange
        _contentManager.AddContent(_testContent);

        // Act
        _contentManager.RemoveContent(_testContent.Id);

        // Assert
        var notification = _notificationService.Notifications.Last();
        Assert.Equal(_testContent.Id, notification.ContentId);
        Assert.Equal(ContentUpdateType.ContentRemoved, notification.UpdateType);
        Assert.Contains(_testContent.Title, notification.Message);
    }

    [Fact]
    public void UpdateStreamingQuality_ShouldNotifyObservers()
    {
        // Arrange
        _contentManager.AddContent(_testContent);

        // Act
        _contentManager.UpdateStreamingQuality(_testContent.Id, "4K");

        // Assert
        var notification = _notificationService.Notifications.Last();
        Assert.Equal(_testContent.Id, notification.ContentId);
        Assert.Equal(ContentUpdateType.QualityChanged, notification.UpdateType);
        Assert.Contains("4K", notification.Message);
    }

    [Fact]
    public void RemoveObserver_ShouldNotReceiveNotifications()
    {
        // Arrange
        _contentManager.RemoveObserver(_notificationService);

        // Act
        _contentManager.AddContent(_testContent);

        // Assert
        Assert.Empty(_notificationService.Notifications);
    }
} 