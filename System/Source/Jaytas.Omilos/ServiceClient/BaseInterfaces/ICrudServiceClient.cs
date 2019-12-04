using Jaytas.Omilos.Common;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jaytas.Omilos.ServiceClient.BaseInterfaces
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public interface ICrudServiceClient<T, in TKey> : IServiceClient where T : class
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="payload"></param>
		/// <returns></returns>
		[Post(Constants.Route.Crud.Create)]
		Task<T> Create([Body] T payload);

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[Get(Constants.Route.Crud.GetAll)]
		Task<List<T>> GetAll();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Get(Constants.Route.Crud.Get)]
		Task<T> Get(TKey id);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="payload"></param>
		/// <returns></returns>
		[Put(Constants.Route.Crud.Update)]
		Task Update(TKey id, [Body]T payload);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="payload"></param>
		/// <returns></returns>
		[Patch(Constants.Route.Crud.Patch)]
		Task Patch(TKey id, [Body]T payload);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[Delete(Constants.Route.Crud.Delete)]
		Task Delete(TKey key);
	}
}
