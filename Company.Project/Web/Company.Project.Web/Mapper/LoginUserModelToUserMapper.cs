using AutoMapper;
using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.Web.Models;

namespace Company.Project.Web.Mapper
{
    public class LoginUserModelToUserMapper
    {
        public User LoginUserToUserMapping(UserModel userModel)
        {
            var config = new MapperConfiguration(conf => {

                conf.CreateMap<UserModel,User>();
            });

            IMapper iMapper = config.CreateMapper();

            var source = userModel;
            var degination = iMapper.Map<UserModel,User>(source);

            return degination;
        }
    }
}
