using System;
using System.IO;
using System.Reflection;

namespace MayLily.DataAccess.FluentMigrator
{
    public static class FluentMigratorClassGenerator
    {
        public static void Generate(MigrationSettings settings)
        {
            var schemaMigrator = new SchemaMigrator(settings);
            var migrationMetadata = schemaMigrator.GetMigrationMetadata();
            var template = new MigrationTemplate
            {
                Name = settings.Name,
                Description = settings.Description,
                Version = DateTime.UtcNow.ToTimestamp(),
                Namespace = Assembly.GetExecutingAssembly().GetName().Name,
                Metadata = schemaMigrator.GetMigrationMetadata()
            };

            var filename = Path.Combine(settings.BasePath, "{0}_{1}.cs".Fmt(template.Name, template.Version));
            File.WriteAllText(filename, template.TransformText());
        }
    }
}
