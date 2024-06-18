using System;
namespace FruitablesProject.Services.Interfaces
{
	public interface ISettingService
	{
		Task<Dictionary<string, string>> GetAllAsync();
	}
}

