using System;
using System.Collections.Generic;

namespace Jaytas.Omilos.Web.Controllers.Commands
{
	/// <summary>
	/// Represents a command to perform for a resource.  This helps bridge the gap between
	/// RESTful interfaces and business layers by providing an extension point for properties that do not exist
	/// on our Api contracts.
	/// </summary>
	/// <typeparam name="TModel">The type of the model.</typeparam>
	/// <typeparam name="TModelBaseType">The base type of the model.</typeparam>
	public class Command<TModel, TModelBaseType> : ICommand<TModel, TModelBaseType> where TModelBaseType : struct
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Command{TModel, TModelBaseType}"/> class.
		/// </summary>
		/// <param name="resource">The resource.</param>
		public Command(TModel resource) : this(resource, new Dictionary<string, dynamic>())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Command{TModel, TModelBaseType}"/> class.
		/// </summary>
		/// <param name="resource">The resource.</param>
		/// <param name="commandProperties">The additional command Properties.</param>
		public Command(TModel resource, Dictionary<string, dynamic> commandProperties)
		{
			Resource = resource;
			CommandProperties = commandProperties;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Command{TModel, TModelBaseType}"/> class.
		/// </summary>
		/// <param name="resource">The resource.</param>
		/// <param name="resourceId">The resource identifier.</param>
		public Command(TModel resource, TModelBaseType resourceId) : this(resource, resourceId, new Dictionary<string, dynamic>())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Command{TModel, TModelBaseType}"/> class.
		/// </summary>
		/// <param name="resource">The resource.</param>
		/// <param name="resourceId">The resource identifier.</param>
		/// <param name="commandProperties">The additional command Properties.</param>
		public Command(TModel resource, TModelBaseType resourceId, Dictionary<string, dynamic> commandProperties) : this(resource, commandProperties)
		{
			ResourceId = resourceId;
		}

		/// <summary>
		/// Gets or sets the Resource this command applies to.
		/// </summary>
		/// <value>
		/// The model.
		/// </value>
		public TModel Resource { get; set; }

		/// <summary>
		/// Gets or sets the identifier for the Resource instance.
		/// <remarks>This ID is necessary because our Api models do not expose an ID property, 
		/// instead, the ID is provided in the URL routes.</remarks>
		/// </summary>
		public TModelBaseType ResourceId { get; set; }

		/// <summary>
		/// Additional Properties to set
		/// </summary>
		public Dictionary<String, dynamic> CommandProperties { get; set; }
	}
}
