﻿namespace Pterodactyl_app.Models.WebSocketModel;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class Rootobject
{
    public string @event
    {
        get; set;
    }
    public string[] args
    {
        get; set;
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
