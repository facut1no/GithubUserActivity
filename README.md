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

## Running the Project
```
dotnet run -- <github-username>
```

## Example Output
```
[PUSH] Pushed a commit to facut1no/GithubUserActivity
[STAR] Starred the repository golang/go
[CREATE] Created a new repository in facut1no/ApiRedSocial
```