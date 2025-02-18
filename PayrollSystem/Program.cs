using System.Text.Json;
using PayrollSystem;


string jsonString = File.ReadAllText("input.json");
Input input = JsonSerializer.Deserialize<Input>(jsonString,  new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

var output = PayCalculator.CalculateNetPay(input);

Console.WriteLine("");