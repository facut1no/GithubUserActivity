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
            return;
        }

        var username = args[0];

        string Url = $"https://api.github.com/users/{username}/events";

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "github-activity-app");
        var events = await client.GetFromJsonAsync<List<GithubEvent>>(Url);

        if (events is null)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Error: Failed to retrieve data from the GitHub API.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please check the username or try again later.");
            Console.ResetColor();
            return;
        }

        if (events.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"The user {username} ");
            Console.ResetColor();
            return;
        }

        foreach (var githubEvent in events)
        {
            switch (githubEvent.Type)
            {
                case EventType.PushEvent:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(
                        $"[PUSH] Pushed a commit to {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
                    );
                    break;

                case EventType.CreateEvent:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(
                        $"[CREATE] Created a new repository or branch in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
                    );
                    break;

                case EventType.DeleteEvent:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        $"[DELETE] Deleted a repository or branch in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
                    );
                    break;

                case EventType.PullRequestEvent:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(
                        $"[PR] Opened or updated a pull request in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
                    );
                    break;

                case EventType.IssuesEvent:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        $"[ISSUE] Created or updated an issue in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
                    );
                    break;

                case EventType.ForkEvent:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(
                        $"[FORK] Forked the repository {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
                    );
                    break;

                case EventType.WatchEvent:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(
                        $"[STAR] Starred the repository {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
                    );
                    break;

                case EventType.ReleaseEvent:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(
                        $"[RELEASE] Published a release in {githubEvent.Repo?.Name} at {githubEvent.CreatedAt}"
                    );
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(
                        $"[UNKNOWN] Unknown event in {githubEvent.Repo?.Name}"
                    );
                    break;
            }

            Console.ResetColor();
        }
    }
}