﻿using eMagTemplateGenerator.Models;

namespace eMagTemplateGenerator.Static;

public static class SpecificationMapping
{
    public static Dictionary<string, string> MapToDict(this IEnumerable<SpecificationItem> items)
        => items.ToDictionary(item => item.Key, item => item.Value);
}