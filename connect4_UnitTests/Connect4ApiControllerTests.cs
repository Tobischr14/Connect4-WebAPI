namespace connect4_UnitTests;
using Microsoft.AspNetCore.Mvc;
using Xunit;

public class Connect4ApiControllerTests
{
    [Theory]
    [InlineData("diagonal_winner_200_A.txt")]
    [InlineData("diagonal_winner_200_B.txt")]
    [InlineData("empty_board_no_winner_200_X.txt")]
    [InlineData("floating_piece_error_400_I.txt")]
    [InlineData("full_board_no_winner_200_X.txt")]
    [InlineData("horizontal_winner_200_A.txt")]
    [InlineData("horizontal_winner_200_B.txt")]
    [InlineData("input_wrong_size_400_I.txt")]
    [InlineData("multiple_winner_400_I.txt")]
    [InlineData("ongoing_no_winner_200_X.txt")]
    [InlineData("vertical_winner_200_A.txt")]
    [InlineData("vertical_winner_200_B.txt")]
    // naming convention UnitOfWork_StateUnterTest_ExpectedBehaviour()
    public void CheckWinner_InputTest_ReturnsStatusCodeAndWinner(string fileName)
    {

        // (in development)loop through testfiles
        // string[] getFilePaths = Directory.GetFiles("../../../../tests", "*.txt");
        
                
        // Arrange

        // read input and expected values from file and filename
        // testfile naming convention Name_ExpectedStatusCode_ExpectedWinner.txt
        var filePath = $"../../../../test/{fileName}";
        int nameLength = fileName.Length;
        string statuscode_str = "";

        for (int i = nameLength - 9; i < nameLength - 6; i++)
        {
            statuscode_str = statuscode_str + fileName[i];
        }

        int expectedStatusCode = Int16.Parse(statuscode_str);
        char expectedResult = fileName[nameLength - 5];
        string input = File.ReadAllText($"{filePath}");

        
        // Act
        var controller = new connect4_api.Controllers.Connect4ApiController();
        var response = controller.CheckWinner(input);

        // Assert
        var result = response as ObjectResult;
        string winner = result.Value.ToString();

        var statuscode = result.StatusCode;
        
        Assert.NotNull(result);
        Assert.Equal(expectedStatusCode, statuscode);
        Assert.Equal(expectedResult, winner[0]);
        
    }

    [Fact]
    public void CheckWinner_WinnerIsA_ResultIsChar()
    {
        // Arrange
        string input = "AXXXXXBAXXXXBAAXXXBBAAXXXXXXXXXXXXXXBXXXXX";
        var controller = new connect4_api.Controllers.Connect4ApiController();
    
        // Act
        var response = controller.CheckWinner(input);

        // Assert
        var result = response as ObjectResult;
        var winner = result.Value;

        Assert.NotNull(result);
        Assert.IsType<char>(winner);
    }
}
