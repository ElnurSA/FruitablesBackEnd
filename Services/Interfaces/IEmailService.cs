using System;
namespace FruitablesProject.Services.Interfaces
{
	public interface IEmailService
	{
        public void Send(string to, string subject, string html, string from = null);

    }
}

