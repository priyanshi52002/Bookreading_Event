using AutoMapper;
using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.Web.Models;

namespace Company.Project.Web.Mapper
{
    public class RegisterUserModelToUserMapper
    {
        //Using Automapping for map RegisterUsrModel to User Class Properties
        public User RegisterUserToUserMapping(RegisterUserModel registerUserModel)
        {
            var config = new MapperConfiguration(conf => {

                conf.CreateMap<RegisterUserModel,User>();
            });

            IMapper iMapper = config.CreateMapper();
            var source = registerUserModel;
            var degination = iMapper.Map<RegisterUserModel,User>(source);

            return degination;
        }
    }
}
