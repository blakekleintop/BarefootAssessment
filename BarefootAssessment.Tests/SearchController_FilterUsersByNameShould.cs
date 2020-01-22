using BarefootAssessment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Newtonsoft;
using Newtonsoft.Json;

namespace BarefootAssessment.Tests
{
    public class SearchController_FilterUsersByNameShould
    {
        protected readonly Controllers.SearchController _searchController;
        protected IEnumerable<Models.User> _users;

        public SearchController_FilterUsersByNameShould()
        {
            _searchController = new Controllers.SearchController();

            string json = "[{\"name\":\"Lovelace\"},{\"name\":\"Hamilton\"},{\"name\":\"Hopper\"},{\"name\":\"Torvalds\"},{\"name\":\"Gosling\"},{\"name\":\"Eich\"},{\"name\":\"Crockford\"},{\"name\":\"Dahl\"}]";
            _users = JsonConvert.DeserializeObject<IEnumerable<Models.User>>(json);

        }

        [Theory]
        [InlineData("e")]
        [InlineData("Lov")]
        public void FilterUsersByName_SearchResultsFound_ReturnUsers(string value)
        {
            // Arrange
            IEnumerable<Models.User> FoundUsers;

            // Act
            FoundUsers = _searchController.FilterUsersByName(value, _users);

            // Assert
            Assert.NotEmpty(FoundUsers);
        }

        [Theory]
        [InlineData("abcdefg")]
        [InlineData("Kleintop")]
        public void FilterUsersByName_SearchResultsNotFound_ReturnEmptyList(string value)
        {
            // Arrange
            IEnumerable<Models.User> FoundUsers;

            // Act
            FoundUsers = _searchController.FilterUsersByName(value, _users);

            // Assert
            Assert.Empty(FoundUsers);
        }

        [Theory]
        [InlineData("Love")]
        public void FilterUsersByName_SearchTermsLove_ReturnLoveLace(string value)
        {
            // Arrange
            IEnumerable<Models.User> FoundUsers;

            // Act
            FoundUsers = _searchController.FilterUsersByName(value, _users);

            // Assert
            Assert.True(FoundUsers.Count() == 1);
            Assert.True(FoundUsers.FirstOrDefault().Name.Equals("Lovelace"));
        }

        [Theory]
        [InlineData("Or")]
        public void FilterUsersByName_SearchTermsOr_ReturnTorvaldsCrockford(string value)
        {
            // Arrange
            IEnumerable<Models.User> FoundUsers;

            // Act
            FoundUsers = _searchController.FilterUsersByName(value, _users);

            //Assert
            Assert.True(FoundUsers.Count() == 2);
            Assert.True(FoundUsers.FirstOrDefault().Name == "Crockford");
            Assert.True(FoundUsers.Last().Name == "Torvalds");
        }

        [Theory]
        [InlineData("cRoCk")]
        public void FilterUsersByName_SearchTermsCaseInsensitive_ReturnIdentical(string value)
        {
            // Arrange
            IEnumerable<Models.User> InitialUsers;
            IEnumerable<Models.User> UpperUsers;
            IEnumerable<Models.User> LowerUsers;

            // Act
            InitialUsers = _searchController.FilterUsersByName(value, _users);
            UpperUsers = _searchController.FilterUsersByName(value.ToUpper(), _users);
            LowerUsers = _searchController.FilterUsersByName(value.ToLower(), _users);

            //Assert
            Assert.Equal(InitialUsers, UpperUsers);
            Assert.Equal(InitialUsers, LowerUsers);
            Assert.Equal(LowerUsers, UpperUsers);
        }
    }
}
