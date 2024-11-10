

namespace EndPoint.Admin.Utilities
{
    public interface ICurrentUserV2
    {
        public ActiveUser Get();
        public void Set(ActiveUser activeUser);
        public void Clear();
        public void Reset();
        public bool Fill(string userName);

    }
}
