﻿namespace ProductInventoryApp.Models
{
    public class AuthResult
    {
        public string Token { get; set; }

        public bool Result { get; set; }
        public List<string> Error { get; set; }
    }
}
