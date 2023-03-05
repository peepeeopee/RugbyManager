using System.Reflection;
using System.Xml.Linq;
using AutoMapper;
using RugbyManager.Application.Common.Models;
using RugbyManager.Application.Players.Commands;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

        var types = assembly.GetExportedTypes()
                            .Where(t => t.GetInterfaces()
                                         .Any(HasInterface))
                            .ToList();

        var argumentTypes = new Type[] {typeof(Profile)};

        types.ForEach(t =>
        {
            var instance = Activator.CreateInstance(t);

            var methodInfo = t.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] {this});
            }
            else
            {
                var interfaces = t.GetInterfaces()
                                  .Where(HasInterface)
                                  .ToList();

                if (interfaces.Count > 0)
                {
                    interfaces.ForEach(i =>
                    {
                        var interfaceMethodInfo = i.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] {this});
                    });
                }
            }
        });
    }
}