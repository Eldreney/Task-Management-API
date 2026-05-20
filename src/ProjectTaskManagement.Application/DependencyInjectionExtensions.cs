using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
