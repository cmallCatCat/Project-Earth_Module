namespace Core.Save_And_Load
{
    public interface ISave
    {
        object Save();
        void Load(object save);
    }
}