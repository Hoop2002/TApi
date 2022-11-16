using System.ComponentModel.DataAnnotations.Schema;

namespace TApi
{
    public class UserDataVkDB
    {
        public int Id { get; set; }

        public long? Vk_Id { get; set; }

        public string? Name { get; set; }

        public string[]? NumOfIncoingLetters { get; set; }
    }
}
