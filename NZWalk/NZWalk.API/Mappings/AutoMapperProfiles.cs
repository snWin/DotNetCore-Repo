/*
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Mappings
{
	//AutoMapperProfiles class inherits from AutoMapper package which is Profile class 
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			//create mapping between domain and DTOs
			//CreateMap<Source,Desitation>
			CreateMap<Region, RegionDto>().ReverseMap();
			//here map only for single Entitt Map, Automapper is smart enough to convert the list of Regions as well. 

			//For Post - Create([FromBody] AddRegionRequestDto addRegionRequestDto). Actually, ReverseMap is not necessary here as it is save, one direction.
			CreateMap<AddRegionRequestDto, Region>().ReverseMap();
			CreateMap<AddWalkRequestDto,Walk>().ReverseMap();
			CreateMap<Walk,WalkDto>().ReverseMap();
		}
	}
}
*/

// If the Names of the properties are match each other.
//public class AutoMapperProfiles : Profile
//{
//	public AutoMapperProfiles()
//	{
//		//create mapping between domain and DTOs
//		//CreateMap<Source,Desitation>

//		//CreateMap<UserDTO,UserDomain>();
//		//CreateMap<UserDomain,UserDTO>();
//		//OR 
//		//CreateMap<UserDTO, UserDomain>().ReverseMap(); // this will create mapping in both direction, so we don't have to write two lines of code for mapping in both direction.
//	}
//}
//public class UserDTO
//{
//	public string FirstName { get; set; }
//}
//public class UserDomain
//{
//	public string FirstName { get; set; }
//}



// If the Names of the properties are NOT match each other.
//public class AutoMapperProfiles : Profile
//{
//	public AutoMapperProfiles()
//	{
//		//create mapping between domain and DTOs
//		//CreateMap<Source,Desitation>

//		CreateMap<UserDTO,UserDomain>()
//			.ForMember(x=>x.Name, opt=>opt.MapFrom(x=>x.FirstName))
//			.ReverseMap(); // this will map the FirstName property of UserDTO to the Name property of UserDomain.
//	}
//}
//public class UserDTO
//{
//	public string FirstName { get; set; }
//}
//public class UserDomain
//{
//	public string Name { get; set; }
//}