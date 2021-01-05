namespace Grundfos.TW.SQL
{
    public class PageCounter
    {
        public int GetPageCount(int recordCount, int pageSize)
        {
            int pages = recordCount / pageSize;
            if (pages * pageSize < recordCount)
            {
                pages++;
            }

            return pages;
        }
    }
}
