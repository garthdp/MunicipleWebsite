namespace PROG_POE.Models
{
    public class GraphNode<T>
    {
        public int ID;
        public T Data;
        public LinkedList<GraphNode<T>> adjacent = new LinkedList<GraphNode<T>>();
        public GraphNode(T data, int ID)
        {
            this.ID = ID;
            this.Data = data;
        }
    }
}
