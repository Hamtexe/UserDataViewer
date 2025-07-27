namespace UserDataViewerCore;

using System;
using System.Collections.Generic;
using System.Text;

public class CharacterReplacer
{
    public static string[] ReplaceRussianWithEnglish(string[] arrayWordsToChange)
    {
        if (arrayWordsToChange.Length == 0)
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
            result.Append(Constants.RussianToEnglishMap.TryGetValue(c, out char replacement) ? replacement : c);
        }

        var stringResult = result.ToString();
        return CapitalizeFirstLetter(stringResult);
    }
    
    
    private static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) 
            return input;
        
        return char.ToUpper(input[0]) + input.Substring(1);
    }
}
