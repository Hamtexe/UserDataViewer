using System.ComponentModel;
using UserDataViewerCore; 

namespace UserDataViewerTests;

    public class DataViewerTests
    {
        private readonly List<User> _testUsers = new List<User>
        {
            new User { Id = 3, FirstName = "John", LastName = "Doe", Email = "john@example.com", Gender = "Male", IpAddress = "192.168.1.1" },
            new User { Id = 1, FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", Gender = "Female", IpAddress = "192.168.1.3" },
            new User { Id = 2, FirstName = "Bob", LastName = "Johnson", Email = "bob@example.com", Gender = "Male", IpAddress = "192.168.1.2" }
        };

        [Fact]
        public void SortUser_ByIdAscending_CorrectOrder()
        {
            var result = DataViewer.SortUser("Id", ListSortDirection.Ascending, _testUsers);
            
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
            Assert.Equal(3, result[2].Id);
        }

        [Fact]
        public void SortUser_ByIdDescending_CorrectOrder()
        {
            var result = DataViewer.SortUser("Id", ListSortDirection.Descending, _testUsers);
            
            Assert.Equal(3, result[0].Id);
            Assert.Equal(2, result[1].Id);
            Assert.Equal(1, result[2].Id);
        }

        [Fact]
        public void SortUser_ByFirstNameAscending_CorrectOrder()
        {
            var result = DataViewer.SortUser("FirstName", ListSortDirection.Ascending, _testUsers);
            
            Assert.Equal("Alice", result[0].FirstName);
            Assert.Equal("Bob", result[1].FirstName);
            Assert.Equal("John", result[2].FirstName);
        }

        [Fact]
        public void SortUser_ByLastNameDescending_CorrectOrder()
        {
            var result = DataViewer.SortUser("LastName", ListSortDirection.Descending, _testUsers);

            Assert.Equal("Smith", result[0].LastName);
            Assert.Equal("Johnson", result[1].LastName);
            Assert.Equal("Doe", result[2].LastName);
        }

        [Fact]
        public void SerchUser_EmptySearchText_ReturnsAllUsers()
        {
            var result = DataViewer.SerchUser("", _testUsers, _testUsers);
            
            Assert.Equal(_testUsers.Count, result.Count);
            Assert.Equal(_testUsers, result);
        }

        [Fact]
        public void SerchUser_ByFirstName_FindsCorrectUsers()
        {
            var result = DataViewer.SerchUser("Alice", _testUsers, _testUsers);
            
            Assert.Single(result);
            Assert.Equal("Alice", result[0].FirstName);
        }

        [Fact]
        public void SerchUser_ByPartialLastName_FindsCorrectUsers()
        {
            var result = DataViewer.SerchUser("Joh", _testUsers, _testUsers);
            
            Assert.Equal(2, result.Count);
            Assert.Contains(result, u => u.LastName == "Johnson");
            Assert.Contains(result, u => u.LastName == "Doe");
        }
    }

