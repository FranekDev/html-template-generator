using HtmlTemplateGenerator.Exceptions;
using HtmlTemplateGenerator.Models;

namespace HtmlTemplateGenerator.Static;

public static class SpecificationHelper
{
    public static void GenerateSpecificationItems(this List<SpecificationItem> specificationItems, string? text)
    {
        if (text is null)
        {
            return;
        }
        
        var items = text.Split("\n");
        
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
                throw new WrongSpecificationItemFormatException("Specification item is not in the correct format. Please make sure that the key and value are separated by a colon (:) and that there are no leading or trailing spaces.");
            }

            specificationItems.Add(new SpecificationItem { Key = key, Value = value });
        }
    }
}