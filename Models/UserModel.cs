﻿using Newtonsoft.Json;

namespace STE.Models
{
    public class UserModel
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Age { get; set; }
    }

}
