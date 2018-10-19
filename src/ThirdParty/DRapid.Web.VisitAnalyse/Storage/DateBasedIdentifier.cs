using System;
using DRapid.Web.VisitAnalyse.Core;

namespace DRapid.Web.VisitAnalyse.Storage
{
    public class DateBasedIdentifier<T> : IIdentifier
    {
        public DateBasedIdentifier(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; }

        public static DateBasedIdentifier<T> Build(DateTime date)
        {
            return new DateBasedIdentifier<T>(date);
        }

        public string GetStringIdentifier()
        {
            var identifier = typeof(T).BuildUniqueIdentifier();
            return $"[{identifier.DisplayName}_{identifier.Hash}]({Date.ToString(DateTimeParts.Day)})";
        }
    }
}