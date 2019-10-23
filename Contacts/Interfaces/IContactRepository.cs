using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Contacts.Models.Api;
using Contacts.Models.Entities;

namespace Contacts.Interfaces
{
	/// <summary>
	/// ContactRepository
	/// </summary>
	public interface IContactRepository
	{
        /// <summary>
        /// The method will return a list of contacts
        /// </summary>
        /// <returns>Contact</returns>        
        IList<Contact> GetAllContacts();

		/// <summary>
		/// The method will return a specific contact based on contact id
		/// </summary>
		/// <param name="contactId"></param>
		/// <returns>Contact</returns>        
		ContactData GetContact(int contactId);

		/// <summary>
		/// CreateContact.
		/// </summary>
		/// <param name="contact"></param>
		/// <returns>Contact</returns>
		Contact CreateContact(Contact contact);


		/// <summary>
		/// EditContact.
		/// </summary>
		/// <param name="updContact"></param>
		/// <param name="newContact"></param>
		/// <returns>Contact</returns>
		Contact EditContact(ContactData updContact, Contact newContact);

		/// <summary>        
		/// Delete contact.
		/// </summary> 
		/// <param name="Contact"></param>
		/// <returns>Contact</returns>
		Contact DeleteContact(ContactData Contact);

    }
}
