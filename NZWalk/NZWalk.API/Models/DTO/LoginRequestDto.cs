using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NZWalk.API.Models.DTO
{
	public class LoginRequestDto
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string UserName {  get; set; }

		[Required]		
		[DataType(DataType.Password)]
		public string Password { get; set; }

	}
}
