namespace Pterodactyl_app.Models.EstablishWebsocket;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable IDE1006 // Naming Styles
public class Rootobject
{
    public Data data
    {
        get; set;
    }
}

public class Data
{
    public string token
    {
        get; set;
    }
    public string socket
    {
        get; set;
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore IDE1006 // Naming Styles
