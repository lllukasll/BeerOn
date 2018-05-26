namespace BeerOn.Data.ModelsDto.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
    }
}