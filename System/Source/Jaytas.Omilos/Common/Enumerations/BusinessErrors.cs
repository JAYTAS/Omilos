using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Enumerations
{
	/// <summary>
	/// Values that represent business errors.
	/// </summary>
	/// 

	public enum BusinessErrors
	{
		/// <summary>
		/// An enum constant representing the missing required field option.
		/// </summary>
		MissingRequiredField = 1,

		/// <summary>
		/// An enum constant representing a required resource is not accesible.
		/// The resource could be deleted, or the current actor may not be authorized.
		/// </summary>
		InaccessibleResource,

		/// <summary>
		/// An enum constant representing the field out of range option.
		/// 1. Date out of range
		/// 2. Incorrect number of expected items in list
		/// 3. Numerical values are too large or small
		/// </summary>
		FieldOutOfRange,

		/// <summary>
		/// An enum constant representing the not authorized option.
		/// </summary>
		NotAuthorized,

		/// <summary>
		/// Represents when a microservice call failed and the calling code cannot continue
		/// </summary>
		DependentServiceFailure,

		/// <summary>
		/// Represents when data that is expected to exist is missing.  These situations should never happen.
		/// </summary>
		ExpectedResourceGone,

		/// <summary>
		/// An enum constant representing the value conflicts with another resource/value.
		/// </summary>
		ConflictingValue,

		/// <summary>
		/// An enum constant representing an error due to operation whose business rules set a precondition that doesn't match 
		/// </summary>
		PreconditionFailed,

		/// <summary>
		/// An Enum to represent that the operation trying to perform is not supported on the entity. 
		/// </summary>
		OperationNotSupported,

		/// <summary>
		/// Duplicate Element Name
		/// </summary>
		DuplicateName
	}
}
