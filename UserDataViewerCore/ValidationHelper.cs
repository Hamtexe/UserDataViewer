using System.Text.RegularExpressions;


namespace UserDataViewerCore
{
    public class ValidationHelper
    {
        public static (bool IsValid, int? id, string ErrorMessage) ValidateId(string ID, int i)
        {
            if (int.TryParse(ID, out int id))
            {
                if (id <= 0)
                {
                    return (false, id, $"Строка {i}: Некорректный ID '{ID}'.");
                }
                else return (true, id, "Ошибок нет");
            }
            else return (false, id, $"Строка {i}: Некорректный ID '{ID}'.");

        }


        public static (bool IsValid, string ErrorMessage) ValidateName(string name, int i)
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, $"Строка {i}: Имя не может быть пустым");

            if (name.Length < 2 || name.Length > 50)
                return (false, $"Строка {i}: Имя должно быть от 2 до 50 символов, введено - '{name}'");

            var errors = new List<string>();

            if (!Regex.IsMatch(name, Constants.NameRegex))
                errors.Add("Допустимы только английские буквы");

            if (name.Length > 0 && !char.IsUpper(name[0]))
                errors.Add("Первая буква должна быть большая");

            if (name.StartsWith(" ") || name.EndsWith(" ") || name.Contains("  "))
                errors.Add("Пробел может быть только между словами");

            if (errors.Count > 0)
            {
                string errorDetails = string.Join(", ", errors);
                return (false, $"Строка {i}: {errorDetails}, введено - '{name}'");
            }

            return (true, "Ошибок нет");
        }

        public static (bool IsValid, string ErrorMessage) ValidateEmail(string email, int i)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, $"Строка {i}: Имя не может быть пустым.");

            if (!Constants.EmailRegex.IsMatch(email))
                return (false, $"Строка {i}: Не корректный email, введено - '{email}'");

            return (true, "Ошибок нет");
        }

        public static (bool IsValid, string ErrorMessage) ValidateGender(string gender, int i)
        {
            if (gender == "Male" || gender == "Female")
                return (true, "Ошибок нет");

            return (false, $"Строка {i}: Допустимо указание Male или Femail, введено - '{gender}'");
        }

        public static (bool IsValid, string ErrorMessage) ValidateIpAddress(string ipAddress, int i)
        {
            if (System.Net.IPAddress.TryParse(ipAddress, out _))
                return (true, "Ошибок нет");

            return (false, $"Строка {i}: Не корректно введен IpAddress, введено - '{ipAddress}'");
        }

    }
}
