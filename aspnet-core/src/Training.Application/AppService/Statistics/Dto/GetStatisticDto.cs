using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Statistics.Dto
{
    public class GetStatisticDto
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string CategoryName { get; set; }
        public DateTime DateBorrow { get; set; }
        public string AuthorName { get; set; }
        public int Quantity { get; set; }

    }
}
