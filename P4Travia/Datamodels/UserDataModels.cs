using System;
using System.Collections.Generic;

namespace P4Travia.Datamodels
{
    public class UserDataStorage  //constructors?
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Birthday { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
        public IList<string> Language { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string DownloadUrl { get; set; }

    }
}