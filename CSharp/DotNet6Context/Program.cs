// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

public class SampleContext: DbContext
{
    public DbSet<SampleClass> SampleClass => Set<SampleClass>();
}

public class SampleClass
{
    public string Name { get; set; } = "Unnamed";
    public string Description { get; set; } = "No description.";
}
