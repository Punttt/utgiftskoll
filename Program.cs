using System.Data.Common;
using Utgiftskoll;

// Försök att läsa in sparad budget från fil.
var budget = BudgetStorage.Load();

// Finns ingen sparad budget får användaren ange en ny.
if ( budget == null )
{
   Console.WriteLine("Välkommen till Utgiftskoll! Ingen sparad budget hittades.");
   decimal limit = ReadAmount("Ange budget för månaden i kronor: ");
   budget = new Budget {Limit = limit };
   BudgetStorage.Save(budget);
}

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
   Console.WriteLine("5. Ändra budget");
   Console.WriteLine("\n0. Avsluta");
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
         RemoveExpenseMenu(budget);
         break;
      case "4":
         ShowCategoryTotals(budget);
         break;
      case "5":
         ChangeLimitMenu(budget);
         break;
      case "0":
         running = false; // Avslutar while loopen
         Console.WriteLine("Tack för den här gången!");
         break;
      default:
         Console.Clear();
         Console.WriteLine("(Ogiltigt val, vänligen försök igen!)");
         break;

   }
}



// Skriver ut alla utgifter,
// ========================================================
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
      Console.WriteLine($"{e.Description} ({e.Category}): {e.Amount} kr");
   }

   Console.WriteLine($"\nBudget: {budget.Limit} kr");
   Console.WriteLine($"Spenderat: {budget.TotalSpent()} kr");
   Console.WriteLine($"Kvar: {budget.Remaining()} kr");
   Console.WriteLine("\n==============================\n");
}



// Lägger till utgift
// ========================================================
static void AddExpenseMenu(Budget budget)
{
   Console.Clear();
   Console.WriteLine("\n====== Lägg till utgift ======\n");

   string description = ReadText("Beskrivning: ");
   decimal amount = ReadAmount("Belopp (Kr): ");
   string category = ReadText("Kategori: ");

   budget.AddExpense(description, amount, category);
   BudgetStorage.Save(budget);
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

// Frågar efter ett tal tills användaren skriver in ett giltigt tal större än 0
static decimal ReadAmount(string prompt)
{
   while (true)
   {
      Console.Write(prompt);
      string? input = Console.ReadLine();

      if (decimal.TryParse(input, out decimal amount) && amount > 0)
      {
         return amount;
      }

      Console.WriteLine("Ange ett giltigt belopp större än 0, T.ex 250 eller 99,50 ");
   }
}



// Låter användaren ta bort utgift genom att ange Id
// ========================================================
static void RemoveExpenseMenu(Budget budget)
{
   Console.Clear();
   Console.WriteLine("\n======= Ta bort utgift =======\n");

   // Om inga utgifter finns, avslutar direkt.
   if ( budget.Expenses.Count == 0 )
   {
      Console.WriteLine("Det finns inga utgifter att ta bort.");
      return;
   }

   // Visa lista så att användaren vet vad som ska tas bort, visa med id på varje utgift
   foreach( var e in budget.Expenses)
   {
      Console.WriteLine($"[{e.Id}] {e.Description} ({e.Category}): {e.Amount} kr");
   }

   int id = ReadId("\nAnge Id på utgiften som ska tas bort: ");

   // RemoveExpanse returnerar true om utgiften fanns och togs bort.
   if( budget.RemoveExpense(id) )
   {
      BudgetStorage.Save(budget);
      Console.WriteLine($"\nUtgiften med id {id} har tagits bort.");
   }
   else
   {
      Console.WriteLine($"\nDet finns ingen utgift med id {id}");
   }
}

// Frågar om ett heltal tills att användare skriver ett giltigt.
static int ReadId(string prompt)
{
   while(true)
   {
      Console.Write(prompt);
      string? input = Console.ReadLine();

      if(int.TryParse(input, out int id))
      {
         return id;
      }

      Console.WriteLine("Ange ett heltar, T.ex. 2.");
   }
}

// Visa hur mycket som spenderats i varje kkategori
// ========================================================
static void ShowCategoryTotals(Budget budget)
{
   Console.Clear();
   Console.WriteLine("\n===== Summa per kategori =====\n");

   // Om inga utgifter finns, avslutar direkt.
   if ( budget.Expenses.Count == 0 )
   {
      Console.WriteLine("Det finns inga utgifter inlagd.");
      return;
   }

   // Går igenom varje kategori och skriver ut summan
   foreach(var item in budget.TotalByCategory())
   {
      Console.WriteLine($"{item.Key}: {item.Value} kr");
   }
}

// Uppdaterar budget-beloppet.
// ========================================================
static void ChangeLimitMenu(Budget budget)
{
   Console.Clear();
   Console.WriteLine("\n======== Ändra budget ========\n");
   Console.WriteLine($"Nuvarande budget: {budget.Limit} kr\n");

   budget.Limit = ReadAmount("Ny budget (kr): ");
   BudgetStorage.Save(budget);

   Console.WriteLine($"Budget är nu ändrad till: {budget.Limit} kr");
}