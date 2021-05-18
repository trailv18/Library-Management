using System;

namespace Training.AppService.BookLibraries.Dto
{
    public class GetAllBookLibraryDto
    {
        public Guid Id { get; set; }
        public string Book { get; set; }
        public string Library { get; set; }
        public int Stock { get; set; }
    }
}
