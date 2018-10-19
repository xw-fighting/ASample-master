using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRapid.Web.VisitAnalyse.Storage.SqlServer
{
    [Table("HttpVisitInfo")]
    public class SqlHttpVisitInfoTable
    {
        [Key]
        public Guid Id { get; set; }

        public Guid HttpVisitId { get; set; }

        [MaxLength(100)]
        public string Key { get; set; }

        [MaxLength]
        public string Value { get; set; }

        public DateTime Time { get; set; }
    }
}