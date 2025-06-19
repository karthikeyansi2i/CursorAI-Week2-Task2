namespace OttPlatform.Core;

/// <summary>
/// Represents a subject that can be observed for content updates
/// </summary>
public interface ISubject
{
    /// <summary>
    /// Registers an observer to receive updates
    /// </summary>
    /// <param name="observer">The observer to register</param>
    void RegisterObserver(IObserver observer);

    /// <summary>
    /// Removes an observer from receiving updates
    /// </summary>
    /// <param name="observer">The observer to remove</param>
    void RemoveObserver(IObserver observer);

    /// <summary>
    /// Notifies all registered observers about a content update
    /// </summary>
    /// <param name="contentId">The ID of the content that was updated</param>
    /// <param name="updateType">The type of update that occurred</param>
    /// <param name="message">Additional information about the update</param>
    void NotifyObservers(string contentId, ContentUpdateType updateType, string message);
} 