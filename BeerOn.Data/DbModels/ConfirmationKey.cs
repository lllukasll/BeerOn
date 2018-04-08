using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOn.Data.DbModels
{
    public class ConfirmationKey : BaseEntity
    {
        public bool Revoked { get; set; }
        public string Key { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
