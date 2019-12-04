using Jaytas.Omilos.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Configuration.Providers
{
	public class DefaultConfigurationProvider : BaseConfiguration, IBaseConfiguration
	{
		public DefaultConfigurationProvider(IConfiguration configuration) : base(configuration)
		{
			ParseConfiguration();
		}
	}
}
