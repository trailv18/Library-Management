using System;

namespace Training.AppService.Statistics.Dto
{
    public class GetAllStatisticByDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid LibraryId { get; set; }
        public string LibraryName { get; set; }
        //public string BookName { get; set; }
        public Guid DistrictId { get; set; }
        public string DistrictName { get; set; }
        public Guid ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int Quantity { get; set; }
        //public DateTime DateBorrow { get; set; }
    }
}
