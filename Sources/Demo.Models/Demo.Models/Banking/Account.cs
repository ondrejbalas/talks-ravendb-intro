using System;

namespace Demo.Models.Banking
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public Guid? Etag { get; set; }
    }
}
