namespace Pterodactyl_app.Models.ResourcesAPIModel;

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


public class Rootobject
{
    public string _object
    {
        get; set;
    }
    public Attributes attributes
    {
        get; set;
    }
}

public class Attributes
{
    public string current_state
    {
        get; set;
    }
    public bool is_suspended
    {
        get; set;
    }
    public Resources resources
    {
        get; set;
    }
}

public class Resources
{
    public int memory_bytes
    {
        get; set;
    }
    public float cpu_absolute
    {
        get; set;
    }
    public int disk_bytes
    {
        get; set;
    }
    public int network_rx_bytes
    {
        get; set;
    }
    public int network_tx_bytes
    {
        get; set;
    }
    public int uptime
    {
        get; set;
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore IDE1006 // Naming Styles
