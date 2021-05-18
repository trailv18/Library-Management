using System.ComponentModel.DataAnnotations;

namespace Training.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}