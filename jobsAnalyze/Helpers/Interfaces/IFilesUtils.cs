namespace jobsAnalyze.Helpers.Interfaces
{
    public interface IFilesUtils
    {
        public MemoryStream CreateFile<T>(List<T> items);
    }
}
