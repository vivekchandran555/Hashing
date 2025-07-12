using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var options1 = new DbContextOptionsBuilder<Reporting.WithoutHash.AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
using var dbContext1 = new Reporting.WithoutHash.AppDbContext(options1);
Reporting.WithoutHash.ReportManager reportManager1 = new(dbContext1);
await reportManager1.GenerateReportAsync("SalesReport",
    new Dictionary<string, string>
    {
        { "StartDate", "2023-01-01" },
        { "EndDate", "2023-12-31" },
        { "Region", "North America" },
        { "Category", "Electronics" },
        { "Format", "PDF" },
        { "IncludeCharts", "true" },
        { "IncludeSummary", "true" },
        { "Language", "en-US" },
        { "Timezone", "America/New_York" },
        { "Currency", "USD" },
        { "CustomerType", "Retail" },
        { "SalesChannel", "Online" },
        { "IncludeDiscounts", "true" },
        { "IncludeReturns", "false" },
        { "SortBy", "Date" },
        { "SortOrder", "Descending" }
    });

Console.WriteLine("Hello, World!");

var options2 = new DbContextOptionsBuilder<Reporting.WithHash.AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
using var dbContext2 = new Reporting.WithHash.AppDbContext(options2);
Reporting.WithHash.ReportManager reportManager2 = new(dbContext2, new Reporting.WithHash.HashProvider());
await reportManager2.GenerateReportAsync("SalesReport",
    new Dictionary<string, string>
    {
        { "StartDate", "2023-01-01" },
        { "EndDate", "2023-12-31" },
        { "Region", "North America" },
        { "Category", "Electronics" },
        { "Format", "PDF" },
        { "IncludeCharts", "true" },
        { "IncludeSummary", "true" },
        { "Language", "en-US" },
        { "Timezone", "America/New_York" },
        { "Currency", "USD" },
        { "CustomerType", "Retail" },
        { "SalesChannel", "Online" },
        { "IncludeDiscounts", "true" },
        { "IncludeReturns", "false" },
        { "SortBy", "Date" },
        { "SortOrder", "Descending" }
    });

Console.WriteLine("Hello, World!");