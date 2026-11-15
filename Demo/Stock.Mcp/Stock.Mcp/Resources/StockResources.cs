namespace Stock.Mcp.Resources;

/// <summary>
/// Markdown sample resources for stock-control demos.
/// </summary>
[McpServerResourceType]
public class StockResources
{
    private const string extension = ".md";
    private static readonly Assembly assembly = typeof(StockResources).Assembly;
    private static readonly string prefix = $"{typeof(StockResources).Namespace}.Samples.";

    private static readonly Dictionary<string, string> resources =
        assembly.GetManifestResourceNames()
            .Where(name => name.StartsWith(prefix, StringComparison.Ordinal))
            .ToDictionary(
                name => name[prefix.Length..^extension.Length],
                name => name,
                StringComparer.OrdinalIgnoreCase);

    private static string Read(string name)
    {
        using var stream = assembly.GetManifestResourceStream(name) ??
            throw new McpProtocolException("The embedded stock sample could not be loaded.", McpErrorCode.InternalError);
        using StreamReader reader = new(stream);
        return reader.ReadToEnd();
    }

    private static string ReadSample(string name)
    {
        if (!resources.TryGetValue(name, out var resourceName))
            throw new McpProtocolException(
                $"Unknown sample '{name}'. Use stock://samples to list available samples.",
                McpErrorCode.InvalidParams);

        return Read(resourceName);
    }

    /// <summary>
    /// Gets the index of embedded stock sample resource URIs.
    /// </summary>
    [McpServerResource(UriTemplate = "stock://samples", Name = "stock_samples_index_markdown",
        Title = "Stock samples index", MimeType = "text/markdown")]
    [Description("List stock-control sample resources")]
    public string SamplesIndex()
    {
        if (resources.Count == 0)
            throw new McpProtocolException("No embedded stock samples were found.", McpErrorCode.InternalError);

        var sampleNames = resources.Keys.OrderBy(name => name, StringComparer.OrdinalIgnoreCase);
        return "# Stock samples\n\n" + string.Join(
            Environment.NewLine,
            sampleNames.Select(name => $"- stock://samples/{name}"));
    }

    /// <summary>
    /// Gets an embedded stock sample resource by name.
    /// </summary>
    /// <param name="name">Sample name with or without the .md extension.</param>
    /// <returns>The sample Markdown content.</returns>
    /// <exception cref="McpProtocolException">The sample is unknown or cannot be loaded.</exception>
    [McpServerResource(UriTemplate = "stock://samples/{name}", Name = "stock_sample_markdown",
        Title = "Stock sample", MimeType = "text/markdown")]
    [Description("Get a stock-control sample markdown resource")]
    public string SampleMarkdown([Description("Sample name from embedded resources, with or without .md")] string name)
    {
        var sampleName = name.EndsWith(extension, StringComparison.OrdinalIgnoreCase)
            ? name[..^extension.Length]
            : name;
        return ReadSample(sampleName);
    }

    /// <summary>Gets the brake pads stock sample.</summary>
    /// <returns>The brake pads sample as Markdown.</returns>
    /// <exception cref="McpProtocolException">The embedded sample cannot be loaded.</exception>
    [McpServerResource(UriTemplate = "stock://samples/brake-pads", Name = "brake_pads_sample_markdown",
        Title = "Brake pads sample", MimeType = "text/markdown")]
    public static string BrakePadsSample() => ReadSample("brake-pads");

    /// <summary>Gets the demo products stock sample.</summary>
    /// <returns>The demo products sample as Markdown.</returns>
    /// <exception cref="McpProtocolException">The embedded sample cannot be loaded.</exception>
    [McpServerResource(UriTemplate = "stock://samples/demo-products", Name = "demo_products_sample_markdown",
        Title = "Demo products sample", MimeType = "text/markdown")]
    public static string DemoProductsSample() => ReadSample("demo-products");

    /// <summary>Gets the sample demonstrating stock-control error paths.</summary>
    /// <returns>The error paths sample as Markdown.</returns>
    /// <exception cref="McpProtocolException">The embedded sample cannot be loaded.</exception>
    [McpServerResource(UriTemplate = "stock://samples/error-paths", Name = "error_paths_sample_markdown",
        Title = "Error paths sample", MimeType = "text/markdown")]
    public static string ErrorPathsSample() => ReadSample("error-paths");

    /// <summary>Gets the sample showing expected stock-control output.</summary>
    /// <returns>The expected output sample as Markdown.</returns>
    /// <exception cref="McpProtocolException">The embedded sample cannot be loaded.</exception>
    [McpServerResource(UriTemplate = "stock://samples/expected-output", Name = "expected_output_sample_markdown",
        Title = "Expected output sample", MimeType = "text/markdown")]
    public static string ExpectedOutputSample() => ReadSample("expected-output");

    /// <summary>Gets the low-stock sample.</summary>
    /// <returns>The low-stock sample as Markdown.</returns>
    /// <exception cref="McpProtocolException">The embedded sample cannot be loaded.</exception>
    [McpServerResource(UriTemplate = "stock://samples/low-stock", Name = "low_stock_sample_markdown",
        Title = "Low stock sample", MimeType = "text/markdown")]
    public static string LowStockSample() => ReadSample("low-stock");

    /// <summary>Gets the sales history sample.</summary>
    /// <returns>The sales history sample as Markdown.</returns>
    /// <exception cref="McpProtocolException">The embedded sample cannot be loaded.</exception>
    [McpServerResource(UriTemplate = "stock://samples/sales-history", Name = "sales_history_sample_markdown",
        Title = "Sales history sample", MimeType = "text/markdown")]
    public static string SalesHistorySample() => ReadSample("sales-history");

    /// <summary>Gets the stock sample catalogue.</summary>
    /// <returns>The sample catalogue as Markdown.</returns>
    /// <exception cref="McpProtocolException">The embedded sample cannot be loaded.</exception>
    [McpServerResource(UriTemplate = "stock://samples/sample-catalogue", Name = "sample_catalogue_markdown",
        Title = "Sample catalogue", MimeType = "text/markdown")]
    public static string SampleCatalogue() => ReadSample("sample-catalogue");
}