using DRapid.Web.VisitAnalyse.Core;

namespace DRapid.Web.VisitAnalyse.Storage
{
    public class TextBasedIdentifier<T> : IIdentifier
    {
        public TextBasedIdentifier(string text)
        {
            Text = text;
        }

        public string Text { get; }

        public string GetStringIdentifier()
        {
            var identifier = typeof(T).BuildUniqueIdentifier();
            return $"[{identifier.DisplayName}_{identifier.Hash}]({Text})";
        }

        public static TextBasedIdentifier<T> Build(string text)
        {
            return new TextBasedIdentifier<T>(text);
        }
    }
}