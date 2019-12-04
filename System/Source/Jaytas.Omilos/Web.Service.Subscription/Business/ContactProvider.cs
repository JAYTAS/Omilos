using Jaytas.Omilos.Common.Extensions;
using Jaytas.Omilos.Common.Models;
using Jaytas.Omilos.Web.Providers;
using Jaytas.Omilos.Web.Service.Subscription.Business.Interfaces;
using Jaytas.Omilos.Web.Service.Subscription.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.Business
{
	/// <summary>
	/// 
	/// </summary>
	public class ContactProvider : CrudByFieldBaseProvider<DomainModel.Contact, IContactRepository, long, Guid>, IContactProvider
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="contactRepository"></param>
		public ContactProvider(IContactRepository contactRepository) : base(contactRepository)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async override Task AssertEntityToCreateIsValidAsync(IEnumerable<DomainModel.Contact> domains)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="identifiers"></param>
		/// <returns></returns>
		public async override Task AssertEntityToDeleteIsValidAsync(IEnumerable<Guid> identifiers)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async override Task AssertEntityToUpdateIsValidAsync(IEnumerable<DomainModel.Contact> domains)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageDetails"></param>
		/// <param name="subscriptionId"></param>
		/// <returns></returns>
		public async Task<PagedResultSet<DomainModel.Contact>> MyContacts(Models.Common.PageDetails pageDetails, Guid? subscriptionId)
		{
			Expression<Func<DomainModel.Contact, bool>> expression = contact => true;

			if(subscriptionId != null && subscriptionId != Guid.Empty)
			{
				expression = contact => contact.SubscriptionId == subscriptionId;
			}

			if (!string.IsNullOrWhiteSpace(pageDetails?.SearchText))
			{
				expression = expression.And(contact => (contact.FirstName.Contains(pageDetails.SearchText) ||
														contact.LastName.Contains(pageDetails.SearchText) ||
														contact.Email.Contains(pageDetails.SearchText) ||
														contact.PhoneNumber.Contains(pageDetails.SearchText) ||
														contact.GroupContactAssociations.Any(asso => asso.Group.Name.Contains(pageDetails.SearchText))));
			}

			var contacts = await Repository.GetAsync(expression);

			var skip = pageDetails?.PageSize != null && pageDetails?.PageNo != null ? 
					   pageDetails.PageSize.Value * (pageDetails.PageNo.Value - 1) : 
					   pageDetails?.PageSize;
			return PagedResultSet<DomainModel.Contact>.Construct(contacts, skip, pageDetails?.PageSize);
		}
	}
}