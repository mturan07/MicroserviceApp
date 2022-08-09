namespace FlowardApp.Services.CatalogService.Settings
{
    public interface IDatabaseSettings
    {
        public string ProductCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
