using Microsoft.AspNetCore.Identity;

namespace PennStateSoft.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public byte[]? ProfilePic { get; set; }
        public List<Notification>? Notifications { get; set; }
        public List<Complaint>? Complaints { get; set; }
        public List<Meeting>? Meetings { get; set; }
    }

}
