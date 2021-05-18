using Training.Entity.Paging;

namespace Training.AppService.Publishers.Dto
{
    public class PublisherFilterDto: PagingRequestDto
    {
        public string PublisherName { get; set; }
    }
}