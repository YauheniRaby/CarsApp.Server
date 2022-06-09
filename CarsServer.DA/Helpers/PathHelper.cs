namespace CarsServer.DA.Helpers
{
    public static class PathHelper
    {
        public static string CombineForJson(string basePath, string fileName)
        {
            return $"{basePath}{fileName}.json";
        }
    }
}
