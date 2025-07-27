namespace UserDataViewerCoreTests;
using UserDataViewerCore; 

public class ValidationHelperTests
{
    [Theory]
    [InlineData("1", 1, true)]       
    [InlineData("100", 2, true)]      
    [InlineData("0", 3, false)]      
    [InlineData("-5", 4, false)]      
    [InlineData("abc", 5, false)]     
    public void ValidateId_TestCases(string id, int line, bool expectedValid)
    {
        var result = ValidationHelper.ValidateId(id, line);
        Assert.Equal(expectedValid, result.IsValid);
    }

    [Theory]
    [InlineData("John", 1, true)]            
    [InlineData("John Doe", 2, true)]       
    [InlineData("", 3, false)]             
    [InlineData("A", 4, false)]            
    [InlineData("john", 5, false)]         
    [InlineData("John1", 6, false)]       
    [InlineData(" John", 7, false)]       
    public void ValidateName_TestCases(string name, int line, bool expectedValid)
    {
        var result = ValidationHelper.ValidateName(name, line);
        Assert.Equal(expectedValid, result.IsValid);
    }

    [Theory]
    [InlineData("test@example.com", 1, true)]    
    [InlineData("user.name@domain.com", 2, true)]
    [InlineData("", 3, false)]                  
    [InlineData("invalid-email", 4, false)]   
    [InlineData("user@.com", 5, false)]          
    public void ValidateEmail_TestCases(string email, int line, bool expectedValid)
    {
        var result = ValidationHelper.ValidateEmail(email, line);
        Assert.Equal(expectedValid, result.IsValid);
    }

    [Theory]
    [InlineData("Male", 1, true)]       
    [InlineData("Female", 2, true)]   
    [InlineData("male", 3, false)]      
    [InlineData("Other", 4, false)]     
    [InlineData("", 5, false)]      
    public void ValidateGender_TestCases(string gender, int line, bool expectedValid)
    {
        var result = ValidationHelper.ValidateGender(gender, line);
        Assert.Equal(expectedValid, result.IsValid);
    }

    [Theory]
    [InlineData("192.168.1.1", 1, true)]       
    [InlineData("255.255.255.255", 2, true)]  
    [InlineData("256.0.0.1", 3, false)]      
    [InlineData("invalid", 5, false)]          
    public void ValidateIpAddress_TestCases(string ip, int line, bool expectedValid)
    {
        var result = ValidationHelper.ValidateIpAddress(ip, line);
        Assert.Equal(expectedValid, result.IsValid);
    }

    [Fact]
    public void ValidateName_ErrorMessages_ContainLineNumber()
    {
        int lineNumber = 42;
        var result = ValidationHelper.ValidateName("invalid name", lineNumber);
        
        Assert.False(result.IsValid);
        Assert.Contains($"Строка {lineNumber}:", result.ErrorMessage);
    }

    [Fact]
    public void ValidateEmail_ErrorMessages_ContainTimestamp()
    {
        var result = ValidationHelper.ValidateEmail("invalid", 1);
        
        Assert.False(result.IsValid);
        Assert.Contains("Время ошибки:", result.ErrorMessage);
    }
}