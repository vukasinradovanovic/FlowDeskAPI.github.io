using DataAccess.FlowDesk;          
using DataAccess.FlowDesk.Seeders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using DataAccess;

namespace FlowDeskAPI.Extentions
{
    /// <summary>
    /// Provides extension methods for managing database initialization routines via command line arguments.
    /// </summary>
    public static class DatabaseSeedingExtensions
    {
        /// <summary>
        /// Intercepts application startup arguments to execute a database seed operation if requested.
        /// </summary>
        /// <param name="app">The active running <see cref="WebApplication"/> instance context.</param>
        /// <param name="args">The raw string array of command-line arguments passed into the application entry point.</param>
        /// <returns>
        /// <c>true</c> if the seeding command flag was detected and executed (signaling that the application should stop execution); 
        /// otherwise, <c>false</c> if the application should continue booting into standard web server execution mode.
        /// </returns>
        /// <remarks>
        /// <para><b>Operational Flow:</b></para>
        /// <list type="bullet">
        /// <item>Checks the <paramref name="args"/> collection for the explicit presence of the <c>--seed</c> string flag.</item>
        /// <item>If found, spawns an isolated service lifetime scope to safely request the registered <see cref="FlowDbContext"/>.</item>
        /// <item>Dispatches execution down to the <see cref="MasterDatabaseSeeder.Execute"/> processing sequence.</item>
        /// <item>Intercepts thrown exceptions during processing and prints detailed diagnostic alerts directly to the terminal standard error engine streams.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// var app = builder.Build();
        /// if (app.RunCommandLineSeeders(args)) return;
        /// </code>
        /// </example>
        public static bool RunCommandLineSeeders(this WebApplication app, string[] args)
        {
            if (!args.Contains("--seed"))
            {
                return false; // Tells Program.cs to continue standard web runtime execution path
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Seed Engine] Starting initialization routine sequence...");
            Console.ResetColor();

            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<FlowDbContext>();

                    // Fire your Laravel-style sequential execution chain array loop
                    MasterDatabaseSeeder.Execute(context);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[Seed Engine] All data catalogs committed successfully!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ [Seed Engine] Critical processing error encountered: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"   Inner Details: {ex.InnerException.Message}");
                }
                Console.ResetColor();
            }

            return true; // Signals to Program.cs that processing concluded and it should exit out immediately
        }
    }
}