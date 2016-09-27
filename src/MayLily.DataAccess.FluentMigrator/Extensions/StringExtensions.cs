namespace MayLily.DataAccess.FluentMigrator
{
    public static class StringExtensions
    {
        public static string Fmt(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
