using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Models.Entities
{
	/// <summary>
	/// ContactModel
	/// </summary>
	[Table("Contact")]
	public class ContactData
	{
		/// <summary>
		/// Contact ID
		/// </summary>
		[Key]
		public int ContactId { get; set; }

		/// <summary>
		/// First name
		/// </summary>
		[Column("FirstName", TypeName = "Nvarchar(20)")]
		public string FirstName { get; set; }

		/// <summary>
		/// Last name
		/// </summary>
		[Column("LastName", TypeName = "Nvarchar(20)")]
		public string LastName { get; set; }


		/// <summary>
		/// Email
		/// </summary>
		[Column("Email", TypeName = "Nvarchar(20)")]
		public string Email { get; set; }


		/// <summary>
		/// Phone Number
		/// </summary>
		[Column("PhoneNumber", TypeName = "Nvarchar(20)")]
		public string PhoneNumber { get; set; }


		/// <summary>
		/// Status if contact is active/inactive
		/// </summary>
		[Column("Status")]
		public bool Status { get; set; }
		
	}
}
