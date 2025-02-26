using System.Runtime.CompilerServices;
using System.Text.Json;
using PayrollSystem;

namespace PayrollTests;

public class OutputTests
{
    [Fact]
    public void Equals_WitholdingsAreEqual_ReturnTrue()
    {
        //Arrange
        var withholdingOne = new Withholding { AmountWithheld = 10, Code = WithholdingCode.FederalIncome, Deficit = 0, Type = WithholdingType.Deduction};
        var withholdingTwo = new Withholding { AmountWithheld = 10, Code = WithholdingCode.FederalIncome, Deficit = 0, Type = WithholdingType.Deduction};

        //Act
        var withholdingsAreEqual = withholdingOne.Equals(withholdingTwo);

        //Assert
        Assert.True(withholdingsAreEqual);
    }

    [Fact]
    public void Equals_WitholdingsAreNotEqual_ReturnFalse()
    {
        //Arrange
        var withholdingOne = new Withholding { AmountWithheld = 10, Code = WithholdingCode.FederalIncome, Deficit = 0, Type = WithholdingType.Deduction};
        var withholdingTwo = new Withholding { AmountWithheld = 10, Code = WithholdingCode.FederalIncome, Deficit = 0, Type = WithholdingType.Tax};

        //Act
        var withholdingsAreEqual = withholdingOne.Equals(withholdingTwo);

        //Assert
        Assert.False(withholdingsAreEqual);
    }

    [Fact]
    public void Equals_OutputsAreEqual_ReturnTrue()
    {
        //Arrange
        var outputOne = new Output { GrossPay = 200, GrossTaxableEarnings = 100, NetPay = 280, Withholdings = new List<Withholding>(){ new Withholding { AmountWithheld = 20, Code = WithholdingCode.FederalIncome, Deficit = 0, Type = WithholdingType.Tax }}};
        var outputTwo = new Output { GrossPay = 200, GrossTaxableEarnings = 100, NetPay = 280, Withholdings = new List<Withholding>(){ new Withholding { AmountWithheld = 20, Code = WithholdingCode.FederalIncome, Deficit = 0, Type = WithholdingType.Tax }}};

        //Act
        var outputsAreEqual = outputOne.Equals(outputTwo);

        //Assert
        Assert.True(outputsAreEqual);
    }

    [Fact]
    public void Equals_OutputsAreNotEqual_ReturnFalse()
    {
        //Arrange
        var outputOne = new Output { GrossPay = 200, GrossTaxableEarnings = 100, NetPay = 280, Withholdings = new List<Withholding>(){ new Withholding { AmountWithheld = 20, Code = WithholdingCode.FederalIncome, Deficit = 0, Type = WithholdingType.Tax }}};
        var outputTwo = new Output { GrossPay = 200, GrossTaxableEarnings = 100, NetPay = 300, Withholdings = new List<Withholding>()};

        //Act
        var outputsAreEqual = outputOne.Equals(outputTwo);

        //Assert
        Assert.False(outputsAreEqual);
    }
}

