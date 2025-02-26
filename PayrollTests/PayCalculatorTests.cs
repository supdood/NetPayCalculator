using System.Runtime.CompilerServices;
using System.Text.Json;
using PayrollSystem;

namespace PayrollTests;

public class PayCalculatorTests
{
    [Fact]
    public void CalculateNetPay_ReturnsExpected()
    {
        //Arrange
        string jsonString = File.ReadAllText("test-cases.json");
        var testCases = JsonSerializer.Deserialize<TestCases>(jsonString,  new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        foreach(var testCase in testCases)
        {
            var input = testCase.Input;
            var expectedOutput = testCase.Output;

            //Act
            var actualOutput = PayCalculator.CalculateNetPay(input);

            //Assert
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}

