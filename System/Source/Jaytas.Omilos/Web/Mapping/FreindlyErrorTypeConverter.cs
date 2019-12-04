using AutoMapper;
using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Common.Exceptions;
using Jaytas.Omilos.Common.Extensions;
using Jaytas.Omilos.Common.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Mapping
{
	/// <summary>
	/// Factory to create FriendlyError from ApiErrors and BusinessValidationExceptions.
	/// </summary>
	public class FreindlyErrorTypeConverter : ITypeConverter<ApiErrors, FriendlyError>, ITypeConverter<BusinessValidationException, FriendlyError>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FreindlyErrorTypeConverter" /> class.
		/// AutoMapper will create a new instance of this class for every call to map.
		/// </summary>
		public FreindlyErrorTypeConverter()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="destination"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public FriendlyError Convert(BusinessValidationException source, FriendlyError destination, ResolutionContext context)
		{
			var validationException = source;
			ApiErrors apiErrorCode;

			switch (validationException.ErrorCode)
			{
				case BusinessErrors.MissingRequiredField:
				case BusinessErrors.InaccessibleResource:
				case BusinessErrors.FieldOutOfRange:
					apiErrorCode = ApiErrors.InvalidModel;
					break;
				case BusinessErrors.NotAuthorized:
					apiErrorCode = ApiErrors.Unauthorized;
					break;
				case BusinessErrors.DependentServiceFailure:
					apiErrorCode = ApiErrors.Generic;
					break;
				case BusinessErrors.ExpectedResourceGone:
					apiErrorCode = ApiErrors.ExpectedResourceGone;
					break;
				case BusinessErrors.ConflictingValue:
					apiErrorCode = ApiErrors.DuplicateResource;
					break;
				case BusinessErrors.PreconditionFailed:
					apiErrorCode = ApiErrors.PreconditionFailed;
					break;
				case BusinessErrors.OperationNotSupported:
					apiErrorCode = ApiErrors.OperationNotSupported;
					break;
				case BusinessErrors.DuplicateName:
					apiErrorCode = ApiErrors.DuplicateName;
					break;
				default:
					throw new NotImplementedException("Unknown enum " + validationException.ErrorCode);
			}

			// create & initialize new dto
			var result = new FriendlyError
			{
				Key = apiErrorCode.GetDisplayName(),
				Message = apiErrorCode.GetDescription(),
				DataElement = validationException.DataElement,
				HttpStatusCode = Convert(apiErrorCode),
				CorrelationId = Guid.NewGuid().ToString() //logger.ActivityId.ToString()
			};
			result.SetException(validationException);

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="destination"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public FriendlyError Convert(ApiErrors source, FriendlyError destination, ResolutionContext context)
		{
			var errorCode = source;

			var result = new FriendlyError
			{
				Key = errorCode.GetDisplayName(),
				Message = errorCode.GetDescription(),
				HttpStatusCode = Convert(errorCode),
				CorrelationId = Guid.NewGuid().ToString() //logger.ActivityId.ToString()
			};
			return result;
		}

		/// <summary>
		/// Converts the specified error code to a HTTP status code.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <returns></returns>
		private static System.Net.HttpStatusCode Convert(ApiErrors errorCode)
		{
			// Api error codes map 1-to-1 with http status codes
			return (System.Net.HttpStatusCode)(int)errorCode;
		}
	}
}
