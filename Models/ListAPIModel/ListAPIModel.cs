namespace Pterodactyl_app.Models.ListAPIModel;

#pragma warning disable IDE1006

public class Rootobject
{
    public string? _object
    {
        get; set;
    }
    public Datum[]? data
    {
        get; set;
    }
    public Meta? meta
    {
        get; set;
    }
}

public class Meta
{
    public Pagination? pagination
    {
        get; set;
    }
}

public class Pagination
{
    public int total
    {
        get; set;
    }
    public int count
    {
        get; set;
    }
    public int per_page
    {
        get; set;
    }
    public int current_page
    {
        get; set;
    }
    public int total_pages
    {
        get; set;
    }
    public Links? links
    {
        get; set;
    }
}

public class Links
{
}

public class Datum
{
    public string? _object
    {
        get; set;
    }
    public Attributes? attributes
    {
        get; set;
    }
}

public class Attributes
{
    public bool server_owner
    {
        get; set;
    }
    public string? identifier
    {
        get; set;
    }
    public int internal_id
    {
        get; set;
    }
    public string? uuid
    {
        get; set;
    }
    public string? name
    {
        get; set;
    }
    public string? node
    {
        get; set;
    }
    public Sftp_Details? sftp_details
    {
        get; set;
    }
    public string? description
    {
        get; set;
    }
    public Limits? limits
    {
        get; set;
    }
    public string? invocation
    {
        get; set;
    }
    public string? docker_image
    {
        get; set;
    }
    public string[]? egg_features
    {
        get; set;
    }
    public Feature_Limits? feature_limits
    {
        get; set;
    }
    public object? status
    {
        get; set;
    }
    public bool is_suspended
    {
        get; set;
    }
    public bool is_installing
    {
        get; set;
    }
    public bool is_transferring
    {
        get; set;
    }
    public Relationships? relationships
    {
        get; set;
    }
}

public class Sftp_Details
{
    public string? ip
    {
        get; set;
    }
    public int port
    {
        get; set;
    }
}

public class Limits
{
    public int memory
    {
        get; set;
    }
    public int swap
    {
        get; set;
    }
    public int disk
    {
        get; set;
    }
    public int io
    {
        get; set;
    }
    public int cpu
    {
        get; set;
    }
    public string? threads
    {
        get; set;
    }
    public bool oom_disabled
    {
        get; set;
    }
}

public class Feature_Limits
{
    public int databases
    {
        get; set;
    }
    public int allocations
    {
        get; set;
    }
    public int backups
    {
        get; set;
    }
}

public class Relationships
{
    public Allocations? allocations
    {
        get; set;
    }
    public Variables? variables
    {
        get; set;
    }
}

public class Allocations
{
    public string? _object
    {
        get; set;
    }
    public Datum1[]? data
    {
        get; set;
    }
}

public class Datum1
{
    public string? _object
    {
        get; set;
    }
    public Attributes1? attributes
    {
        get; set;
    }
}

public class Attributes1
{
    public int id
    {
        get; set;
    }
    public string? ip
    {
        get; set;
    }
    public string? ip_alias
    {
        get; set;
    }
    public int port
    {
        get; set;
    }
    public object? notes
    {
        get; set;
    }
    public bool is_default
    {
        get; set;
    }
}

public class Variables
{
    public string? _object
    {
        get; set;
    }
    public Datum2[]? data
    {
        get; set;
    }
}

public class Datum2
{
    public string? _object
    {
        get; set;
    }
    public Attributes2? attributes
    {
        get; set;
    }
}

public class Attributes2
{
    public string? name
    {
        get; set;
    }
    public string? description
    {
        get; set;
    }
    public string? env_variable
    {
        get; set;
    }
    public string? default_value
    {
        get; set;
    }
    public string? server_value
    {
        get; set;
    }
    public bool is_editable
    {
        get; set;
    }
    public string? rules
    {
        get; set;
    }
}

