using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROG_POE.Models
{
    public class ServiceRequestTree
    {
        // root node for tree
        public Node root;

        // gets all requests and puts it in a list
        public List<Node> GetList()
        {
            var list = new List<Node>();
            MakeListOfRequests(root, list);
            return list;
        }

        // gets all found requests and puts it in a list
        public List<Node> FoundList(RequestSearch search)
        {
            var list = new List<Node>();

            // calls method to get found nodes
            FindRequest(root, list, search);

            // further filters it so that only data that matches the exact search is added
            if (search.id != null)
            {
                list = list.Where(x=> x.data.RequestId.ToString() == search.id).ToList();
            }
            if (search.date != null)
            {
                list = list.Where(x => x.data.SubmissionDate == search.date).ToList();
            }
            if (search.status != null)
            {
                list = list.Where(x => x.data.Status == search.status).ToList();
            }
            if (search.priority != null)
            {
                list = list.Where(x => x.data.Priority.ToString() == search.priority).ToList();
            }

            // returns list
            return list;
        }

        // recursively makes list of requests
        public void MakeListOfRequests(Node node, List<Node> list, string indent = "", bool last = true)
        {
            if (node == null) return;

            for (int i = 0; i < node.Children.Count; i++)
            {
                list.Add(node.Children[i]);
                MakeListOfRequests(node.Children[i], list, indent, i == node.Children.Count - 1);
            }
        }

        // recursively makes list of requests based on user searches.
        public void FindRequest(Node node, List<Node> list, RequestSearch search, string indent = "", bool last = true)
        {
            if (node == null) return;

            if (search.id != null)
            {
                if(search.id == node.data.RequestId.ToString())
                {
                    list.Add(node);
                }
            }
            if (search.date != null)
            {
                if (search.date == node.data.SubmissionDate)
                {
                    list.Add(node);
                }
            }
            if (search.status != null)
            {
                if (search.status == node.data.Status)
                {
                    list.Add(node);
                }
            }
            if (search.priority != null)
            {
                if (search.priority == node.data.Priority.ToString())
                {
                    list.Add(node);
                }
            }

            for (int i = 0; i < node.Children.Count; i++)
            {
                FindRequest(node.Children[i], list, search, indent, i == node.Children.Count - 1);
            }
        }
    }
}
