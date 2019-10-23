using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Contacts.Models.Entities;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Contacts.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class ContactDBContext : DbContext
	{
		/// <summary>
		/// The configuration for this Startup object.
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// ContactDBContext()
		/// </summary>
		public ContactDBContext()
		{			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="options"></param>
		/// <param name="configuration"></param>
		public ContactDBContext(DbContextOptions<ContactDBContext> options, IConfiguration configuration)
			: base(options)
		{

		}

		/// <summary>
		/// ClientData
		/// </summary>
		public DbSet<ContactData> ContactsData { get; set; }
		
		
	}
}
