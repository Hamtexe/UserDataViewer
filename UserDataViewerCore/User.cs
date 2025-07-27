namespace UserDataViewerCore
{
  public class User
    {
        public int? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;


        public User() { }
        
        public User(int id, string firstName, string lastName, 
                  string email, string gender, string ipAddress)
        {
            
            var resultId = ValidationHelper.ValidateName(id.ToString(), 0);
            if (!resultId.IsValid)
            {
                throw new ArgumentException("Не корректное ID!");
            }
            else
            {
                Id = id;
            }
            
            var resultFirstName = ValidationHelper.ValidateName(firstName, 0);
            if (!resultFirstName.IsValid)
            {
                throw new ArgumentException("Не корректное имя!");
            }
            else
            {
                FirstName = firstName;
            }
            
            var resultLastName = ValidationHelper.ValidateName(lastName, 0);
            if (!resultLastName.IsValid)
            {
                throw new ArgumentException("Не корректная фамилия");
            }
            else
            {
                LastName = lastName;
            }
            
            var resultEmail = ValidationHelper.ValidateName(email, 0);
            if (!resultEmail.IsValid)
            {
                throw new ArgumentException("Не корректный email");
            }
            else
            {
                Email = email;
            }

            var resultGender = ValidationHelper.ValidateName(gender, 0);
            if (!resultGender.IsValid)
            {
                throw new ArgumentException("Не корректный пол");
            }
            else
            {
                Gender = gender;
            }

            var resultIpAddress = ValidationHelper.ValidateName(ipAddress, 0);
            if (!resultIpAddress.IsValid)
            {
                throw new ArgumentException("Не корректный IpAddress");
            }
            else
            {
                IpAddress = ipAddress;
            }
            
        }
        
        public static (List<User> allUsers, List<string> validationErrors) LoadUsersFromFile(string filePath)
        {
            var allUsers = new List<User>();
            var validationErrors = new List<string>();

            try
            {
                var lines = File.ReadAllLines(filePath);
                
                lines = CharacterReplacer.ReplaceRussianWithEnglish(lines);

                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(new[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length < 6)
                    {
                        validationErrors.Add($"Строка {i + 1}: Не хватает данных (ожидается 6 полей).)");
                        continue;
                    }

                    var idValidateResult = ValidationHelper.ValidateId(parts[0], i);

                    if (!idValidateResult.IsValid || idValidateResult.id == null)
                    {
                        validationErrors.Add(idValidateResult.ErrorMessage);
                        continue;
                    }
                    
                    var firtsNameValidateResult = ValidationHelper.ValidateName(parts[1], i);

                    if (!firtsNameValidateResult.IsValid)
                    {
                        validationErrors.Add(firtsNameValidateResult.ErrorMessage);
                        continue;
                    }
                    
                    var lastNameValidateResult = ValidationHelper.ValidateName(parts[2], i);

                    if (!lastNameValidateResult.IsValid)
                    {
                        validationErrors.Add(lastNameValidateResult.ErrorMessage);
                        continue;
                    }

                    var emailValidateResult = ValidationHelper.ValidateEmail(parts[3], i);

                    if (!emailValidateResult.IsValid)
                    {
                        validationErrors.Add(emailValidateResult.ErrorMessage);
                        continue;
                    }

                    var genderValidateResult = ValidationHelper.ValidateGender(parts[4], i);

                    if (!genderValidateResult.IsValid)
                    {
                        validationErrors.Add(genderValidateResult.ErrorMessage);
                        continue;
                    }

                    var ipAddressValidateResult = ValidationHelper.ValidateIpAddress(parts[5], i);

                    if (!ipAddressValidateResult.IsValid)
                    {
                        validationErrors.Add(ipAddressValidateResult.ErrorMessage);
                        continue;
                    }

                    allUsers.Add(new User
                    {
                        Id = idValidateResult.id,
                        FirstName = parts[1].Trim(),
                        LastName = parts[2].Trim(),
                        Email = parts[3].Trim(),
                        Gender = parts[4].Trim(),
                        IpAddress = parts[5].Trim()
                    });
                }

                return (allUsers, validationErrors);
            }
            catch (Exception ex)
            {
                validationErrors.Add($"Ошибка чтения файла: {ex.Message}");
                return (allUsers, validationErrors);
            }
        }
    }
}