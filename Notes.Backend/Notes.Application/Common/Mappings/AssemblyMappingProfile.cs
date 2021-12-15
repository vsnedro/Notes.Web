using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Notes.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {
            _ = assembly ?? throw new ArgumentNullException(nameof(assembly));

            ApplyMappingsFromAssembly(assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMappableWith<>)))
                .ToList();

            foreach (var type in types)
            {
                //var methodInfo = type.GetMethod(nameof(IMappableWith<object>.Mapping));
                var method = type.GetMethod("Mapping")
                    ?? type.GetInterface(typeof(IMappableWith<>).Name)?.GetMethod("Mapping");
                method?.Invoke(Activator.CreateInstance(type), new object[] { this });
            }
        }
    }
}
