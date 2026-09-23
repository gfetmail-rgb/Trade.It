using System.Text.Json;

namespace Trade.It;

public sealed class MarketStructureNode
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string ParentId { get; set; } = "";
    public string Title { get; set; } = "";
    public int SortOrder { get; set; }
}

public sealed class MarketStructureData
{
    public List<MarketStructureNode> Nodes { get; set; } = new();
    public List<string> AssetCategories { get; set; } = new();
    public List<string> OtherCategories { get; set; } = new();
}

public static class MarketStructureStore
{
    private static string FilePath =>
        Path.Combine(AppContext.BaseDirectory, "Data", "MarketStructure.json");

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    public static MarketStructureData Load()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                var data = CreateDefault();
                Save(data);
                return data;
            }

            var loadedData = JsonSerializer.Deserialize<MarketStructureData>(
                File.ReadAllText(FilePath)) ?? CreateDefault();

            Normalize(loadedData);
            return loadedData;
        }
        catch
        {
            return CreateDefault();
        }
    }

    public static void Save(MarketStructureData data)
    {
        Normalize(data);

        Directory.CreateDirectory(
            Path.GetDirectoryName(FilePath)!);

        File.WriteAllText(
            FilePath,
            JsonSerializer.Serialize(data, Options),
            new System.Text.UTF8Encoding(false));
    }

    private static void Normalize(MarketStructureData data)
    {
        data.Nodes ??= new();
        data.AssetCategories ??= new();
        data.OtherCategories ??= new();

        var usedIds = new HashSet<string>(StringComparer.Ordinal);
        var duplicateIds = new HashSet<string>(StringComparer.Ordinal);

        foreach (var node in data.Nodes)
        {
            node.Id = string.IsNullOrWhiteSpace(node.Id)
                ? Guid.NewGuid().ToString("N")
                : node.Id.Trim();

            if (!usedIds.Add(node.Id))
            {
                duplicateIds.Add(node.Id);
                node.Id = CreateUniqueId(usedIds);
            }

            node.ParentId = (node.ParentId ?? "").Trim();
            node.Title = (node.Title ?? "").Trim();
        }

        var nodeIds = data.Nodes
            .Select(x => x.Id)
            .ToHashSet(StringComparer.Ordinal);

        foreach (var node in data.Nodes)
        {
            if (string.IsNullOrWhiteSpace(node.ParentId))
                continue;

            if (duplicateIds.Contains(node.ParentId) ||
                string.Equals(node.ParentId, node.Id, StringComparison.Ordinal) ||
                !nodeIds.Contains(node.ParentId))
            {
                node.ParentId = "";
            }
        }

        BreakParentCycles(data);

        RecalculateSortOrders(data);

        if (!data.Nodes.Any(x =>
            string.IsNullOrEmpty(x.ParentId) &&
            string.Equals(x.Title, "بورس انرژی ایران", StringComparison.OrdinalIgnoreCase)))
        {
            AddNode(data, "", "بورس انرژی ایران");
        }

        data.AssetCategories = data.AssetCategories
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        data.OtherCategories = data.OtherCategories
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static void BreakParentCycles(MarketStructureData data)
    {
        var byId = data.Nodes.ToDictionary(x => x.Id, StringComparer.Ordinal);

        foreach (var start in data.Nodes)
        {
            var path = new HashSet<string>(StringComparer.Ordinal);
            var current = start;

            while (!string.IsNullOrWhiteSpace(current.ParentId))
            {
                if (!path.Add(current.Id))
                {
                    current.ParentId = "";
                    break;
                }

                if (!byId.TryGetValue(current.ParentId, out current!))
                {
                    current.ParentId = "";
                    break;
                }
            }
        }
    }

    private static string CreateUniqueId(HashSet<string> usedIds)
    {
        string id;
        do
        {
            id = Guid.NewGuid().ToString("N");
        }
        while (!usedIds.Add(id));

        return id;
    }

    private static void RecalculateSortOrders(MarketStructureData data)
    {
        foreach (var group in data.Nodes
            .GroupBy(x => x.ParentId, StringComparer.Ordinal))
        {
            var ordered = group
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Title, StringComparer.OrdinalIgnoreCase)
                .ToList();

            for (var i = 0; i < ordered.Count; i++)
                ordered[i].SortOrder = i;
        }
    }

    public static MarketStructureData CreateDefault()
    {
        var data = new MarketStructureData();

        AddBranch(data, "", "بورس اوراق بهادار تهران", new[]
        {
            "بازار اول",
            "بازار دوم",
            "بازار صندوق‌های سرمایه‌گذاری"
        });

        var tehranFirst = data.Nodes.First(
            x => x.Title == "بازار اول");

        AddNode(data, tehranFirst.Id, "تابلوی اصلی");
        AddNode(data, tehranFirst.Id, "تابلوی فرعی");

        AddBranch(data, "", "فرابورس ایران", new[]
        {
            "بازار اول",
            "بازار دوم",
            "بازار پایه",
            "بازار شرکت‌های کوچک و متوسط",
            "بازار ابزارهای نوین مالی",
            "بازار مشتقه"
        });

        var otcBase = data.Nodes.First(
            x => x.Title == "بازار پایه");

        AddNode(data, otcBase.Id, "بازار پایه زرد");
        AddNode(data, otcBase.Id, "بازار پایه نارنجی");
        AddNode(data, otcBase.Id, "بازار پایه قرمز");

        var otcModern = data.Nodes.First(
            x => x.Title == "بازار ابزارهای نوین مالی");

        AddNode(data, otcModern.Id, "صندوق‌های سرمایه‌گذاری");
        AddNode(data, otcModern.Id, "سایر ابزارهای مالی");

        var otcDerivatives = data.Nodes.First(
            x => x.Title == "بازار مشتقه");

        AddNode(data, otcDerivatives.Id, "اوراق اختیار معامله");
        AddNode(data, otcDerivatives.Id, "قراردادهای آتی");

        AddBranch(data, "", "بورس کالای ایران", new[]
        {
            "بازار مشتقه",
            "بازار مالی"
        });

        var commodityDerivatives = data.Nodes.First(x =>
            x.Title == "بازار مشتقه" &&
            x.ParentId ==
            data.Nodes.First(
                y => y.Title == "بورس کالای ایران").Id);

        AddNode(
            data,
            commodityDerivatives.Id,
            "قراردادهای آتی");

        AddNode(
            data,
            commodityDerivatives.Id,
            "قراردادهای اختیار معامله");

        var commodityFinancial = data.Nodes.First(x =>
            x.Title == "بازار مالی" &&
            x.ParentId ==
            data.Nodes.First(
                y => y.Title == "بورس کالای ایران").Id);

        AddNode(
            data,
            commodityFinancial.Id,
            "گواهی سپرده کالایی");

        data.AssetCategories.AddRange(new[]
        {
            "سهام",
            "صندوق سرمایه‌گذاری",
            "اوراق",
            "اختیار معامله",
            "قراردادهای آتی",
            "گواهی سپرده کالایی",
            "سایر ابزارهای مالی"
        });

        return data;
    }

    private static void AddBranch(
        MarketStructureData data,
        string parentId,
        string title,
        IEnumerable<string> children)
    {
        var parent = AddNode(data, parentId, title);

        foreach (var child in children)
            AddNode(data, parent.Id, child);
    }

    private static MarketStructureNode AddNode(
        MarketStructureData data,
        string parentId,
        string title)
    {
        var node = new MarketStructureNode
        {
            ParentId = parentId,
            Title = title,
            SortOrder = data.Nodes.Count(
                x => x.ParentId == parentId)
        };

        data.Nodes.Add(node);
        return node;
    }
}