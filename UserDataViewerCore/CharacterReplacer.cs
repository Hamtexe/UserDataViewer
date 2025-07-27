namespace UserDataViewerCore;

using System;
using System.Collections.Generic;
using System.Text;

public class CharacterReplacer
{
    private static readonly Dictionary<char, char> RussianToEnglishMap = new Dictionary<char, char>
    {
        
        { 'А', 'A' }, 
        { 'В', 'B' }, 
        { 'С', 'C' }, 
        { 'Е', 'E' }, 
        { 'Н', 'H' }, 
        { 'К', 'K' }, 
        { 'М', 'M' }, 
        { 'О', 'O' }, 
        { 'Р', 'P' }, 
        { 'Т', 'T' }, 
        { 'Х', 'X' }, 
        { 'У', 'Y' }, 

        
        { 'а', 'a' }, 
        { 'с', 'c' }, 
        { 'е', 'e' }, 
        { 'о', 'o' }, 
        { 'р', 'p' }, 
        { 'у', 'y' }, 
        { 'х', 'x' }, 
        { 'Ё', 'E' }, 
        { 'ё', 'e' } 
    };

    public static string[] ReplaceRussianWithEnglish(string[] arrayWordsToChange)
    {
        if (arrayWordsToChange == null || arrayWordsToChange.Length == 0)
            return arrayWordsToChange;

        string[] result = new string[arrayWordsToChange.Length];
        
        for (int i = 0; i < arrayWordsToChange.Length; i++)
        {
            result[i] = ReplaceInString(arrayWordsToChange[i]);
        }

        return result;
    }

    private static string ReplaceInString(string wordToChange)
    {
        if (string.IsNullOrEmpty(wordToChange))
            return wordToChange;

        var result = new StringBuilder(wordToChange.Length);
        
        foreach (char c in wordToChange)
        {
            result.Append(RussianToEnglishMap.TryGetValue(c, out char replacement) ? replacement : c);
        }

        var stringResult = result.ToString();
        return CapitalizeFirstLetter(stringResult);
    }
    
    
    private static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
        
        return char.ToUpper(input[0]) + input.Substring(1);
    }
}
