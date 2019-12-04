using AutoMapper;
using Jaytas.Omilos.Common;
using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Common.Exceptions;
using Jaytas.Omilos.Common.Models;
using Jaytas.Omilos.Common.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class BaseApiController : ControllerBase
	{
		/// <summary>
		/// The mapper.
		/// </summary>
		protected readonly IMapper Mapper;
		/// <summary>
		/// The Logger 
		/// </summary>
		//protected readonly ILogger Logger;
		/// <summary>
		/// initializes a new instance of the Deloitte.Radia.Web.Controllers.BaseApiController class.
		/// </summary>
		/// <param name="mapper">The mapper.</param>
		/// <param name="logger">The logger.</param>
		protected BaseApiController(IMapper mapper)//, ILogger logger)
		{
			Mapper = mapper;
			//Logger = logger;
		}

		/// <summary>
		/// Gets the generic Api error code.
		/// </summary>
		/// <value>The generic Api error code.</value>
		protected virtual ApiErrors GenericApiErrorCode { get; } = ApiErrors.Generic;

		/// <summary>
		/// Executes the provided function asynchronously with exception handling.  It converts the TDomain result from the function
		/// to TResponse and returns it as an OK/200 response. 
		/// </summary>
		/// <typeparam name="TDomain">The type of the domain.</typeparam>
		/// <typeparam name="TResponse">The type of the response.</typeparam>
		/// <param name="action">The action asynchronous.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> ExecuteWithExceptionHandlingAsync<TDomain, TResponse>(Func<Task<TDomain>> action)
		{
			try
			{
				var domainModel = await action().ConfigureAwait(true);
				if (domainModel == null)
				{
					return NotFound();
				}

				var webModel = Mapper.Map<TResponse>(domainModel);
				return Ok(webModel);
			}
			catch (BusinessValidationException bveEx)
			{
				return BadRequest(bveEx);
			}
			catch (AggregateException aggregateEx)
			{
				return BadRequest(aggregateEx);
			}
			catch (Exception ex)
			{
				return InternalServerError(GenericApiErrorCode, ex);
			}
		}

		/// <summary>
		/// Executes the provided function asynchronously with exception handling.  It converts the TDomain result from the function
		/// to TResponse and returns it as an OK/200 response. 
		/// </summary>
		/// <typeparam name="TResponse">The type of the response.</typeparam>
		/// <param name="action">The action asynchronous.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> ExecuteWithExceptionHandlingAsync<TResponse>(Func<Task<TResponse>> action)
		{
			try
			{
				var webModel = await action().ConfigureAwait(true);
				if (webModel == null)
				{
					return NotFound();
				}

				return Ok(webModel);
			}
			catch (BusinessValidationException bveEx)
			{
				return BadRequest(bveEx);
			}
			catch (AggregateException aggregateEx)
			{
				return BadRequest(aggregateEx);
			}
			catch (Exception ex)
			{
				return InternalServerError(GenericApiErrorCode, ex);
			}
		}

		/// <summary>
		/// Executes the paged result with (global) exception handling.
		/// </summary>
		/// <typeparam name="TDomain">The type of the result.</typeparam>
		/// <typeparam name="TResponse">The type of the result.</typeparam>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> ExecutePagedResultWithExceptionHandlingAsync<TDomain, TResponse>(Func<Task<PagedResultSet<TDomain>>> action)
		{
			try
			{
				var pagedModel = await action().ConfigureAwait(true);
				if (pagedModel == null || pagedModel.Items == null)
				{
					return NotFound();
				}

				var webModel = Mapper.Map<TResponse>(pagedModel.Items);
				AddPagingHeaders(pagedModel);
				return Ok(webModel);
			}
			catch (BusinessValidationException bveEx)
			{
				return BadRequest(bveEx);
			}
			catch (AggregateException aggregateEx)
			{
				return BadRequest(aggregateEx);
			}
			catch (Exception ex)
			{
				return InternalServerError(GenericApiErrorCode, ex);
			}
		}

		/// <summary>
		/// Executes the paged result with (global) exception handling.
		/// </summary>
		/// <typeparam name="TResponse">The type of the result.</typeparam>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> ExecutePagedResultWithExceptionHandlingAsync<TResponse>(Func<Task<PagedResultSet<TResponse>>> action)
		{
			try
			{
				var pagedModel = await action().ConfigureAwait(true);
				if (pagedModel == null || pagedModel.Items == null)
				{
					return NotFound();
				}

				AddPagingHeaders(pagedModel);
				return Ok(pagedModel.Items);
			}
			catch (BusinessValidationException bveEx)
			{
				return BadRequest(bveEx);
			}
			catch (AggregateException aggregateEx)
			{
				return BadRequest(aggregateEx);
			}
			catch (Exception ex)
			{
				return InternalServerError(GenericApiErrorCode, ex);
			}
		}

		/// <summary>
		/// Executes the provided function asynchronously with exception handling.  It converts the TDomain result from the function
		/// to TResponse and returns it as an OK/200 response. 
		/// </summary>
		/// <param name="action">The action asynchronous.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PutOrStatusCodeAsync(Func<Task> action)
		{
			try
			{
				// ensure our model is valid
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				await action().ConfigureAwait(true);

				return NoContentResult();
			}
			catch (BusinessValidationException bveEx)
			{
				return BadRequest(bveEx);
			}
			catch (AggregateException aggregateEx)
			{
				return BadRequest(aggregateEx);
			}
			catch (HttpRestException restEx)
			{
				return HandleRestError(restEx);
			}
			catch (Exception ex)
			{
				return InternalServerError(GenericApiErrorCode, ex);
			}
		}

		/// <summary>
		/// Creates the provided Resource and translates any errors into appropriate status code responses.
		/// </summary>
		/// <param name="action">The command to process containing the Resource to create.</param>
		/// <param name="routeName">Name of the get-by-identifier route.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PostOrStatusCodeAsync<TBaseType>(Func<Task<TBaseType>> action, string routeName)
		{
			try
			{
				// ensure our model is valid
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var newId = await action().ConfigureAwait(true);

				var routeValues = new Dictionary<string, object>
				{
					{"id", newId}
				};

				return CreatedAtRoute(routeName, routeValues, null);
			}
			catch (BusinessValidationException bveEx)
			{
				return BadRequest(bveEx);
			}
			catch (AggregateException aggregateEx)
			{
				return BadRequest(aggregateEx);
			}
			catch (HttpRestException restEx)
			{
				return HandleRestError(restEx);
			}
			catch (Exception ex)
			{
				return InternalServerError(GenericApiErrorCode, ex);
			}
		}


		/// <summary>
		/// Creates the provided Resource and translates any errors into appropriate status code responses.
		/// </summary>
		/// <param name="action">The command to process containing the Resource to create.</param>
		/// <param name="routeName">Name of the get-by-identifier route.</param>
		/// <param name="routeValues">route values.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PostOrStatusCodeAsync(Func<Task> action, string routeName, Dictionary<string, object> routeValues)
		{
			try
			{
				// ensure our model is valid
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				await action().ConfigureAwait(true);

				return CreatedAtRoute(routeName, routeValues, null);
			}
			catch (BusinessValidationException bveEx)
			{
				return BadRequest(bveEx);
			}
			catch (AggregateException aggregateEx)
			{
				return BadRequest(aggregateEx);
			}
			catch (HttpRestException restEx)
			{
				return HandleRestError(restEx);
			}
			catch (Exception ex)
			{
				return InternalServerError(GenericApiErrorCode, ex);
			}
		}

		/// <summary>
		/// Creates the provided Resource and translates any errors into appropriate status code responses.
		/// </summary>
		/// <param name="action">The command to process containing the Resource to create.</param>
		/// <param name="routeName">Name of the get-by-identifier route.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PostOrContentAsync<TDomain, TResponse>(Func<Task<TResponse>> action)
		{
			try
			{
				// ensure our model is valid
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var domain = await action().ConfigureAwait(true);
				var response = Mapper.Map<TResponse>(domain);

				return Ok(response);
			}
			catch (BusinessValidationException bveEx)
			{
				return BadRequest(bveEx);
			}
			catch (AggregateException aggregateEx)
			{
				return BadRequest(aggregateEx);
			}
			catch (HttpRestException restEx)
			{
				return HandleRestError(restEx);
			}
			catch (Exception ex)
			{
				return InternalServerError(GenericApiErrorCode, ex);
			}
		}

		/// <summary>
		/// Executes the provided function asynchronously with exception handling.  It converts the TDomain result from the function
		/// to TResponse and returns it as an OK/200 response. 
		/// </summary>
		/// <param name="action">The action asynchronous.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PatchOrStatusCodeAsync(Func<Task> action)
		{
			try
			{
				// ensure our model is valid
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				await action().ConfigureAwait(true);

				return NoContentResult();
			}
			catch (BusinessValidationException bveEx)
			{
				return BadRequest(bveEx);
			}
			catch (AggregateException aggregateEx)
			{
				return BadRequest(aggregateEx);
			}
			catch (HttpRestException restEx)
			{
				return HandleRestError(restEx);
			}
			catch (Exception ex)
			{
				return InternalServerError(GenericApiErrorCode, ex);
			}
		}

		/// <summary>
		/// Deletes the specified resource and yields 204/No Content on success.  Or 404/Not Found if the resource does not exist.
		/// This method will provide the appropriate exception handling as well.
		/// </summary>
		/// <param name="action">The command.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> DeleteOrStatusCodeAsync(Func<Task> action)
		{
			try
			{
				await action().ConfigureAwait(true);

				return NoContentResult();
			}
			catch (BusinessValidationException bveEx)
			{
				return BadRequest(bveEx);
			}
			catch (AggregateException aggregateEx)
			{
				return BadRequest(aggregateEx);
			}
			catch (HttpRestException restEx)
			{
				return HandleRestError(restEx);
			}
			catch (Exception ex)
			{
				return InternalServerError(GenericApiErrorCode, ex);
			}
		}

		/// <summary>
		/// Returns our standard SingularError for an internal server error
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <param name="ex"></param>
		/// <returns>NegotiatedContentResult <see cref="FriendlyError"/>.</returns>
		protected IActionResult InternalServerError(ApiErrors errorCode, Exception ex)
		{
			var bve = ex.GetBaseException() as BusinessValidationException;
			if (bve != null)
			{
				// sometimes BVE's are wrapped up in other exceptions (e.g. during an AutoMapper map)
				// find the BVE and responds appropriately according to it
				return BadRequest(bve);
			}
			var message = Mapper.Map<FriendlyError>(errorCode);
			//message.CorrelationId = Logger.Log(Log.Severity.Error, Log.Category.Business, ex);
			message.SetException(ex);
			 
			return StatusCode((int)HttpStatusCode.InternalServerError, message);
		}

		/// <summary>
		/// Returns our standard SingularError for a Conflict/409.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <returns></returns>
		protected IActionResult Conflict(ApiErrors errorCode)
		{
			var message = Mapper.Map<FriendlyError>(errorCode);
			//message.CorrelationId = Logger.Log(Log.Severity.Error, Log.Category.Business, message.Message);
			return StatusCode((int)HttpStatusCode.Conflict, message);
		}

		/// <summary>
		/// Returns our standard SingularError for a Bads Request/400.
		/// </summary>
		/// <param name="validationException">The validation exception.</param>
		/// <returns></returns>
		protected IActionResult BadRequest(BusinessValidationException validationException)
		{
			var message = Mapper.Map<FriendlyError>(validationException);
			//message.CorrelationId = Logger.Log(Log.Severity.Error, Log.Category.Business, validationException);
			return SafeNegotiatedContentResult(message.HttpStatusCode, message);
		}

		/// <summary>
		/// Returns a list of SingularErrors.
		/// For each BusinessValidationException a correct error code and message will be used.
		/// For all other Exceptions, a generic error will be used.
		/// </summary>
		/// <param name="aggregateExceptions">The aggregate exceptions.</param>
		/// <returns></returns>
		protected IActionResult BadRequest(AggregateException aggregateExceptions)
		{
			var responseCode = HttpStatusCode.BadRequest;
			var errors = new List<FriendlyError>();
			//var correlationId = Logger.Log(Log.Severity.Error, Log.Category.Business, aggregateExceptions);
			foreach (var innerException in aggregateExceptions.InnerExceptions)
			{
				var bizException = innerException as BusinessValidationException;
				if (bizException != null)
				{
					var message = Mapper.Map<FriendlyError>(bizException);
					//message.CorrelationId = correlationId;
					errors.Add(message);

				}
				else
				{
					var restException = innerException as HttpRestException;
					if (restException != null && restException.Body is List<FriendlyError>)
					{
						// try to take on the status code from the REST call that caused this error
						var errorStatusCode = restException.StatusCode;
						if (errorStatusCode >= HttpStatusCode.Ambiguous)
						{
							responseCode = errorStatusCode;
						}
						errors.AddRange((List<FriendlyError>)restException.Body);
					}
					else
					{
						// we dont know the exception, make a generic SingularError for it
						var unknownError = Mapper.Map<FriendlyError>(ApiErrors.Generic);
						unknownError.SetException(innerException);
						errors.Add(unknownError);
					}
				}
			}

			return SafeNegotiatedContentResult(responseCode, errors);
		}

		/// <summary>
		/// Returns a 400 with a list of SingularErrors + MissingRequiredField for each error in the ModelState dictionary provided.
		/// </summary>
		/// <param name="modelState">State of the model.</param>
		/// <returns></returns>
		protected new IActionResult BadRequest(ModelStateDictionary modelState)
		{
			var errors = new List<BusinessValidationException>();

			foreach (var state in modelState)
			{
				var dataElement = state.Key;
				foreach (var error in modelState[state.Key].Errors)
				{
					var message = string.IsNullOrEmpty(error.ErrorMessage) ? error.Exception.GetBaseException().Message : error.ErrorMessage;
					errors.Add(new BusinessValidationException(message, error.Exception, dataElement, BusinessErrors.MissingRequiredField));
				}
			}

			return BadRequest(new AggregateException(errors));
		}

		/// <summary>
		/// Returns a 400 with a SingularErrors + MissingRequiredField for the message provided.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		protected IActionResult BadRequest(string message)
		{
			return BadRequest(new BusinessValidationException(BusinessErrors.MissingRequiredField, message));
		}

		/// <summary>
		/// Safely returns a negotiated content result when the statusCode allows, or translates to the
		/// appropriate valid REST protocoal response.  i.e. In the case of NoContent response codes the
		/// REST protocol requires that we do not send any response content back.
		/// </summary>
		/// <typeparam name="T">Type of the body for this response.</typeparam>
		/// <param name="statusCode">The HTTP status code.</param>
		/// <param name="body">The body.</param>
		/// <returns></returns>
		private IActionResult SafeNegotiatedContentResult<T>(HttpStatusCode statusCode, T body)
		{
			if (statusCode == HttpStatusCode.NoContent)
			{
				// TODO: if we ever get here, should we just change the status code so we may return the body?
				return NoContentResult();
			}

			return StatusCode((int)statusCode, body);
		}

		/// <summary>
		/// Returns 204 - No Content result with the response body being the string 'No Content'.
		/// </summary>
		/// <returns>An IActionResult.</returns>
		protected IActionResult NoContentResult()
		{
			// No Content results MUST NOT contain any body content, otherwise it is a protocol violation
			return new StatusCodeResult((int)HttpStatusCode.NoContent);
		}

		/// <summary>
		/// Interprets an exception from calling a REST Api into SingularError objects and returns the 
		/// appropriate response.
		/// </summary>
		/// <param name="restEx"></param>
		/// <returns></returns>
		protected IActionResult HandleRestError(HttpRestException restEx)
		{
			var errors = new List<FriendlyError>();
			var serviceErrors = restEx.Body as List<FriendlyError>;
			if (serviceErrors != null)
			{
				// if we understand this exception, translate to SingularErrors
				errors.AddRange(serviceErrors);
			}
			else
			{
				// we dont know the exception, make a generic SingularError for it
				var unknownError = Mapper.Map<FriendlyError>(ApiErrors.Generic);
				//unknownError.CorrelationId = Logger.Log(Log.Severity.Error, Log.Category.Transport, restEx);
				unknownError.SetException(restEx);
				errors.Add(unknownError);
			}

			// use the status code from the original REST call that caused this error
			var httpStatusCode = HttpStatusCode.InternalServerError;
			var errorStatusCode = restEx.StatusCode;
			if (errorStatusCode >= HttpStatusCode.Ambiguous)
			{
				httpStatusCode = errorStatusCode;
			}
			return SafeNegotiatedContentResult(httpStatusCode, errors);
		}

		/// <summary>
		/// Adds the paging headers.
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="model">The model.</param>
		private void AddPagingHeaders<TResult>(PagedResultSet<TResult> model)
		{
			var response = Response;
			response.Headers.Add(Constants.SharedHttpHeaders.PagingTotal, model.Total.ToString());

			if (model.First.HasValue)
			{
				response.Headers.Add(Constants.SharedHttpHeaders.PagingFirst, model.First.ToString());
			}

			if (model.Last.HasValue)
			{
				response.Headers.Add(Constants.SharedHttpHeaders.PagingLast, model.Last.ToString());
			}
		}
	}
}
