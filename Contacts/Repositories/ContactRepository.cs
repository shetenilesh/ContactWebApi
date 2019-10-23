using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Contacts.Interfaces;
using Contacts.Models;
using Contacts.Models.Api;
using Contacts.Models.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Contacts.Repositories
{
	/// <summary>
	/// ContactRepository
	/// </summary>
	public class ContactRepository : IContactRepository
	{
		#region Injected Members
		private readonly ContactDBContext _dBContext;
		private readonly IMapper _mapper;
		#endregion

		/// <summary>
		/// Parameterized Constructor
		/// </summary>       
		/// <param name="dBContext"></param>
		/// <param name="mapper"></param>
		public ContactRepository(ContactDBContext dBContext, IMapper mapper)
		{
			_dBContext = dBContext;
			_mapper = mapper;
		}

		/// <summary>
		/// The method will return a list of contacts
		/// </summary>
		/// <returns></returns>
		public IList<Contact> GetAllContacts()
		{
			List<Contact> contacts = null;
			contacts = new List<Contact>();

			var tmpcontacts =
								_dBContext.ContactsData
								.ToList();

			foreach (var contact in tmpcontacts)
			{
				contacts.Add(new Contact()
				{
					ContactId = contact.ContactId,
					FirstName = contact.FirstName,
					LastName = contact.FirstName,
					Email = contact.Email,
					PhoneNumber = contact.PhoneNumber,
					Status = contact.Status,
				});
			}

			return contacts;
		}


		/// <summary>
		/// method will return a specific contact based on contact id
		/// </summary>
		/// <returns></returns>
		public ContactData GetContact(int contactId)
		{
			ContactData contact = null;
			using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
			{
				contact = _dBContext.ContactsData
								.Where(x => x.ContactId == contactId)
								.FirstOrDefault();
			}
			return contact;
		}

		/// <summary>
		/// Create Contact.
		/// </summary>
		/// <param name="contact"></param>
		/// <returns>Contact</returns>
		public Contact CreateContact(Contact contact)
		{
			//Map request object to DB Model
			ContactData contactModel = _mapper.Map<ContactData>(contact);

			//Save data to DB
			using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
			{
				_dBContext.ContactsData.Add(contactModel);
				_dBContext.SaveChanges();

				transactionScope.Complete();
			}

			return contact;
		}

		/// <summary>
		/// Edit Contact
		/// </summary>
		/// <param name="updcontact"></param>
		/// <param name="newcontact"></param>
		/// <returns>Contact</returns>
		public Contact EditContact(ContactData updcontact, Contact newcontact)
		{
			using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
			{
				Contact exContact = null;
				if (newcontact == null || updcontact == null)
				{
					return exContact;
				}

				//Update if record found
				updcontact.FirstName = newcontact.FirstName;
				updcontact.LastName = newcontact.LastName;
				updcontact.Email = newcontact.Email;
				updcontact.PhoneNumber = newcontact.PhoneNumber;
				updcontact.Status = newcontact.Status;

				//Map to DB object 
				_dBContext.ContactsData.Update(updcontact);
				//Save
				_dBContext.SaveChanges();

				transactionScope.Complete();
			}

			return newcontact;
		}


		/// <summary>
		/// Delete Contact
		/// </summary>
		/// <param name="contact"></param>
		/// <returns>Contact</returns>
		public Contact DeleteContact(ContactData contact)
		{
			Contact deletecontact = null;
			using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
			{

				//Delete
				_dBContext.ContactsData.Remove(contact);
				_dBContext.SaveChanges();

				transactionScope.Complete();
			}

			return deletecontact;
		}
	}
}
