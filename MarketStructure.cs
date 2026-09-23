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

        foreach (var node in data.Nodes)
        {
            node.Id = string.IsNullOrWhiteSpace(node.Id)
                ? Guid.NewGuid().ToString("N")
                : node.Id;

            node.ParentId ??= "";
            node.Title = (node.Title ?? "").Trim();
        }

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