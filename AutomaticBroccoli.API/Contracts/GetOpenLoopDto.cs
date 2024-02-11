namespace AutomaticBroccoli.API.Contracts
{
    public class GetOpenLoopDto
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string UserLogin { get; set; }
    }
}
