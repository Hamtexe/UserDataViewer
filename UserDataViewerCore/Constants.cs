using System.Text.RegularExpressions;

namespace UserDataViewerCore;

public class Constants
{
    //Словарь для подмены с русских букв на английские
    public static readonly Dictionary<char, char> RussianToEnglishMap = new Dictionary<char, char>
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
    
    //Регулярки для валидации
    public const string NameRegex = @"^[A-Za-z ]+$";
    public static readonly Regex EmailRegex = new Regex(
        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        RegexOptions.IgnoreCase
    );
    
    //Колонки
    public const string Id = "Id";
    public const string FirstName = "FirstName";
    public const string LastName = "LastName";
    public const string Email = "Email";
    public const string Gender = "Gender";
    public const string IpAddress = "IpAddress";
}