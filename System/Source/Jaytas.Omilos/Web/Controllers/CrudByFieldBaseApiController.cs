using AutoMapper;
using Jaytas.Omilos.Common.Domain.Interfaces;
using Jaytas.Omilos.Common.Exceptions;
using Jaytas.Omilos.Web.Controllers.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Controllers
{
	public abstract class CrudByFieldBaseApiController<TBiz, TModel, TCommand, TBizFieldType, TBizBaseType> : BaseApiController
							where TModel : class
							where TBiz : IFieldEntity<TBizFieldType>, IBaseEntity<TBizBaseType>
							where TCommand : class, ICommand<TModel, TBizFieldType>
							where TBizBaseType : struct
							where TBizFieldType : struct
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mapper"></param>
		/// 
		protected CrudByFieldBaseApiController(IMapper mapper) : base(mapper)
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
		protected internal async Task<IActionResult> DeleteOrStatusCodeAsync(TBizFieldType id)
		{
			return await DeleteOrStatusCodeAsync(CreateCommand(null, id)).ConfigureAwait(true);
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
		protected internal async Task<IActionResult> GetOrStatusCodeAsync(TBizFieldType id)
		{
			return await GetOrStatusCodeAsync(CreateCommand(null, id)).ConfigureAwait(true);
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
			return await PostOrStatusCodeAsync(CreateCommand(model, default(TBizFieldType)), routeName).ConfigureAwait(true);
		}

		/// <summary>
		/// Posts the or status code.
		/// </summary>
		/// <param name="model">The client model.</param>
		/// <param name="commandProperties">The command properties.</param>
		/// <param name="routeName">Name of the route used to fetch the resource being created by it identifier.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PostOrStatusCodeAsync(TModel model, Dictionary<string, dynamic> commandProperties, string routeName)
		{
			return await PostOrStatusCodeAsync(CreateCommand(model, default(TBizFieldType), commandProperties), routeName).ConfigureAwait(true);
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

				//Maps Additional Properties
				if (command.CommandProperties.Keys.Any()){
					Mapper.Map(command, entityToCreate);
				}

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
		protected internal async Task<IActionResult> PutOrStatusCodeAsync(TModel webModel, TBizFieldType resourceId)
		{
			return await PutOrStatusCodeAsync(CreateCommand(webModel, resourceId)).ConfigureAwait(true);
		}

		/// <summary>
		/// Updates the provided Resource specified by the provided ID - and optionally version - and translates any errors into appropriate status code responses.
		/// </summary>
		/// <param name="webModel">The web model.</param>
		/// <param name="commandProperties">The command properties.</param>
		/// <param name="resourceId">The identifier.</param>
		/// <returns></returns>
		protected internal async Task<IActionResult> PutOrStatusCodeAsync(TModel webModel, TBizFieldType resourceId, Dictionary<string, dynamic> commandProperties)
		{
			return await PutOrStatusCodeAsync(CreateCommand(webModel, resourceId, commandProperties)).ConfigureAwait(true);
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
		protected abstract TCommand CreateCommand(TModel model, TBizFieldType resourceId);

		/// <summary>
		/// Creates the command with the provided arguments.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="commandProperties">The additional command Properties.</param>
		/// <param name="resourceId">The resource identifier.</param>
		/// <returns></returns>
		protected abstract TCommand CreateCommand(TModel model, TBizFieldType resourceId, Dictionary<string, dynamic> commandProperties);

		/// <summary>
		/// Creates the specified model and returns its id.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns>The new or exception.</returns>
		protected abstract Task<TBizFieldType> CreateAsync(TBiz model);

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
