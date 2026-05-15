using System.Text;
using System.Text.Json.Serialization;

internal sealed record GithubEvent
{
    public long Id { get; init; }
    public EventType Type { get; init; }
    public Repo? Repo { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    public override string ToString()
    {
        return $"Id: {Id}\nEvent Type: {Type.ToString()}\nRepo: {Repo?.ToString()}\nCreatedAt: {CreatedAt.ToString()}";
    }
}