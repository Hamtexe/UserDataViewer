using System.ComponentModel;

namespace UserDataViewerCore
{
    public class DataViewer
    {
        public static (List<User> currentUsers, string? ErrorSort) SortUser(string columnName, ListSortDirection direction, List<User> currentUsers)
        {
            var _currentUsers = currentUsers;

            switch (columnName)
            {
                case Constants.Id:
                    _currentUsers = direction == ListSortDirection.Ascending
                        ? _currentUsers.OrderBy(u => u.Id).ToList()
                        : _currentUsers.OrderByDescending(u => u.Id).ToList();
                    break;
                case Constants.FirstName:
                    _currentUsers = direction == ListSortDirection.Ascending
                        ? _currentUsers.OrderBy(u => u.FirstName).ToList()
                        : _currentUsers.OrderByDescending(u => u.FirstName).ToList();
                    break;
                case Constants.LastName:
                    _currentUsers = direction == ListSortDirection.Ascending
                        ? _currentUsers.OrderBy(u => u.LastName).ToList()
                        : _currentUsers.OrderByDescending(u => u.LastName).ToList();
                    break;
                case Constants.Email:
                    _currentUsers = direction == ListSortDirection.Ascending
                        ? _currentUsers.OrderBy(u => u.Email).ToList()
                        : _currentUsers.OrderByDescending(u => u.Email).ToList();
                    break;
                case Constants.Gender:
                    _currentUsers = direction == ListSortDirection.Ascending
                        ? _currentUsers.OrderBy(u => u.Gender).ToList()
                        : _currentUsers.OrderByDescending(u => u.Gender).ToList();
                    break;
                case Constants.IpAddress:
                    _currentUsers = direction == ListSortDirection.Ascending
                        ? _currentUsers.OrderBy(u => u.IpAddress).ToList()
                        : _currentUsers.OrderByDescending(u => u.IpAddress).ToList();
                    break;
                default:
                    return (_currentUsers, "Такого столбца не существует");
                    break;
            }

            return (_currentUsers, null);;
        }

        public static List<User> SerchUser(string searchText, List<User> currentUsers, List<User> allUsers)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return allUsers;
            }

            var searchLower = searchText.ToLower();

            return allUsers.Where(u =>
                    u.FirstName.ToLower().Contains(searchLower) ||
                    u.LastName.ToLower().Contains(searchLower) ||
                    u.Email.ToLower().Contains(searchLower) ||
                    u.Gender.ToLower().Contains(searchLower) ||
                    u.IpAddress.ToLower().Contains(searchLower))
                .ToList();
        }
    }
}