
using System.Text.RegularExpressions;

namespace UserDataViewerCoreTests;

public class CharacterReplacerTests
{
    [Fact]
    public void ReplaceRussianWithEnglish_WhenNullInput_ReturnsNull()
    {

        string[] input = null;
        
        var result = UserDataViewerCore.CharacterReplacer.ReplaceRussianWithEnglish(input);
        
        Assert.Null(result);
    }

    [Fact]
    public void ReplaceRussianWithEnglish_WhenEmptyArray_ReturnsEmptyArray()
    {
        var input = new string[0];
        
        var result = UserDataViewerCore.CharacterReplacer.ReplaceRussianWithEnglish(input);
        
        Assert.Empty(result);
    }

    [Fact]
    public void ReplaceRussianWithEnglish_WhenEmptyString_ReturnsEmptyString()
    {
        var input = new string[] { "" };
        
        var result = UserDataViewerCore.CharacterReplacer.ReplaceRussianWithEnglish(input);
        
        Assert.Equal("", result[0]);
    }

    [Fact]
    public void ReplaceRussianWithEnglish_WhenNoRussianLetters_ReturnsSameString()
    {
        var input = new string[] { "Hello World!", "12345", "test@example.com" };

        var result = UserDataViewerCore.CharacterReplacer.ReplaceRussianWithEnglish(input);
        
        Assert.Equal("Hello World!", result[0]);
        Assert.Equal("12345", result[1]);
        Assert.Equal("test@example.com", result[2]);
    }

    [Fact]
    public void ReplaceRussianWithEnglish_WhenRussianLetters_ReplacesCorrectly()
    {
        var input = new string[] { "Привет", "Слово", "Русский Текст" };
        
        var result = UserDataViewerCore.CharacterReplacer.ReplaceRussianWithEnglish(input);
        
        Assert.Equal("Privet", result[0]);
        Assert.Equal("Clovo", result[1]);
        Assert.Equal("Pusskij Tekct", result[2]);
    }

    [Fact]
    public void ReplaceRussianWithEnglish_WhenMixedLetters_ReplacesOnlyRussian()
    {
        var input = new string[] { "HelloПривет", "TestСлово123", "РусскийText" };
        
        var result = UserDataViewerCore.CharacterReplacer.ReplaceRussianWithEnglish(input);
        
        Assert.Equal("HelloPrivet", result[0]);
        Assert.Equal("TestClovo123", result[1]);
        Assert.Equal("PusskijText", result[2]);
    }

    [Fact]
    public void ReplaceRussianWithEnglish_WhenSpecialCharacters_PreservesThem()
    {
        var input = new string[] { "Привет, мир!", "Слово-слово", "Текст@текст.рф" };
        
        var result = UserDataViewerCore.CharacterReplacer.ReplaceRussianWithEnglish(input);
        
        Assert.Equal("Privet, mir!", result[0]);
        Assert.Equal("Clovo-clovo", result[1]);
        Assert.Equal("Tekct@tekct.pф", result[2]); 
    }

    [Fact]
    public void ReplaceRussianWithEnglish_WhenMultipleWords_ProcessesAll()
    {
        var input = new string[] { "Первая строка", "Вторая строка", "Третья строка" };
        
        var result = UserDataViewerCore.CharacterReplacer.ReplaceRussianWithEnglish(input);
        
        Assert.Equal("Pervaja stroka", result[0]);
        Assert.Equal("Vtoraja stroka", result[1]);
        Assert.Equal("Tret'ja stroka", result[2]);
    }
}
