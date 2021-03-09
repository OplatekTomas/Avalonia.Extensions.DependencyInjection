using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia.Extensions.DependencyInjection.Exceptions;

namespace Avalonia.Extensions.DependencyInjection {
    internal class ServiceCollection : IServiceCollection {
        public Dictionary<Type, object> Singletons { get; }

        public Dictionary<Type, (Func<object>, IEnumerable<FieldInfo>)> Transients { get; }

        public ServiceCollection() {
            Singletons = new Dictionary<Type, object>();
            Transients = new Dictionary<Type, (Func<object> ctor, IEnumerable<FieldInfo> fields)>();
        }
        
        public IEnumerable<Type> GetServices() {
            return Singletons.Keys.Concat(Transients.Keys);
        }

        private object CreateTransient(Func<object> ctor, IEnumerable<FieldInfo> fields) {
            var obj = ctor.Invoke();
            foreach (var item in fields) {
                item.SetValue(obj, GetService(item.FieldType));
            }
            return obj;
        }

        public object GetService(Type type) {
            if (Singletons.ContainsKey(type)) {
                return Singletons[type];
            }
            if (Transients.ContainsKey(type)) {
                return CreateTransient(Transients[type].Item1, Transients[type].Item2);
            }
            throw new ServiceNotFoundException();
        }
        
        public void AddTransient<T>(Func<T> ctor) {
            var type = typeof(T);
            if (Contains(type)) {
                throw new ServiceAlreadyAddedException();
            }
            Transients.Add(type, (ctor as Func<object>, Helper.GetRequiredServices(typeof(T))));
        }
        
        public void AddSingleton<T>(Func<T> ctor) {
            var type = typeof(T);
            if (Contains(type)) {
                throw new ServiceAlreadyAddedException();
            }
            Singletons.Add(type, ctor.Invoke());
        }

        private bool Contains(Type t) {
            return Singletons.ContainsKey(t) || Transients.ContainsKey(t);
        }
    }
}