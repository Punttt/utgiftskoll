// Representerar en utgift - exempel Mat 250 kronor

public class Expense
{
    // Unikt nummer för en utgift, för att sedan kunna ändra eller ta bort
    public int Id { get; set; }

    // Kort text om vad utgiften är
    public string Description { get; set; } = "";

    // Belopp i kronor, decimal används för att få exakt uträkning
    public decimal Amount { get; set; }

    // Vilken kategori utgiftern hör till - exempel "Mat", "Hushåll", "Nöje"
    public string Category { get; set; } = "";
}