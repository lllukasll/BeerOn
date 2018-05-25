namespace BeerOn.Data.ModelsDto.User
{
    public class GetUserDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
    }
}
