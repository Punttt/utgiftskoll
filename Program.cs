// Skapar en test utgift.
var expense = new Expense
{
   Id = 1,
   Description = "Bilförsäkring",
   Amount = 299,
   Category = "Hushåll" 
};

// Skriver ut test i konsolen
Console.WriteLine($"{expense.Description} : {expense.Amount} Kronor");