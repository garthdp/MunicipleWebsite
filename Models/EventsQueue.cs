namespace PROG_POE.Models
{
    public class EventsQueue
    {
        public MunicipalityEvent[] pr = new MunicipalityEvent[100000];
        int size = -1;
        public void enqueue(MunicipalityEvent eventItem)
        {
            size++;
            pr[size] = eventItem;
        }
        public int peek()
        {
            DateTime earliestDate = DateTime.MaxValue;
            int iPos = -1;

            for (int i = 0; i <= size; i++)
            {
                if (pr[i].EventDateTime < earliestDate)
                {
                    earliestDate = pr[i].EventDateTime;
                    iPos = i;
                }
            }
            return iPos;
        }
        public MunicipalityEvent dequeue()
        {
            int iPos = peek();
            if (iPos == -1) return null;

            MunicipalityEvent eventToReturn = pr[iPos];

            for (int i = iPos; i < size; i++)
            {
                pr[i] = pr[i + 1];
            }
            size--;
            return eventToReturn;
        }

    }
}
