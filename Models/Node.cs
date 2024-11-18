using System.Xml.Linq;

namespace PROG_POE.Models
{
    public class Node
    {
        public ServiceRequest data { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();
    }
}
