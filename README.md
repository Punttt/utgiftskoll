# Utgiftskoll

En konsolapplikation i C#/.NET för att hålla koll på en månadsbudget.
Användaren anger sin budget, lägger till och tar bort utgifter och ser hur mycket som är spenderat och kvar.

Projektuppgift i kursen Programmering i C#.NET vid Mittuniversitetet.

Länk till redovisning: https://youtu.be/yCYnuuP8xiE 

## Funktioner

- Ange en budget för månaden vid första start
- Lägga till utgifter med beskrivning, belopp och kategori
- Visa alla utgifter med totalt spenderat och kvar av budgeten
- Ta bort utgifter
- Visa summa per kategori
- Ändra budgetbeloppet
- Data sparas automatiskt i `budget.json` och finns kvar vid omstart
- Felaktig inmatning hanteras utan att programmet kraschar

## Köra projektet

Kräver [.NET SDK](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/Punttt/utgiftskoll.git
cd utgiftskoll
dotnet run
```

Välj i menyn genom att skriva siffran och trycka Enter. Välj `0` för att avsluta.

För att börja om med en ny månad, radera filen `budget.json`.

## Struktur

| Fil | Innehåll |
|---|---|
| `Program.cs` | Menyn och all in- och utmatning |
| `Budget.cs` | Budgetens logik: lägga till, ta bort, beräkningar |
| `Expense.cs` | Representerar en utgift |
| `BudgetStorage.cs` | Sparar och läser budgeten till/från JSON-fil |
