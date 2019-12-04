using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Jaytas.Omilos.Common.Exceptions
{
	/// <summary>
	/// (Serializable)a business validation exception.
	/// </summary>
	[Serializable]
	public class BusinessValidationException : Exception
	{
		/// <summary>
		/// Gets the error code description.
		/// </summary>
		/// <value>
		/// The error code description.
		/// </value>
		/// 
		public string ErrorCodeDescription
		{
			get { return _errorCodeDescription; }
		}

		/// <summary>
		/// 
		/// </summary>

		private BusinessErrors _errorCode;

		/// <summary>
		/// 
		/// </summary>
		/// 

		private string _errorCodeDescription;

		/// <summary>
		/// Initializes a new instance of the <see cref="BusinessValidationException"/> class.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// 
		public BusinessValidationException(BusinessErrors errorCode) : this(errorCode, (string)null)
		{
			ErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BusinessValidationException"/> class.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <param name="dataElement">The data element.</param>
		/// 
		public BusinessValidationException(BusinessErrors errorCode, string dataElement) : base(errorCode.GetDescription())
		{
			DataElement = dataElement;
			ErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BusinessValidationException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="dataElement">The data element.</param>
		/// <param name="errorCode">The error code.</param>
		/// 
		public BusinessValidationException(string message, string dataElement, BusinessErrors errorCode) : base(errorCode.GetDescription())
		{
			DataElement = dataElement;
			ErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BusinessValidationException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="innerException">The inner exception.</param>
		/// <param name="dataElement">The data element.</param>
		/// <param name="errorCode">The error code.</param>
		/// 
		public BusinessValidationException(string message, Exception innerException, string dataElement, BusinessErrors errorCode) : base(errorCode.GetDescription(), innerException)
		{
			DataElement = dataElement;
			ErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BusinessValidationException"/> class.
		/// </summary>
		/// <param name="info">The information.</param>
		/// <param name="context">The context.</param>
		/// <param name="dataElement">The data element.</param>
		/// <param name="errorCode">The error code.</param>
		/// 
		protected BusinessValidationException(SerializationInfo info, StreamingContext context, string dataElement, BusinessErrors errorCode) : base(info, context)
		{
			DataElement = dataElement;
			ErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BusinessValidationException"/> class.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="businessErrors">The business errors.</param>
		/// 
		public BusinessValidationException(Exception exception, BusinessErrors businessErrors) : base(businessErrors.GetDescription(), exception)
		{
			ErrorCode = businessErrors;
		}

		/// <summary>
		/// Gets or sets the data element.
		/// </summary>
		/// <value>The data element.</value>
		/// 
		public string DataElement { get; set; }

		/// <summary>
		/// Gets or sets the error code.
		/// </summary>
		/// <value>The error code.</value>
		/// 
		public BusinessErrors ErrorCode
		{
			get { return _errorCode; }
			set
			{
				_errorCode = value;
				_errorCodeDescription = _errorCode.GetDescription();
			}
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="BusinessValidationException"/> class.
		/// </summary>
		/// <param name="info">The information.</param>
		/// <param name="context">The context.</param>
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected BusinessValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.DataElement = info.GetString(nameof(DataElement));
			this.ErrorCode = (BusinessErrors)info.GetValue(nameof(ErrorCode), typeof(BusinessErrors));
			this._errorCodeDescription = info.GetString(nameof(ErrorCodeDescription));
		}

		/// <summary>
		/// Gets the object data.
		/// </summary>
		/// <param name="info">The information.</param>
		/// <param name="context">The context.</param>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (null == info)
			{
				throw new ArgumentNullException();
			}


			info.AddValue(nameof(DataElement), this.DataElement);
			info.AddValue(nameof(ErrorCode), this.ErrorCode);
			info.AddValue(nameof(ErrorCodeDescription), this.ErrorCodeDescription);

			base.GetObjectData(info, context);
		}
	}
}
