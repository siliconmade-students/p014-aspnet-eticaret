namespace Eticaret.Business.Dtos
{
    public class SearchDto
    {
        public int? CategoryId { get; set; }
        public string Query { get; set; }

        public bool? IsPopular { get; set; }
        public bool? IsNewArrival { get; set; }
    }
}
