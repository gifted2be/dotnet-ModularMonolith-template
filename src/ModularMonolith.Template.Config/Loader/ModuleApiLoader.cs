using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ModularMonolith.Template.Config.Loader
{
    public static class ModuleApiLoader
    {
        public static void RegisterModuleApis(IMvcBuilder mvcBuilder, string modulesPath, IServiceCollection services) {
            if (!Directory.Exists(modulesPath)) return;

            string[] dllFiles = Directory.GetFiles(modulesPath, "*.API.dll", SearchOption.AllDirectories);

            foreach (string dll in dllFiles)
            {
                Assembly assembly = Assembly.LoadFrom(dll);
                mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));

                Type? moduleType = assembly.GetTypes()
                    .FirstOrDefault(t => 
                        t.IsClass && 
                        t.IsPublic && 
                        t.GetMethod("RegisterModule", BindingFlags.Public | BindingFlags.Static) != null);

                if (moduleType != null) {
                    MethodInfo? registerMethod = moduleType.GetMethod("RegisterModule", BindingFlags.Public | BindingFlags.Static);
                    registerMethod?.Invoke(null, new object[] { services });
                }
            }
        }
    }
}
