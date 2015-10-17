namespace Chemicals.ExcelImporter.Contracts
{
    public interface IZipExtractor
    {
        void Extract(string sourcePath, string destinationPath);
    }
}
