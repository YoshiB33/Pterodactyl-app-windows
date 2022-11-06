namespace Pterodactyl_app.Models.AuthenticateWebsocket;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class Rootobject
{
    public string Event
    {
        get; set;
    }
    public string[] Args
    {
        get; set;
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
