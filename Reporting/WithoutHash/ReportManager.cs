using Microsoft.EntityFrameworkCore;

namespace Reporting.WithoutHash;

public interface IReportManager
{
    Task<byte[]> GenerateReportAsync(string reportName, Dictionary<string, string> parameters);
}

public class ReportManager(AppDbContext dbContext) : IReportManager
{
    public async Task<byte[]> GenerateReportAsync(string reportName, Dictionary<string, string> parameters)
    {
        ReportRequest reportRequest = await CreateReportRequest(reportName, parameters);

        ReportRequest? existingReportRequest = await GetMatchingReportRequestAsync(reportRequest);

        if (existingReportRequest != null)
        {
            return await GetReportAsync(existingReportRequest.Id);
        }

        return await GenerateReportAsync(reportRequest);
    }

    private async Task<ReportRequest?> GetMatchingReportRequestAsync(ReportRequest reportRequest)
    {
        var inputParams = reportRequest.Parameters.ToDictionary(p => p.Name, p => p.Value);

        var candidates = await dbContext.ReportRequests
            .Include(r => r.Parameters)
            .Where(r => r.ReportName == reportRequest.ReportName)
            .ToListAsync();

        return candidates.FirstOrDefault(r =>
            r.Parameters.Count == inputParams.Count &&
            r.Parameters.All(p => inputParams.TryGetValue(p.Name, out var val) && val == p.Value));

        // Alternative implementation using LINQ to filter directly
        //return await dbContext.ReportRequests
        //    .Include(r => r.Parameters)
        //    .FirstOrDefaultAsync(r =>
        //        r.ReportName == reportRequest.ReportName &&
        //        r.Parameters.All(p => inputParams.ContainsKey(p.Name) && inputParams[p.Name] == p.Value) &&
        //        r.Parameters.Count == inputParams.Count);
    }


    private async Task<ReportRequest> CreateReportRequest(string reportName, Dictionary<string, string> parameters)
    {
        ReportRequest reportRequest = ReportRequest.Create(reportName, parameters);
        await dbContext.ReportRequests.AddAsync(reportRequest);
        await dbContext.SaveChangesAsync();
        return reportRequest;
    }

    private static Task<byte[]> GetReportAsync(Guid id)
        => Task.FromResult(id.ToByteArray());

    private static Task<byte[]> GenerateReportAsync(ReportRequest reportRequest)
        => Task.FromResult(reportRequest.Id.ToByteArray());
}
