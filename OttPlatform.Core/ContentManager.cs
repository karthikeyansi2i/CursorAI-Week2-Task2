using System.Collections.Concurrent;

namespace OttPlatform.Core;

/// <summary>
/// Manages content and notifies observers about content updates
/// </summary>
public class ContentManager : ISubject
{
    private readonly ConcurrentDictionary<string, IObserver> _observers = new();
    private readonly ConcurrentDictionary<string, Content> _content = new();

    /// <summary>
    /// Registers an observer to receive content updates
    /// </summary>
    public void RegisterObserver(IObserver observer)
    {
        if (observer == null)
            throw new ArgumentNullException(nameof(observer));

        _observers.TryAdd(observer.GetHashCode().ToString(), observer);
    }

    /// <summary>
    /// Removes an observer from receiving content updates
    /// </summary>
    public void RemoveObserver(IObserver observer)
    {
        if (observer == null)
            throw new ArgumentNullException(nameof(observer));

        _observers.TryRemove(observer.GetHashCode().ToString(), out _);
    }

    /// <summary>
    /// Notifies all registered observers about a content update
    /// </summary>
    public void NotifyObservers(string contentId, ContentUpdateType updateType, string message)
    {
        foreach (var observer in _observers.Values)
        {
            observer.Update(contentId, updateType, message);
        }
    }

    /// <summary>
    /// Adds new content to the platform
    /// </summary>
    public void AddContent(Content content)
    {
        if (content == null)
            throw new ArgumentNullException(nameof(content));

        _content.TryAdd(content.Id, content);
        NotifyObservers(content.Id, ContentUpdateType.NewContent, $"New content added: {content.Title}");
    }

    /// <summary>
    /// Updates existing content
    /// </summary>
    public void UpdateContent(Content content)
    {
        if (content == null)
            throw new ArgumentNullException(nameof(content));

        if (_content.TryUpdate(content.Id, content, _content[content.Id]))
        {
            NotifyObservers(content.Id, ContentUpdateType.ContentUpdated, $"Content updated: {content.Title}");
        }
    }

    /// <summary>
    /// Removes content from the platform
    /// </summary>
    public void RemoveContent(string contentId)
    {
        if (string.IsNullOrEmpty(contentId))
            throw new ArgumentException("Content ID cannot be null or empty", nameof(contentId));

        if (_content.TryRemove(contentId, out var content))
        {
            NotifyObservers(contentId, ContentUpdateType.ContentRemoved, $"Content removed: {content.Title}");
        }
    }

    /// <summary>
    /// Updates the streaming quality for content
    /// </summary>
    public void UpdateStreamingQuality(string contentId, string quality)
    {
        if (string.IsNullOrEmpty(contentId))
            throw new ArgumentException("Content ID cannot be null or empty", nameof(contentId));

        if (_content.TryGetValue(contentId, out var content))
        {
            content.Quality = quality;
            NotifyObservers(contentId, ContentUpdateType.QualityChanged, $"Streaming quality changed to: {quality}");
        }
    }
} 