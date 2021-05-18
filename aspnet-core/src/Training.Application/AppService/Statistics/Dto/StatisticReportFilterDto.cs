using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Paging;

namespace Training.AppService.Statistics.Dto
{
    public class StatisticReportFilterDto: PagingRequestDto
    {
        public Guid LibraryId { get; set; }
        public int? Month { get; set; }
    }
}
