using AutoMapper;
using Jaytas.Omilos.Common.Domain.Interfaces;
using Jaytas.Omilos.Common.Exceptions;
using Jaytas.Omilos.Web.Controllers.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TBiz"></typeparam>
	/// <typeparam name="TModel"></typeparam>
	/// <typeparam name="TCommand"></typeparam>
	/// <typeparam name="TBizBaseType"></typeparam>
	public abstract class CrudBaseApiController<TBiz, TModel, TCommand, TBizBaseType> : BaseApiController
		where TModel : class
		where TBiz : IBaseEntity<TBizBaseType>
		where TCommand : class, ICommand<TModel, TBizBaseType>
		where TBizBaseType : struct
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mapper"></param>
		/// 
		protected CrudBaseApiController(IMapper mapper) : base(mapper)
		{
		}

		/// <summary>
		/// Executes the provided function asynchronously with exception handling.  It converts the IEnumerable&lt;TBiz&gt; result from the function
		/// to List&lt;TWebList&gt; and returns it as an OK/200 response.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> ExecuteWithExceptionHandlingAsync(Func<Task<IEnumerable<TBiz>>> action)
		{
			return await ExecuteWithExceptionHandlingAsync<IEnumerable<TBiz>, List<TModel>>(action).ConfigureAwait(true);
		}

		/// <summary>
		/// Executes the provided function asynchronously with exception handling.  It converts the IEnumerable&lt;TBiz&gt; result from the function
		/// to List&lt;TWebList&gt; and returns it as an OK/200 response.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> ExecuteWithExceptionHandlingAsync(Func<Task<IDictionary<string, IEnumerable<TBiz>>>> action)
		{
			return await ExecuteWithExceptionHandlingAsync<IDictionary<string, IEnumerable<TBiz>>, IDictionary<string, List<TModel>>>(action).ConfigureAwait(true);
		}

		/// <summary>
		/// Deletes the specified resource and yields 204/No Content on success.  Or 404/Not Found if the resource does not exist.
		/// This method will provide the appropriate exception handling as well.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// A Task&lt;IHttpActionResult&gt;
		/// </returns>
		protected internal async Task<IActionResult> DeleteOrStatusCodeAsync(TBizBaseType id)
		{
			return await DeleteOrStatusCodeAsync(CreateCommand(null, id)).ConfigureAwait(true);
		}

		/// <summary>
		/// Deletes the specified resource that belongs to a versioned set and yields 204/No Content on success.  Or 404/Not Found if the resource does not exist.
		/// This method will provide the appropriate exception handling as well.
		/// </summary>
		/// <param name="setId">The set identifier.</param>
		/// <param name="resourceId">The resource identifier.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> DeleteOrStatusCodeAsync(TBizBaseType setId, TBizBaseType resourceId)
		{
			return await DeleteOrStatusCodeAsync(CreateCommand(null, setId, resourceId)).ConfigureAwait(true);
		}

		/// <summary>
		/// Deletes the specified resource and yields 204/No Content on success.  Or 404/Not Found if the resource does not exist.
		/// This method will provide the appropriate exception handling as well.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> DeleteOrStatusCodeAsync(TCommand command)
		{
			try
			{
				var entityToDelete = await GetByIdAsync(command).ConfigureAwait(true);
				if (entityToDelete == null)
				{
					return NotFound();
				}

				await DeleteAsync(command).ConfigureAwait(true);

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
		/// Gets a list of all resources or the appropriate error if exceptions are thrown.
		/// </summary>
		/// <returns></returns>
		protected internal async Task<IActionResult> GetAllOrStatusCodeAsync()
		{
			return await GetAllOrStatusCodeAsync(null).ConfigureAwait(true);
		}

		/// <summary>
		/// Gets a list of all resources for the specified parent set and version or the appropriate error if exceptions are thrown.
		/// </summary>
		/// <param name="setId">The set identifier.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> GetAllOrStatusCodeAsync(TBizBaseType setId)
		{
			return await GetAllOrStatusCodeAsync(CreateCommand(null, setId, default(TBizBaseType))).ConfigureAwait(true);
		}

		/// <summary>
		/// Gets a list of all resources or the appropriate error if exceptions are thrown.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>all or status code.</returns>
		protected internal async Task<IActionResult> GetAllOrStatusCodeAsync(TCommand command)
		{
			try
			{
				var entities = await GetAllAsync(command).ConfigureAwait(true);
				// no list at all is different than an empty list (null => 404)
				if (entities == null)
				{
					return NotFound();
				}

				var models = Mapper.Map<List<TModel>>(entities);

				return Ok(models);
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
		/// Gets the resource specified by ID and version or 404 if not found or appropriate error response if exceptions occur.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> GetOrStatusCodeAsync(TBizBaseType id)
		{
			return await GetOrStatusCodeAsync(CreateCommand(null, id)).ConfigureAwait(true);
		}

		/// <summary>
		/// Gets the resource specified  the parent set ID, resource ID and versions or 404 if not found or appropriate error response if exceptions occur.
		/// </summary>
		/// <param name="setId">The set identifier.</param>
		/// <param name="resourceId">The resource identifier.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> GetOrStatusCodeAsync(TBizBaseType setId, TBizBaseType resourceId)
		{
			return await GetOrStatusCodeAsync(CreateCommand(null, setId, resourceId)).ConfigureAwait(true);
		}

		/// <summary>
		/// Gets the resource specified by ID and version or 404 if not found or appropriate error response if exceptions occur.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>
		/// The or status code.
		/// </returns>
		protected internal async Task<IActionResult> GetOrStatusCodeAsync(TCommand command)
		{
			try
			{
				var bizModel = await GetByIdAsync(command).ConfigureAwait(true);
				if (ReferenceEquals(bizModel, null))
				{
					return NotFound();
				}

				var webControls = Mapper.Map<TModel>(bizModel);

				return Ok(webControls);
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
		/// Posts the or status code.
		/// </summary>
		/// <param name="model">The client model.</param>
		/// <param name="routeName">Name of the route used to fetch the resource being created by it identifier.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PostOrStatusCodeAsync(TModel model, string routeName)
		{
			return await PostOrStatusCodeAsync(CreateCommand(model, default(TBizBaseType)), routeName).ConfigureAwait(true);
		}

		/// <summary>
		/// Creates the provided Resource belonging to the specified parent set and translates any errors into appropriate status code responses.
		/// </summary>
		/// <param name="setId">The set identifier.</param>
		/// <param name="model">The model.</param>
		/// <param name="routeName">Name of the get by identifier route.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PostOrStatusCodeAsync(TBizBaseType setId, TModel model, string routeName)
		{
			return await PostOrStatusCodeAsync(CreateCommand(model, setId, default(TBizBaseType)), routeName).ConfigureAwait(true);
		}

		/// <summary>
		/// Creates the provided Resource and translates any errors into appropriate status code responses.
		/// </summary>
		/// <param name="command">The command to process containing the Resource to create.</param>
		/// <param name="routeName">Name of the get-by-identifier route.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PostOrStatusCodeAsync(TCommand command, string routeName)
		{
			try
			{
				// ensure our model is valid
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var entityToCreate = Mapper.Map<TBiz>(command.Resource);

				var newId = await CreateAsync(entityToCreate).ConfigureAwait(true);

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
		/// Updates the provided Resource specified by the provided ID - and optionally version - and translates any errors into appropriate status code responses.
		/// </summary>
		/// <param name="webModel">The web model.</param>
		/// <param name="resourceId">The identifier.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PutOrStatusCodeAsync(TModel webModel, TBizBaseType resourceId)
		{
			return await PutOrStatusCodeAsync(CreateCommand(webModel, resourceId)).ConfigureAwait(true);
		}

		/// <summary>
		/// Updates the provided Resource specified by the provided ID - and optionally version - belonging to the specified parent set - and optionally set version - 
		/// and translates any errors into appropriate status code responses.
		/// </summary>
		/// <param name="setId">The set identifier.</param>
		/// <param name="webModel">The web model.</param>
		/// <param name="resourceId">The resource identifier.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PutOrStatusCodeAsync(TBizBaseType setId, TModel webModel, TBizBaseType resourceId)
		{
			return await PutOrStatusCodeAsync(CreateCommand(webModel, setId, resourceId)).ConfigureAwait(true);
		}

		/// <summary>
		/// Updates the provided Resource specified by the provided ID - and optionally version - and translates any errors into appropriate status code responses.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>
		/// A Task&lt;IHttpActionResult&gt;
		/// </returns>
		protected internal async Task<IActionResult> PutOrStatusCodeAsync(TCommand command)
		{
			try
			{
				// ensure our model is valid
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				// ensure it exists
				var bizModel = await GetByIdAsync(command).ConfigureAwait(true);
				if (ReferenceEquals(bizModel, null))
				{
					return NotFound();
				}

				// update it...once for the main model, second for any additional properties from the Command object
				Mapper.Map(command.Resource, bizModel);

				await UpdateAsync(command, bizModel).ConfigureAwait(true);

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
		/// Creates the command with the provided arguments.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="resourceId">The resource identifier.</param>
		/// <returns></returns>
		protected abstract TCommand CreateCommand(TModel model, TBizBaseType resourceId);

		/// <summary>
		/// Creates the command with the provided arguments used by resources that require parent identification.
		/// This is typically a SetCommand.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="setId">The set identifier.</param>
		/// <param name="resourceId">The resource identifier.</param>
		/// <returns></returns>
		protected abstract TCommand CreateCommand(TModel model, TBizBaseType setId, TBizBaseType resourceId);

		/// <summary>
		/// Creates the specified model and returns its id.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns>The new or exception.</returns>
		protected abstract Task<TBizBaseType> CreateAsync(TBiz model);

		/// <summary>
		/// Deletes the entity specified by identifier.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>
		/// A Task.
		/// </returns>
		protected abstract Task DeleteAsync(TCommand command);

		/// <summary>
		/// Returns a list of all business entities.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>
		/// all or exception.
		/// </returns>
		protected abstract Task<IEnumerable<TBiz>> GetAllAsync(TCommand command);

		/// <summary>
		/// Gets the business model by its identifier.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>
		/// The by identifier or exception.
		/// </returns>
		protected abstract Task<TBiz> GetByIdAsync(TCommand command);

		/// <summary>
		/// Updates the specified model.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="model">The model.</param>
		/// <returns>
		/// A Task.
		/// </returns>
		protected abstract Task UpdateAsync(TCommand command, TBiz model);
	}
}
