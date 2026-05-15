https://roadmap.sh/projects/github-user-activity
# GitHub User Activity

A simple .NET console application that fetches and displays a user's public GitHub activity using the GitHub API.

## Features

- Fetch public GitHub events
- Display repository activity
- Colorized console output
- Supports multiple GitHub event types

## Requirements

- .NET 8 SDK or later

Check your installation:

```bash
dotnet --version
```

## Installation
Clone the repository:

```bash
git clone https://github.com/facut1no/GithubUserActivity.git
```

Navigate to the project directory:

```bash
cd GithubUserActivity
```

## Running the Project

Run the application with a GitHub username:

```bash
dotnet run <github-username>
```

Example:

```bash
dotnet run -- facut1no
```

## Example Output
```
[PUSH] Pushed a commit to facut1no/GithubUserActivity at 15/5/2026 8:11:05 p. m.
[PUSH] Pushed a commit to facut1no/GithubUserActivity at 15/5/2026 7:48:44 p. m.
[PUSH] Pushed a commit to facut1no/GithubUserActivity at 15/5/2026 7:44:11 p. m.
[CREATE] Created a new repository or branch in facut1no/GithubUserActivity at 15/5/2026 6:04:55 p. m.
```