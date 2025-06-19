namespace OttPlatform.Core;

/// <summary>
/// Represents content in the OTT platform
/// </summary>
public class Content
{
    /// <summary>
    /// Gets or sets the unique identifier of the content
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the title of the content
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the content
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the streaming quality of the content
    /// </summary>
    public string Quality { get; set; } = "HD";

    /// <summary>
    /// Gets or sets the duration of the content in minutes
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    /// Gets or sets the genre of the content
    /// </summary>
    public string Genre { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the release date of the content
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Gets or sets whether the content is currently streaming
    /// </summary>
    public bool IsStreaming { get; set; }
} 