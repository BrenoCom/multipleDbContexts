using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update;

namespace Persistence.contexts.otherSoftware;

public class MultitenancySqlServerMigrationsSqlGenerator(
    MigrationsSqlGeneratorDependencies dependencies,
    ICommandBatchPreparer commandBatchPreparer)
    : SqlServerMigrationsSqlGenerator(dependencies, commandBatchPreparer)
{

    public override IReadOnlyList<MigrationCommand> Generate(
        IReadOnlyList<MigrationOperation> operations,
        IModel? model = null,
        MigrationsSqlGenerationOptions options = MigrationsSqlGenerationOptions.Default)
    {
        return base.Generate(RewriteOperations(operations, model, options), model, options);
    }

    private IReadOnlyList<MigrationOperation> RewriteOperations(
        IReadOnlyList<MigrationOperation> migrationOperations,
        IModel? model,
        MigrationsSqlGenerationOptions options)
    {
        string? defaultSchema = null;

        bool IsDefaultSchema(string? schemaName)
        {
            return schemaName is null || schemaName == defaultSchema;
        }

        var schema = Dependencies.CurrentContext.Context.Model.GetDefaultSchema();

        if (schema is null || schema == defaultSchema)
            return migrationOperations;

        foreach (var operation in migrationOperations)
        {
            switch (operation)
            {
                case CreateTableOperation createTableOperation:
                    if (IsDefaultSchema(createTableOperation.Schema))
                    {
                        createTableOperation.Schema = schema;

                        foreach (var ck in createTableOperation.CheckConstraints.Where(ck => IsDefaultSchema(ck.Schema)))
                        {
                            ck.Schema = schema;
                        }

                        foreach (var uk in createTableOperation.UniqueConstraints.Where(uk => IsDefaultSchema(uk.Schema)))
                        {
                            uk.Schema = schema;
                        }

                        foreach (var fk in createTableOperation.ForeignKeys)
                        {
                            if (IsDefaultSchema(fk.Schema))
                            {
                                fk.Schema = schema;
                                fk.PrincipalSchema = schema;
                            }
                        }
                    }
                    break;
                case TableOperation tableOperation:
                    if (IsDefaultSchema(tableOperation.Schema))
                        tableOperation.Schema = schema;
                    break;
                case DropTableOperation dropTableOperation:
                    if (IsDefaultSchema(dropTableOperation.Schema))
                        dropTableOperation.Schema = schema;
                    break;
                case RenameTableOperation renameTableOperation:
                    if (IsDefaultSchema(renameTableOperation.Schema))
                        renameTableOperation.Schema = schema;
                    break;
                case ColumnOperation columnOperation:
                    if (IsDefaultSchema(columnOperation.Schema))
                        columnOperation.Schema = schema;
                    break;
                case CreateIndexOperation createIndexOperation:
                    if (IsDefaultSchema(createIndexOperation.Schema))
                        createIndexOperation.Schema = schema;
                    break;
                case DropIndexOperation dropIndexOperation:
                    if (IsDefaultSchema(dropIndexOperation.Schema))
                        dropIndexOperation.Schema = schema;
                    break;
                case EnsureSchemaOperation ensureSchemaOperation:
                    if (IsDefaultSchema(ensureSchemaOperation.Name))
                        ensureSchemaOperation.Name = schema;
                    break;
                default:
                    //TODO: Add more operations
                    break;
            }
        }

        return migrationOperations;
    }
}