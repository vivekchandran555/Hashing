using Microsoft.EntityFrameworkCore;

namespace Reporting.WithHash;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ReportRequest> ReportRequests { get; set; }

    public DbSet<ReportRequestParameter> ReportRequestParameters { get; set; }
}
