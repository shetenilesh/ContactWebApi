using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Contacts.Controllers;
using Contacts.Interfaces;
using Contacts.Models.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;

namespace Contacts.Test
{
	[TestClass]
	public class ContactTest
	{
		/// <summary>
		/// Test Case for Get Contacts 
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public void GetContacts()
		{
			ContactController controller = SetUpTestData("GET");
			//Act
			var actualResult = controller.Get();
			//Assert           
			actualResult.Should().NotBeNull();
		}

		/// <summary>
		/// Test Case for Create Contacts 
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public void CreateContact()
		{
			ContactController controller = SetUpTestData("POST");
			Contact contact = new Contact()
			{
				ContactId = 1,
				FirstName = "ABC",
				LastName = "XYZ",
				Email = "abc@xyz.com",
				PhoneNumber = "9898978788",
				Status = true
			};
			//Act
			var actualResult = controller.Post(contact);
			//Assert           
			actualResult.Should().NotBeNull();
		}


		/// <summary>
		/// Test Case for Edit Contact
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public void EditContact()
		{
			ContactController controller = SetUpTestData("GET");
			Contact contact = new Contact()
			{
				ContactId = 1,
				FirstName = "NMM",
				LastName = "SDH",
				Email = "abc@xyz.com",
				PhoneNumber = "9898978788",
				Status = true
			};
			//Act
			var actualResult = controller.Put(contact.ContactId, contact);
			//Assert           
			actualResult.Should().NotBeNull();
		}


		/// <summary>
		/// Test Case for Delete Contact
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public void DeleteContact()
		{
			ContactController controller = SetUpTestData("GET");
			Contact contact = new Contact()
			{
				ContactId = 1,
				FirstName = "ABC",
				LastName = "XYZ",
				Email = "abc@xyz.com",
				PhoneNumber = "9898978788",
				Status = true
			};
			//Act
			var actualResult = controller.DeleteContact(contact.ContactId);
			//Assert           
			actualResult.Should().NotBeNull();
		}

		public static ContactController SetUpTestData(string type)
		{
			Mock<IContactRepository> mockRepository = new Mock<IContactRepository>();
			Mock<ILogger<ContactController>> mockLogger= new Mock<ILogger<ContactController>>();
			Mock<IMapper> mapperMock = new Mock<IMapper>();
			
			mockRepository.Setup(x => x.GetAllContacts());
			

			if (type == "POST")
			{
				Contact contact = new Contact()
				{
					ContactId = 2,
					FirstName = "AABBCC",
					LastName = "XYZ",
					Email = "aabbcc@xyz.com",
					PhoneNumber = "9898978788",
					Status = true
				};
				mapperMock.Setup(m => m.Map<Contact>(It.IsAny<Contact>())).Returns(contact);
				mockRepository.Setup(x => x.CreateContact(contact));
			}
			else if (type == "GET")
			{
				List<Contact> applicationMessageModels = new List<Contact>();

				Contact contact = new Contact()
				{
					ContactId = 1,
					FirstName = "ABBBC",
					LastName = "XYZ",
					Email = "abbbc@xyz.com",
					PhoneNumber = "9898978788",
					Status = true
				};

				applicationMessageModels.Add(contact);
				mockRepository.Setup(x => x.GetAllContacts());
			}

			ContactController controller = new ContactController(mockLogger.Object, mockRepository.Object);

			return controller;
		}
	}
}
