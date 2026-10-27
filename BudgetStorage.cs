using System.Text.Json;

namespace Utgiftskoll;

// Sparar och läser budgeten från JSON fil.
public static class BudgetStorage
{
    // Filen som datan finns sparad i
    private const string FilePath = "budget.json";

    // Sparar hela budgeten (belopp, utgifter, id)
    public static void Save(Budget budget)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(budget, options);
        File.WriteAllText(FilePath, json);
    }

    // Läser budget från filen. Returnerar null om ingen fil finns eller om filen är trasig.
    public static Budget? Load()
    {
        if (!File.Exists(FilePath))
        {
            return null;
        }

        try
        {
            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<Budget>(json);
        }
        catch(JsonException)
        {
            // filen gick inte att läsa som JSON, så vi börjar om med en ny budget.
            return null;
        }
    }
}