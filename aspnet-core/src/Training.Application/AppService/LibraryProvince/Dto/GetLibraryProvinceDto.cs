using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.LibraryProvince.Dto
{
    public class GetLibraryProvinceDto
    {
        public Guid ProvinceId { get; set; }
        public Guid DistrictId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public int QuantityLibrary { get; set; }
        public int QuantityUser { get; set; }
        public int TotalBorrow { get; set; }
    }
}
