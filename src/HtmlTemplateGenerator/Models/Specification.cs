namespace HtmlTemplateGenerator.Models;

public record Specification
{
    public string Title { get; init; }
    public string? Text { get; set; }
    public IEnumerable<SpecificationItem> Items { get; set; } = [];

    public IEnumerable<SpecificationItem> GenerateSpecificationItems(string text)
    {
        var specificationItems = new List<SpecificationItem>();
        if (Text is null)
        {
            return specificationItems;
        }
        
        var items = text.Split("\n");
        
        foreach (var item in items)
        {
            var keyValuePair = item.Split(":");
            if (keyValuePair.Length == 2)
            {
                specificationItems.Add(new SpecificationItem
                {
                    Key = keyValuePair[0].Trim(),
                    Value = keyValuePair[1].Trim()
                });
            }
        }

        return specificationItems;
    }
}

public record SpecificationItem
{
    public string Key { get; init; }
    public string Value { get; init; }
}