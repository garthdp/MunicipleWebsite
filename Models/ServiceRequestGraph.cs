namespace PROG_POE.Models
{
    public class ServiceRequestGraph
    {
        // creates dictionary of requests
        public Dictionary<int, GraphNode<Node>> requestList = new Dictionary<int, GraphNode<Node>>();

        //  adds request to dictionary
        public void AddRequest(int requestId, Node request)
        {
            requestList.Add(requestId, new GraphNode<Node>(request, requestId));
        }

        // gets node based on id from dictionary
        public GraphNode<Node> getRequest(int id)
        {
            return requestList[id];
        }

        // adds an edge between different nodes
        public void addEdges(int source, int destination)
        {
            GraphNode<Node> s = getRequest(source);
            GraphNode<Node> d = getRequest(destination);
            s.adjacent.AddFirst(d);
        }

        // gets dependancies for node based on request id
        public List<Node> GetDependencies(int requestId)
        {
            if (!requestList.ContainsKey(requestId))
            {
                return new List<Node>();
            }

            GraphNode<Node> requestNode = requestList[requestId];

            var dependencies = new List<Node>();
            foreach (var adjacentNode in requestNode.adjacent)
            {
                dependencies.Add(adjacentNode.Data);
            }

            return dependencies;
        }
    }

}
