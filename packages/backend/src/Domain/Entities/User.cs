using System.Collections.Generic;

namespace Domain.Entities
{
    public class User
    {
        public User()
        {
            ManagementJobs = new List<ManagementJob>();
        }

        public string Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public ICollection<ManagementJob> ManagementJobs { get; set; }
    }
}