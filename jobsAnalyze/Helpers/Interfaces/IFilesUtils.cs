namespace jobsAnalyze.Helpers.Interfaces
{
    public interface IFilesUtils
    {
        public MemoryStream CreateCSV<T>(List<T> items);
    }
}
