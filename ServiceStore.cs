using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kaharonus.Avalonia.DependencyInjection.Controls;

namespace Kaharonus.Avalonia.DependencyInjection {
    internal static class ServiceStore {
        private static ServiceCollection _services;
        private static Dictionary<Type, IEnumerable<FieldInfo>> requirements = new();
        public static void Inject<T>(this T window) where T : IInjectable {
            var type = window.GetType();
            if (!requirements.ContainsKey(type)) {
                return;
            }
            foreach (var req in requirements[type]) {
                req.SetValue(window, _services.GetService(req.FieldType));
            }
        }
        
        public static void Build(this ServiceCollection services, IEnumerable<Type> types) {
            _services = services;
            var serviceTypes = services.GetServices().ToList();
            foreach (var type in types) {
                var reqServices = Helper.GetRequiredServices(type);
                var enumerable = serviceTypes.ToList();
                if (!enumerable.Contains(type)) {
                    requirements.Add(type, reqServices);
                    continue;
                }
                var parentService  = services.GetService(type);
                foreach (var service in reqServices) {
                    var childService = services.GetService(service.FieldType);
                    service.SetValue(parentService, childService);
                }
            }
        }
    }
}