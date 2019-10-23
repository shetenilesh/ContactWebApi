using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Contacts
{
	/// <summary>
	/// Main program.
	/// </summary>
	public class Program
    {
		/// <summary>
		/// Main entry point.
		/// </summary>
		/// <param name="args">The command line arguments.</param>
		/// <returns>
		/// Exit code 0 for success, not 0 for failure.
		/// </returns>
		public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

		/// <summary>
		/// Creates and builds the web host.
		/// </summary>
		/// <param name="args">The original command line arguments.</param>
		/// <returns>The web host.</returns>
		public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
