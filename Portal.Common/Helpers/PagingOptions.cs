namespace Portal.Common.Helpers
{
    public class PagingOptions
    {
        private int _limit = 50;
        private int _offset;

        public int Limit
        {
            get => _limit;
            set => _limit = value < 50 && value > 0 ? value : 50;
        }

        public int Offset
        {
            get => _offset;
            set => _offset = value > 0 ? value : 0;
        }
    }
}
