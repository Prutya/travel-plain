namespace TravelPlain.Business.Interfaces
{
    public interface IService
    {
        int SaveChanges();
        void Log(string message);
    }
}
