﻿namespace BeerOn.Data.DbModels
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Roles { get; set; }
    }
}
