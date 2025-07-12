using Microsoft.EntityFrameworkCore;

namespace Reporting.WithHash;

public interface IReportManager
{
    Task<byte[]> GenerateReportAsync(string reportName, Dictionary<string, string> parameters);
}

public class ReportManager(AppDbContext dbContext, IHashProvider hashProvider) : IReportManager
{
    public async Task<byte[]> GenerateReportAsync(string reportName, Dictionary<string, string> parameters)
    {
        ReportRequest reportRequest = await CreateReportRequest(reportName, parameters);

        Console.WriteLine($"ReportRequest created with ID: {reportRequest.Id} and Hash: {reportRequest.Hash}");

        ReportRequest? existingReportRequest = await GetMatchingReportRequestAsync(reportRequest);

        if (existingReportRequest != null)
        {
            return await GetReportAsync(existingReportRequest.Id);
        }

        return await GenerateReportAsync(reportRequest);
    }

    public async Task<ReportRequest?> GetMatchingReportRequestAsync(ReportRequest reportRequest)
    {
        return await dbContext.ReportRequests
            .Include(r => r.Parameters)
            .FirstOrDefaultAsync(r => r.Hash == reportRequest.Hash);
    }

    private async Task<ReportRequest> CreateReportRequest(string reportName, Dictionary<string, string> parameters)
    {
        ReportRequest reportRequest = ReportRequest.Create(reportName, parameters, hashProvider);
        await dbContext.ReportRequests.AddAsync(reportRequest);
        await dbContext.SaveChangesAsync();
        return reportRequest;
    }

    private static Task<byte[]> GetReportAsync(Guid id)
        => Task.FromResult(id.ToByteArray());

    private static Task<byte[]> GenerateReportAsync(ReportRequest reportRequest)
        => Task.FromResult(reportRequest.Id.ToByteArray());
}
