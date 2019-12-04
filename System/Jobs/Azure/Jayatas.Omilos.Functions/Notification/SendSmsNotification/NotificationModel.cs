using Jayatas.Omilos.Functions.Common.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Jayatas.Omilos.Functions.Notification.SendSmsNotification
{
	/// <summary>
	/// 
	/// </summary>
	public class NotificationModel : IDataTableResponseModel<NotificationModel>
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid NotificationId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public IEnumerable<NotificationModel> Fill(DataTable table)
		{
			return table.Rows.Cast<DataRow>().Select(row => new NotificationModel
			{
				NotificationId = Guid.Parse(row[nameof(NotificationId)].ToString()),
				PhoneNumber = row[nameof(PhoneNumber)].ToString(),
				Message = row[nameof(Message)].ToString()
			}).ToList();
		}
	}
}
