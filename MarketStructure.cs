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

    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

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

            var data = JsonSerializer.Deserialize<MarketStructureData>(
                File.ReadAllText(FilePath)) ?? CreateDefault();

            Normalize(data);
            return data;
        }
        catch
        {
            return CreateDefault();
        }
    }

    public static void Save(MarketStructureData data)
    {
        Normalize(data);
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
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

        data.AssetCategories = data.AssetCategories
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    public static MarketStructureData CreateDefault()
    {
        var data = new MarketStructureData();

        AddTree(data, "بورس اوراق بهادار تهران",
            ("بازار اول",
                ("تابلوی اصلی"),
                ("تابلوی فرعی")),
            ("بازار دوم"),
            ("بازار صندوق‌های سرمایه‌گذاری"));

        AddTree(data, "فرابورس ایران",
            ("بازار اول"),
            ("بازار دوم"),
            ("بازار پایه",
                ("بازار پایه زرد"),
                ("بازار پایه نارنجی"),
                ("بازار پایه قرمز")),
            ("بازار شرکت‌های کوچک و متوسط"),
            ("بازار ابزارهای نوین مالی",
                ("صندوق‌های سرمایه‌گذاری"),
                ("سایر ابزارهای مالی")),
            ("بازار مشتقه",
                ("اوراق اختیار معامله"),
                ("قراردادهای آتی")));

        AddTree(data, "بورس کالای ایران",
            ("بازار مشتقه",
                ("قراردادهای آتی"),
                ("قراردادهای اختیار معامله")),
            ("بازار مالی",
                ("گواهی سپرده کالایی")));

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

    private static void AddTree(
        MarketStructureData data,
        string rootTitle,
        params (string Title, (string Title, (string Title)[] Children)[] Children)[] children)
    {
        var root = AddNode(data, "", rootTitle);

        foreach (var child in children)
        {
            var childNode = AddNode(data, root.Id, child.Title);

            foreach (var grandChild in child.Children)
                AddNode(data, childNode.Id, grandChild.Title);
        }
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
            SortOrder = data.Nodes.Count(x => x.ParentId == parentId)
        };

        data.Nodes.Add(node);
        return node;
    }
}
