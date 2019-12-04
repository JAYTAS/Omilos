using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jaytas.Omilos.Common.Enumerations
{
	/// <summary>
	/// Represents all error messages to be returned from the Api. The 'Name' corresponds to an error
	/// code a UI tier can keep off of for internalization. The 'Description' is the default English
	/// description to use.
	/// </summary>
	/// <remarks>
	/// The Name should be a three letter group code followed by a three digit error number.
	/// </remarks>
	public enum ApiErrors
	{
		/// <summary>
		/// The 400 Bad Request error when a invalid models is attempting to be created
		/// </summary>
		[Display(Name = Constants.ErrorCodes.InValidModel, Description = Constants.ErrorCodesDescriptions.InvalidModel)]
		InvalidModel = 400,

		/// <summary>
		/// The unauthorized 401 error for when a user is not authorized to perform a given action.
		/// </summary>
		[Display(Name = Constants.ErrorCodes.UnAuthorized, Description = Constants.ErrorCodesDescriptions.UnAuthorized)]
		Unauthorized = 401,

		/// <summary>
		/// The unauthorized 401 error for when a user is not authorized to perform a given action.
		/// </summary>
		[Display(Name = Constants.ErrorCodes.Forbidden, Description = Constants.ErrorCodesDescriptions.UnAuthorized)]
		Forbidden = 403,

		/// <summary>
		/// The 409 Conflict error when a duplicate resource is attempting to be created
		/// </summary>
		[Display(Name = Constants.ErrorCodes.DuplicateResource, Description = Constants.ErrorCodesDescriptions.DuplicateResource)]
		DuplicateResource = 409,

		/// <summary>
		/// The 410 Gone error when data expected by a service is missing
		/// </summary>
		[Display(Name = Constants.ErrorCodes.NotFound, Description = Constants.ErrorCodesDescriptions.NotFound)]
		NotFound = 404,

		/// <summary>
		/// The 410 Gone error when data expected by a service is missing
		/// </summary>
		[Display(Name = Constants.ErrorCodes.ExpectedResourceGone, Description = Constants.ErrorCodesDescriptions.ExpectedResourceGone)]
		ExpectedResourceGone = 410,

		/// <summary>
		/// The 412 Precondition Failed when an expected precondition was not met
		/// </summary>
		[Display(Name = Constants.ErrorCodes.PreconditionFailed, Description = Constants.ErrorCodesDescriptions.PreconditionFailed)]
		PreconditionFailed = 412,

		/// <summary>
		/// The operation trying to perform is not supported on the entity
		/// </summary>
		[Display(Name = Constants.ErrorCodes.OperationNotSupported, Description = Constants.ErrorCodesDescriptions.OperationNotSupported)]
		OperationNotSupported = 420,

		/// <summary>
		/// There is another element with the same name. Please use another name.
		/// </summary>
		[Display(Name = Constants.ErrorCodes.DuplicateName, Description = Constants.ErrorCodesDescriptions.DuplicateName)]
		DuplicateName = 428,

		/// <summary>
		/// The generic internal server error for when not specific error information can be gleaned.
		/// </summary>
		[Display(Name = Constants.ErrorCodes.General, Description = Constants.ErrorCodesDescriptions.General)]
		Generic = 500,
	}
}
