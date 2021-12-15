using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Notes.Application.Common.Mappings
{
    public interface IMappableWith<T> where T : class
    {
        public void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
