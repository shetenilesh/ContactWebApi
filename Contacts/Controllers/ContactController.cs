using Contacts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Contacts.Models;
using Contacts.Models.Api;
using Contacts.Models.Entities;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace Contacts.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
    public class ContactController : Controller
    {


		#region Injected Members
		private readonly ILogger<ContactController> _logger;
		private readonly IContactRepository _contactRepository;
		
		#endregion

		/// <summary>
		/// ContactController Parameterized Constructor
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="contactRepository"></param>
		public ContactController(ILogger<ContactController> logger, IContactRepository contactRepository)
		{
			_logger = logger;
			_contactRepository = contactRepository;
			
		}

		/// <summary>        
		/// This API allows us to Get all Contacts
		/// </summary>
		/// <returns>Contact</returns>
		/// <remarks>
		/// Sample request: 
		/// 
		///     GET api/Contact
		/// </remarks>
		/// <response code="200">The target resource was successfully retrieved.</response>
		/// <response code="400">The request was not valid.</response>
		/// <response code="404">The target resource does not exist.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[HttpGet]
        public IActionResult Get()
        {
			if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation($"Getting all Contacts.");
			IList<Contact> contactResponses = new List<Contact>();
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				contactResponses = _contactRepository.GetAllContacts();

				if (contactResponses == null || contactResponses.Count == 0)
				{
					return NotFound();
				}
				return Ok(contactResponses);
			}
			catch (Exception ex)
			{
				if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation($"Error in Getting all Contacts." + ex.Message);
				return NotFound();
			}

		}


		/// <summary>
		/// Create Contact
		/// </summary>
		/// <param name="contact"></param>
		/// <returns></returns>
		/// <remarks>
		/// Sample request: 
		/// 
		///     POST api/Contact
		///     {
		///			"firstName": "abc",
		///			"lastName": "xyz",
		///			"email": "abc@abc.com",
		///			"phoneNumber": 9178988989,
		///			"status": true
		///     }
		/// </remarks>
		/// <response code="201">The target resource was successfully created</response>
		/// <response code="400">The request was not valid.</response>
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[HttpPost]
		public IActionResult Post(Contact contact)
        {
			if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation($"Creating Contact.");
			
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				if (_contactRepository.GetContact(contact.ContactId) != null)
				{
					return BadRequest(ModelState);
				}

				_contactRepository.CreateContact(contact);
				return CreatedAtAction(nameof(Post), contact);
			}
			catch (Exception ex)
			{
				if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation($"Error in Creating Contact." + ex.Message);
				return NotFound();
			}
			
		}

		/// <summary>
		/// Edit Contact
		/// </summary>
		/// <param name="contactId"></param>
		/// <param name="newContact"></param>
		/// <returns></returns>
		/// <remarks>
		/// Sample request: 
		/// 
		///     PUT api/Contact/{contactId}
		///     
		/// </remarks>
		/// <response code="200">The target resource was successfully updated.</response>
		/// <response code="400">The request was not valid.</response>
		/// <response code="404">The target resource does not exist.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[HttpPut("{contactId}")]
		public IActionResult Put(int contactId, Contact newContact)
		{
			if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation($"Editing Contact.");

			try
			{
				if (!ModelState.IsValid || newContact == null)
				{
					return BadRequest(ModelState);
				}

				var existingContact = _contactRepository.GetContact(contactId);
				if (existingContact == null)
				{
					return NotFound();
				}

				Contact updContact = new Contact();
				updContact.ContactId = existingContact.ContactId;
				updContact.FirstName = existingContact.FirstName;
				updContact.LastName = existingContact.LastName;
				updContact.Email = existingContact.Email;
				updContact.PhoneNumber = existingContact.PhoneNumber;
				updContact.Status = existingContact.Status;

				_contactRepository.EditContact(existingContact, newContact);


				return Ok(newContact);
			}
			catch (Exception ex)
			{
				if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation($"Error in Editing Contact." + ex.Message);
				return NotFound();
			}
		}

		/// <summary>
		/// Delete Contact
		/// </summary>
		/// <param name="contactId"></param>
		/// <returns></returns>
		/// <remarks>
		/// Sample request: 
		/// 
		///     DELETE api/Contact/{contactId}
		/// </remarks>
		/// <response code="200">The target resource was successfully deleted.</response>
		/// <response code="400">The request was not valid.</response>
		/// <response code="404">The target resource does not exist.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[HttpDelete("{contactId}")]
        public IActionResult DeleteContact(int contactId)
        {
			if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation($"Deleting Contact.");
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var existingContact = _contactRepository.GetContact(contactId);
				if (existingContact == null)
				{
					return NotFound();
				}

				_contactRepository.DeleteContact(existingContact);


				return Ok(existingContact);
			}
			catch (Exception ex)
			{
				if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation($"Error in Deleting Contact." + ex.Message);
				return NotFound();
			}
		}
		
	}
}
