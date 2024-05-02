namespace HtmlTemplateGenerator.Models;

public record Specification
{
    public string? Title { get; init; } = null;
    public string? Text { get; set; } = null;
    public IEnumerable<SpecificationItem> Items { get; set; } = [];

    public IEnumerable<SpecificationItem> GenerateSpecificationItems(string text)
    {
        if (Text is null)
        {
            return [];
        }
        
        var items = text.Split("\n");
        var specificationItems = new List<SpecificationItem>();
        
        foreach (var item in items)
        {
            var keyValuePair = item.Split(":");
            if (keyValuePair.Length != 2)
            {
                continue;
            }
            
            var (key, value) = (keyValuePair[0].Trim(), keyValuePair[1].Trim());
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("Specification item is not in the correct format. Please make sure that the key and value are separated by a colon (:) and that there are no leading or trailing spaces.");
            }

            specificationItems.Add(new SpecificationItem { Key = key, Value = value });
        }

        return specificationItems;
    }
}

public record SpecificationItem
{
    public string Key { get; init; }
    public string Value { get; init; }
}