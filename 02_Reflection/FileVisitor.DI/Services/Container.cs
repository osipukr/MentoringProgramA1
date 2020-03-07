using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FileVisitor.DI.Attributes;
using FileVisitor.DI.Interfaces;
using FileVisitor.DI.Models;

namespace FileVisitor.DI.Services
{
    /// <summary>
    ///     Default implementation of <see cref="IContainer" />.
    /// </summary>
    public class Container : IContainer
    {
        private readonly List<RegisteredObject> _registeredObjects = new List<RegisteredObject>();

        /// <summary>
        ///     Ctor.
        /// </summary>
        public Container()
        {
            RegisterInjectedAttributes();
        }

        #region Implementation IContainer

        public void Register<TTypeToResolve, TTargetType>(bool isSingleton = false)
        {
            Register(typeof(TTypeToResolve), typeof(TTargetType), isSingleton);
        }

        public void Register<TTypeToResolve>(bool isSingleton = false)
        {
            Register(typeof(TTypeToResolve), isSingleton);
        }

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve)Resolve(typeof(TTypeToResolve));
        }

        #endregion

        #region Private Methods

        private void Register(Type typeToResolve, bool isSingleton = false)
        {
            Register(typeToResolve, typeToResolve, isSingleton);
        }

        private void Register(Type typeToResolve, Type targetType, bool isSingleton = false)
        {
            _registeredObjects.Add(new RegisteredObject
            {
                TypeToResolve = typeToResolve,
                TargetType = targetType,
                IsSingleton = isSingleton
            });
        }

        private void RegisterInjectedAttributes()
        {
            var definedTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var definedType in definedTypes)
            {
                var isImportedConstructor = definedType.GetCustomAttribute<ImportConstructorAttribute>() != null;

                if (isImportedConstructor)
                {
                    Register(definedType);
                }

                var exportAttr = definedType.GetCustomAttribute<ExportAttribute>();

                if (exportAttr != null)
                {
                    if (exportAttr.TypeToResolve != null)
                    {
                        Register(exportAttr.TypeToResolve, definedType);
                    }

                    Register(definedType);
                }
            }
        }

        private object Resolve(Type typeToResolve)
        {
            var registeredType = _registeredObjects.FirstOrDefault(t => t.TypeToResolve == typeToResolve);

            if (registeredType != null)
            {
                if (registeredType.IsSingleton)
                {
                    if (registeredType.SingletonInstance == null)
                    {
                        registeredType.SingletonInstance = CreateInstance(registeredType.TargetType);
                    }

                    return registeredType.SingletonInstance;
                }

                return CreateInstance(registeredType.TargetType);
            }

            throw new Exception($"You tried to resolve not registered type: {typeToResolve}");
        }

        private object CreateInstance(Type type)
        {
            var constructorParameters = GetConstructorParameters(type).ToArray();

            var instance = constructorParameters.Any()
                ? Activator.CreateInstance(type, constructorParameters)
                : Activator.CreateInstance(type);

            ResolveImportedProperties(instance);

            return instance;
        }

        private void ResolveImportedProperties(object instance)
        {
            var properties = instance.GetType().GetProperties();

            foreach (var property in properties)
            {
                var isImported = property.GetCustomAttribute<ImportAttribute>() != null;

                if (isImported)
                {
                    var propertyValue = Resolve(property.PropertyType);

                    property.SetValue(instance, propertyValue);
                }
            }
        }

        private IEnumerable<object> GetConstructorParameters(Type type)
        {
            var constructorInfo = type.GetConstructors().First();
            var parameters = constructorInfo.GetParameters();

            foreach (var parameter in parameters)
            {
                yield return Resolve(parameter.ParameterType);
            }
        }

        #endregion
    }
}