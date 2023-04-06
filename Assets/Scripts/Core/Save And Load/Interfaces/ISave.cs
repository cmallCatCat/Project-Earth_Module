namespace Core.Save_And_Load.Interfaces
{
    public interface ISave
    {
        object Save();
        void Load(object save);
    }
}