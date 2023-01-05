using AutoMapper;
using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Project.Web.Mapper
{
    public class CommentToCommentModelHelper
    {
        public CommentModel CommentToCommentModelMapping(Comment c)
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Comment, CommentModel>();

            });
            IMapper iMapper = config.CreateMapper();

            var source = c;

            var destination = iMapper.Map<Comment, CommentModel>(source);
            return destination;
        }

        public IEnumerable<CommentModel> GetCommentModels(IEnumerable<Comment> comments)
        {
            List<CommentModel> commentModels = new List<CommentModel>();
            foreach (var item in comments)
            {
                commentModels.Add(CommentToCommentModelMapping(item));
            }
            return commentModels;

        }
    }
}

