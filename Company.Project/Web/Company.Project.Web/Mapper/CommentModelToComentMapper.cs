using AutoMapper;
using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Project.Web.Mapper
{
    public class CommentModelToComentMapper
    { 
        public Comment CommentModelToComentMapping(CommentModel commentModel)
        {
            var config = new MapperConfiguration(conf => {

                conf.CreateMap<CommentModel, Comment>();
            });

            IMapper iMapper = config.CreateMapper();

            var source = commentModel;
            var degination = iMapper.Map<CommentModel, Comment>(source);
            return degination;
        }
    }
}
