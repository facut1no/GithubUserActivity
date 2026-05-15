using System.Net.Http.Json;

public static class Program
{
    public async static Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Error: You must provide a GitHub username.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Example: github-activity <username>");
            Console.ResetColor();
            Environment.Exit(1);
        }

        var username = args[0];

        Console.WriteLine($"Fetching GitHub activity for '{username}'...");
        var events = await GetGithubEventsAsync(username);

        if (events is null)
            Environment.Exit(1);

        foreach (var githubEvent in events)
        {
            switch (githubEvent.Type)
            {
                case EventType.PushEvent:
                    DisplayPushEvent(githubEvent);
                    break;

                case EventType.CreateEvent:
                    DisplayCreateEvent(githubEvent);
                    break;

                case EventType.DeleteEvent:
                    DisplayDeleteEvent(githubEvent);
                    break;

                case EventType.PullRequestEvent:
                    DisplayPullRequestEvent(githubEvent);
                    break;

                case EventType.IssuesEvent:
                    DisplayIssuesEvent(githubEvent);
                    break;

                case EventType.ForkEvent:
                    DisplayForkEvent(githubEvent);
                    break;

                case EventType.WatchEvent:
                    DisplayWatchEvent(githubEvent);
                    break;

                case EventType.ReleaseEvent:
                    DisplayReleaseEvent(githubEvent);
                    break;

                default:
                    DisplayUnknownEvent(githubEvent);
                    break;
            }

            Console.ResetColor();
        }
    }

    static async Task<IEnumerable<GithubEvent>?> GetGithubEventsAsync(string username)
    {
        string url = $"https://api.github.com/users/{Uri.EscapeDataString(username)}/events";

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "GitHubUserActivity-App");

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Error: Failed to retrieve data from the GitHub API.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please check the username or try again later.");
            return null;
        }

        var events = await response.Content.ReadFromJsonAsync<List<GithubEvent>>();

        return events;
    }

    static void DisplayPushEvent(GithubEvent githubEvent)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(
            $"[PUSH] Pushed a commit to {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
        );
    }

    static void DisplayCreateEvent(GithubEvent githubEvent)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(
            $"[CREATE] Created a new repository or branch in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
        );
    }

    static void DisplayDeleteEvent(GithubEvent githubEvent)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(
            $"[DELETE] Deleted a repository or branch in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
        );
    }

    static void DisplayPullRequestEvent(GithubEvent githubEvent)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(
            $"[PR] Opened or updated a pull request in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
        );
    }

    static void DisplayIssuesEvent(GithubEvent githubEvent)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(
            $"[ISSUE] Created or updated an issue in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
        );
    }

    static void DisplayForkEvent(GithubEvent githubEvent)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(
            $"[FORK] Forked the repository {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
        );
    }

    static void DisplayWatchEvent(GithubEvent githubEvent)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(
            $"[STAR] Starred the repository {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
        );
    }

    static void DisplayReleaseEvent(GithubEvent githubEvent)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(
            $"[RELEASE] Published a release in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
        );
    }

    static void DisplayUnknownEvent(GithubEvent githubEvent)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(
            $"[UNKNOWN] Unknown event in {githubEvent.Repo?.Name}"
        );
    }
}