using Jaytas.Omilos.Common.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Domain
{
	/// <summary>
	/// 
	/// </summary>
	/// 
	public class GuidBaseEntity : IBaseEntity<Guid>, IEquatable<IBaseEntity<Guid>>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GuidEntity"/> class.
		/// </summary>
		public GuidBaseEntity()
		{
			Id = Guid.Empty;
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		/// <seealso cref="P:Deloitte.Radia.Common.Domain.IDomainEntity.Id"/>
		public Guid Id { get; set; }

		/// <summary>
		/// Indicates whether the current <see cref="object"/> is equal to another <see
		/// cref="object"/> of the same type.
		/// </summary>
		/// <param name="other">An <see cref="object"/> to compare with this <see cref="object"/>.</param>
		/// <returns>
		/// <c>true</c> if the current <see cref="object"/> is equal to the <paramref
		/// name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <seealso cref="M:System.IEquatable{Jaytas.Omilos.Common.Domain.Interfaces.IBaseEntity{Guid}}.Equals(IBaseEntity{Guid})"/>
		public virtual bool Equals(IBaseEntity<Guid> other)
		{
			return null != other && other.Id.Equals(Id);
		}

		/// <summary>
		/// Determines whether the specified <see cref="object"/>, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="object"/> is equal to this instance;
		/// otherwise, <c>false</c>.
		/// </returns>
		/// <seealso cref="M:System.Object.Equals(object)"/>
		public override bool Equals(object obj)
		{
			return Equals(obj as IBaseEntity<Guid>);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures
		/// like a hash table.
		/// </returns>
		/// <seealso cref="M:System.Object.GetHashCode()"/>
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		bool IEquatable<IBaseEntity<Guid>>.Equals(IBaseEntity<Guid> other)
		{
			return Equals(other);
		}
	}
}
