using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Models.Api
{
    /// <summary>
    /// The Contact class
    /// </summary>
    public class Contact
    {
		/// <summary>
		/// Unique identifier 
		/// </summary>
		
		public int ContactId { get; set; }

		/// <summary>
		/// First Name
		/// </summary>
		[Required]
		[MaxLength(20)]
		public string FirstName { get; set; }

		/// <summary>
		/// Last Name
		/// </summary>
		[Required]
		[MaxLength(20)]
		public string LastName { get; set; }

		/// <summary>
		/// Email
		/// </summary>
		[Required]
		[MaxLength(20)]
		[RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		/// <summary>
		/// Phone Number
		/// </summary>
		[Required]
		[MaxLength(12)]
		[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number")]
		public string PhoneNumber { get; set; }

        /// <summary>
        ///Status
        /// </summary>
        public bool Status { get; set; }
		
    }
}
