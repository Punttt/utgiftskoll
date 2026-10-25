// En mpnadsbudget, ett budgetbelopp och en lista med utgifter.
namespace Utgiftskoll;

public class Budget
{
    // Hur mycket man kan spendera
    public decimal Limit { get; set; }

    // Alla utgifter som lagts till
    public List<Expense> Expenses { get; set; } = new List<Expense>();

    // Håller koll på vilket Id nästa utgift ska få
    public int NextId { get; set; } = 1;

    // Lägger till en ny uppgift och ger den ett unikt Id
    public void AddExpense(string description, decimal amount, string category)
    {
        var expense = new Expense
        {
            Id = NextId,
            Description = description,
            Amount = amount,
            Category = category
        };

        Expenses.Add(expense);
        NextId++; // Nästa utgift får ett nummer som är +1
    }

    // Tar bort utgiften med angivet Id , returnerar True om den finns.
    public bool RemoveExpense(int id)
    {
        int removed = Expenses.RemoveAll(e => e.Id == id);
        return removed > 0;
    }

    // Räknar ut hur mycket som spenderats totalt.
    public decimal TotalSpent()
    {
        return Expenses.Sum(e => e.Amount);
    }

    // Räknar ut hur mycket som är kvar av budgeten.
    public decimal Remaining()
    {
        return Limit - TotalSpent();
    }

    // Räkna ut summa av utgifter för varje kategori. 
    // mat och Mat räknas som samma kategori.
    public Dictionary<string, decimal> TotalByCategory()
    {
        return Expenses
        .GroupBy(e => e.Category, StringComparer.OrdinalIgnoreCase)
        .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
    }
}