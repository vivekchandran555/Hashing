namespace Reporting.WithoutHash;

public class ReportRequest
{
    private ReportRequest() { }

    private ReportRequest(string reportName, Dictionary<string, string> parameters)
    {
        ReportName = reportName;

        foreach (var kvp in parameters)
        {
            Parameters.Add(new ReportRequestParameter(kvp.Key, kvp.Value));
        }
    }

    public Guid Id { get; private set; } = Guid.NewGuid();

    public string ReportName { get; private set; } = string.Empty;

    public ICollection<ReportRequestParameter> Parameters { get; private set; } = [];

    public static ReportRequest Create(string reportName, Dictionary<string, string> parameters)
        => new(reportName, parameters);
}

public class ReportRequestParameter
{
    private ReportRequestParameter() { }

    public ReportRequestParameter(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid ReportRequestId { get; private set; }

    public ReportRequest ReportRequest { get; private set; } = default!;

    public string Name { get; private set; } = string.Empty;

    public string Value { get; private set; } = string.Empty;
}
