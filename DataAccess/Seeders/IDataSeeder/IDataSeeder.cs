using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.FlowDesk.Seeders
{
    /// <summary>
    /// Represents the non-generic core execution baseline marker for all database seeders.
    /// </summary>
    /// <remarks>
    /// This structural contract allows the <c>MasterDatabaseSeeder</c> to manage and execute 
    /// a sequentially ordered collection of mixed generic seeder implementations with complete type safety.
    /// </remarks>
    public interface ISeeder
    {
        /// <summary>
        /// Triggers the database data injection sequence.
        /// </summary>
        /// <param name="context">The active application database context instance.</param>
        void Seed(FlowDbContext context);
    }

    /// <summary>
    /// Defines the contract for an automated, self-checking database data seeder module.
    /// </summary>
    /// <remarks>
    /// This system is modeled after the Laravel migration seeder architecture. It uses C# Default Interface 
    /// Methods to abstract away repetitive database checks, keeping implementing modules focused entirely on data.
    /// </remarks>
    /// <typeparam name="T">The type of domain entity class this seeder manages </typeparam>
    public interface IDataSeeder<T> : ISeeder where T : BaseEntity
    {
        /// <summary>
        /// Provides the collection of lookup data models to be pushed to the database.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> catalog containing the initial database records.</returns>
        /// <example>
        /// <code>
        /// public IEnumerable&lt;Status&gt; GetSeedData()
        /// {
        ///     return new List&lt;Status&gt; { new Status { Id = 1, Name = "To Do" } };
        /// }
        /// </code>
        /// </example>
        IEnumerable<T> GetSeedData();

        /// <summary>
        /// Executes the centralized data entry routine.
        /// </summary>
        /// <param name="context">The active application database context instance.</param>
        /// <remarks>
        /// <para><b>Execution Rules Flow:</b></para>
        /// <list type="bullet">
        /// <item>Scans the corresponding <see cref="Microsoft.EntityFrameworkCore.DbSet{T}"/> using <c>Any()</c>.</item>
        /// <item>If data already exists, the engine logs a skip and <b>exits immediately</b> to prevent duplicate keys.</item>
        /// <item>If empty, calls <see cref="GetSeedData"/>, registers the records, and commits the transaction via <c>SaveChanges()</c>.</item>
        /// </list>
        /// </remarks>
        void ISeeder.Seed(FlowDbContext context)
        {
            // Centralized Guard for checking if the table has data before seeding
            if (context.Set<T>().Any())
            {
                return;
            }

            var dataList = GetSeedData().ToList();

            // Logic for auto-generating IDs for entities that have an ID of 0
            for (int i = 0; i < dataList.Count; i++)
            {
                if (dataList[i].Id == 0)
                {
                    dataList[i].Id = i + 1;
                }
            }

            var entityType = context.Model.FindEntityType(typeof(T));
            var tableName = entityType?.GetTableName();
            var schemaName = entityType?.GetSchema() ?? "dbo";

            // Begin a transaction to ensure atomicity of the seeding operation
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT [{schemaName}].[{tableName}] ON;");

                    context.Set<T>().AddRange(dataList);
                    context.SaveChanges();

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT [{schemaName}].[{tableName}] OFF;");

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}