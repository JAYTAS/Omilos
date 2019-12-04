using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaytas.Omilos.Common.Models
{
	/// <summary>
	/// Class that pages results
	/// </summary>
	public class PagedResultSet<T>
	{
		/// <summary>
		/// Gets or sets the items.
		/// </summary>
		/// <value>
		/// The items.
		/// </value>
		public IEnumerable<T> Items { get; set; }

		/// <summary>
		/// Gets or sets the start.
		/// </summary>
		/// <value>
		/// The start.
		/// </value>
		public int? First { get; set; }

		/// <summary>
		/// Gets or sets the end.
		/// </summary>
		/// <value>
		/// The end.
		/// </value>
		public int? Last { get; set; }

		/// <summary>
		/// Gets or sets the total.
		/// </summary>
		/// <value>
		/// The total.
		/// </value>
		public int Total { get; set; }


		/// <summary>
		/// Constructs a new pagged results from the Ienumerable query.
		/// </summary>
		/// <param name="items">The items.</param>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns></returns>
		public static PagedResultSet<T> Construct(IEnumerable<T> items, int? start, int? end)
		{
			if (!start.HasValue && !end.HasValue)
			{
				return new PagedResultSet<T> { Items = items, Total = items.Count() };
			}

			var count = items.Count();
			var skip = start.Value;
			if (start.HasValue)
			{
				items = items.Skip(skip);
			}

			var take = 0;
			if (end.HasValue && end.Value > skip)
			{
				take = end.Value - skip;
				items = items.Take(take);
			}

			var result = new PagedResultSet<T>
			{
				First = skip + 1,
				Last = end.Value < count ? end.Value : count,
				Total = count
			};
			result.Items = items.ToList();

			return result;
		}
	}
}
