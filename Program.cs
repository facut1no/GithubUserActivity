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
                case "PushEvent":
                    DisplayPushEvent(githubEvent);
                    break;

                case "CreateEvent":
                    DisplayCreateEvent(githubEvent);
                    break;

                case "DeleteEvent":
                    DisplayDeleteEvent(githubEvent);
                    break;

                case "PullRequestEvent":
                    DisplayPullRequestEvent(githubEvent);
                    break;

                case "IssuesEvent":
                    DisplayIssuesEvent(githubEvent);
                    break;

                case "ForkEvent":
                    DisplayForkEvent(githubEvent);
                    break;

                case "WatchEvent":
                    DisplayWatchEvent(githubEvent);
                    break;

                case "ReleaseEvent":
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
        DisplayMessageColor(
            $"[PUSH] Pushed a commit to {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}",
            ConsoleColor.Green
        );
    }

    static void DisplayCreateEvent(GithubEvent githubEvent)
    {
        DisplayMessageColor(
            $"[CREATE] Created a new repository or branch in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}",
            ConsoleColor.DarkCyan
        );
    }

    static void DisplayDeleteEvent(GithubEvent githubEvent)
    {
        DisplayMessageColor(
            $"[DELETE] Deleted a repository or branch in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}",
            ConsoleColor.Red
        );
    }

    static void DisplayPullRequestEvent(GithubEvent githubEvent)
    {
        DisplayMessageColor(
            $"[PR] Opened or updated a pull request in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}",
            ConsoleColor.Magenta
        );
    }

    static void DisplayIssuesEvent(GithubEvent githubEvent)
    {
        DisplayMessageColor(
            $"[ISSUE] Created or updated an issue in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}",
            ConsoleColor.Yellow
        );
    }

    static void DisplayForkEvent(GithubEvent githubEvent)
    {
        DisplayMessageColor(
            $"[FORK] Forked the repository {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}",
            ConsoleColor.Blue
        );
    }

    static void DisplayWatchEvent(GithubEvent githubEvent)
    {
        DisplayMessageColor(
            $"[STAR] Starred the repository {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}",
            ConsoleColor.DarkYellow
        );
    }

    static void DisplayReleaseEvent(GithubEvent githubEvent)
    {
        DisplayMessageColor(
            $"[RELEASE] Published a release in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
            , ConsoleColor.White
        );
    }

    static void DisplayUnknownEvent(GithubEvent githubEvent)
    {
        DisplayMessageColor(
            $"[{githubEvent.Type}] Activity detected in {githubEvent.Repo?.Name}",
            ConsoleColor.DarkBlue
        );
    }

    static void DisplayMessageColor(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}