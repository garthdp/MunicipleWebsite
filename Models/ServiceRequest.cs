using System.Security.Policy;

namespace PROG_POE.Models
{
    public class ServiceRequest
    {
        public int RequestId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int Priority { get; set; }
    }
}
