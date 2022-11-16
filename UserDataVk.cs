using Microsoft.Extensions.Primitives;

namespace TApi
{
    //Модель данных пользователя вк
    public class UserDataVk
    {
        public long Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? TextOfTheLastFivePosts { get; set; }

        public bool RequestSuccess { get; set; }
        
        public string? UserExistence { get; set; }

        public string? NumOfIncoingLetters { get; set; }
    }
}
