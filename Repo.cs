internal sealed record Repo(
    long Id,
    string Name
)
{
    public override string ToString()
    {
        return $"Id: {Id}\nName: {Name}";
    }
}