namespace OttPlatform.Core;

/// <summary>
/// Represents an observer that can receive notifications about content updates
/// </summary>
public interface IObserver
{
    /// <summary>
    /// Updates the observer with new content information
    /// </summary>
    /// <param name="contentId">The ID of the content that was updated</param>
    /// <param name="updateType">The type of update that occurred</param>
    /// <param name="message">Additional information about the update</param>
    void Update(string contentId, ContentUpdateType updateType, string message);
}

/// <summary>
/// Represents the types of content updates that can occur
/// </summary>
public enum ContentUpdateType
{
    NewContent,
    ContentUpdated,
    ContentRemoved,
    StreamingStarted,
    StreamingEnded,
    QualityChanged
} 