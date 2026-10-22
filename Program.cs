using Utgiftskoll;

// Skapa budget, beloppet är hårdkodat vid utveckling.
var budget = new Budget { Limit = 10000m };

// Tillfälliga testvärdet så att det finns något att visa vid testkörning - Tas bort senare
budget.AddExpense("Hyra", 5000m, "Hushåll");
budget.AddExpense("Bensin", 1200m, "Bil");
budget.AddExpense("Mat", 3000m, "Hushåll");

// En bool för att styra, visa meny eller itne.
bool running = true;

// Huvudloop som visar menyn tills att användaren väljer att avsluta
while (running)
{
   Console.WriteLine();
   Console.WriteLine("==== Utgiftskoll ====");
   Console.WriteLine("1. Lägg till utgift");
   Console.WriteLine("2. Visa alla utgifter");
   Console.WriteLine("3. Ta bort utgift");
   Console.WriteLine("4. Visa summa per kategori");
   Console.WriteLine("\n5. Avsluta");
   Console.Write("\nVälj: ");

   // Läsa in användaren val
   string? choice = Console.ReadLine();

   // Hanterar valen med en Switch-sats
   switch (choice)
   {
      case "1":
         AddExpenseMenu(budget);
         break;
      case "2":
         ShowExpenses(budget);
         break;
      case "3":
         Console.WriteLine("Detta val kommer snart...");
         break;
      case "4":
         Console.WriteLine("Detta val kommer snart...");
         break;
      case "5":
         running = false; // Avslutar while loopen
         Console.WriteLine("Tack för den här gången!");
         break;
      default:
         Console.WriteLine("Ogiltigt val, vänligen försök igen!");
         break;

   }
}

// Skriver ut alla utgifter,
static void ShowExpenses(Budget budget)
{
   Console.Clear();
   if (budget.Expenses.Count == 0)
   {
      Console.WriteLine("Inga utgifter inlagd!");
      return;
   }
   
   Console.WriteLine("\n========== Utgifter ==========\n");
   foreach (var e in budget.Expenses)
   {
      Console.WriteLine($"{e.Id}. {e.Description} ({e.Category}): {e.Amount} kr");
   }

   Console.WriteLine($"\nBudget: {budget.Limit} kr");
   Console.WriteLine($"Spenderat: {budget.TotalSpent()} kr");
   Console.WriteLine($"Kvar: {budget.Remaining()} kr");
   Console.WriteLine("\n==============================\n");
}

// Lägger till utgift
static void AddExpenseMenu(Budget budget)
{
   Console.Clear();
   Console.WriteLine("\n====== Lägg till utgift ======\n");

   string description = ReadText("Beskrivning: ");
   decimal amount = ReadAmount("Belopp (Kr): ");
   string category = ReadText("Kategroi: ");

   budget.AddExpense(description, amount, category);
   Console.WriteLine($"\n{description} på {amount} kr har lagts till!");
}

// Frågar efter text tills användaren skriver något som inte är tomt
static string ReadText(string prompt)
{
   while (true)
   {
      Console.Write(prompt);
      string? input = Console.ReadLine();

      if( !string.IsNullOrWhiteSpace(input))
      {
         return input.Trim();
      }

      Console.WriteLine("Fältet får inte vara tomt, försök igen.");
   }
}