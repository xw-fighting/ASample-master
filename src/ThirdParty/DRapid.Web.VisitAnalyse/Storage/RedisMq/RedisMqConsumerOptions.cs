namespace DRapid.Web.VisitAnalyse.Storage.RedisMq
{
    public class RedisMqConsumerOptions
    {
        public string ConnectionString { get; set; }

        public int BulcketLength { get; set; } = 10;
    }
}
