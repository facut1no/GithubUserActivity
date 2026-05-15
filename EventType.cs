using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
internal enum EventType
{
    PushEvent,
    CreateEvent,
    DeleteEvent,
    PullRequestEvent,
    IssuesEvent,
    ForkEvent,
    WatchEvent,
    ReleaseEvent
}