using System;
using Training.Entity.Paging;

namespace Training.AppService.Statistics.Dto
{
    public class StatisticReportFilterByDto : PagingRequestDto
    {
        public Guid LibraryId { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid DistrictId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Month { get; set; }
        public int? Quarter { get; set; }
    }
}
