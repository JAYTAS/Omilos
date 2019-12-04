using Jaytas.Omilos.Common.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaytas.Omilos.Common.Extensions
{
	/// <summary>
	/// 
	/// </summary>
	public static class FieldEntityExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <typeparam name="TFieldEntityType"></typeparam>
		/// <param name="entities"></param>
		public static void EnsureExposedIdField<TEntity, TFieldEntityType>(this IEnumerable<TEntity> entities) 
								where TEntity : class, IFieldEntity<TFieldEntityType>
								where TFieldEntityType : struct
		{
			entities.ToList().ForEach(entity => entity.GenerateExposedField());
		}
	}
}
