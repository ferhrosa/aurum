using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Aurum.Api.Client.ts
{
    /// <summary>
    /// Criada a partir de:
    /// https://bitbucket.org/JamesDiacono/jdiacono/src/97b5ac3b21fe9cbe07b5f4e627961eb22ad1693d/JDiacono/TypeScript/TextTemplateUtilities.cs?at=default&fileviewer=file-view-default
    /// </summary>
    public static class TypeScriptUtil
    {

        public static string GenerateFromAssembly(Assembly assembly)
        {
            var result = new StringBuilder();

            var classes = assembly.ExportedTypes;

            foreach (var classe in classes)
            {
                result.AppendLine(GenerateTypeScriptDeclaration(classe)).AppendLine();
            }

            return result.ToString();
        }

        public static string GenerateTypeScriptDeclaration<T>()
        {
            return GenerateTypeScriptDeclaration(typeof(T));
        }

        public static string GenerateTypeScriptDeclaration(Type modelType)
        {
            if (modelType.IsEnum)
            {
                return GenerateEnum(modelType);
            }
            else
            {
                return GenerateClass(modelType);
            }
        }

        private static string GenerateEnum(Type modelType)
        {
            var result = new StringBuilder();

            result.AppendLine($"export enum {modelType.Name} {{");

            var names = modelType.GetEnumNames();
            var values = modelType.GetEnumValues();

            for (var i = 0; i < names.Length; i++)
            {
                // Carrega o valor do item e o converte para o valor do qual o enum herda.
                // Ou seja, se o enum for do tipo byte, vai pegar o valor byte do item do enum.
                var value = values.GetValue(i);
                value = Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));

                result.AppendLine($"    {names[i]} = {value},");
            }

            result.Append("}");

            return result.ToString();
        }

        private static string GenerateClass(Type modelType)
        {
            var result = new StringBuilder();

            result.AppendFormat("export interface {0} ", modelType.Name);

            if (modelType.BaseType != null
                && modelType.BaseType != typeof(object))
            {
                result.AppendFormat("extends {0} ", modelType.BaseType.Name);
            }

            result.Append("{");
            result.AppendLine();

            // Only get properties that are not derived
            var declaredProperties = modelType.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly
            );

            foreach (var property in declaredProperties)
            {
                var name = Nullable.GetUnderlyingType(property.PropertyType) == null
                    ? property.Name // not nullable
                    : (property.Name + "?"); // nullable

                var type = ConvertTypeName(property.PropertyType);

                result.AppendFormat("    {0}: {1};", name, type);
                result.AppendLine();
            }

            result.Append("}");

            return result.ToString();
        }

        public static string ConvertTypeName(Type type)
        {
            var family = GetTypeFamily(type);

            if (family == TypeFamily.SystemOrEnum)
            {
                var nullableUnderlyingType = Nullable.GetUnderlyingType(type);

                //// Enums are integers
                //if (type.IsEnum
                //    || (nullableUnderlyingType != null
                //        && nullableUnderlyingType.IsEnum))
                //{
                //    return "number";
                //}

                // Se for enum, também utiliza o enum gerado do modelo.
                if (type.IsEnum)
                {
                    return type.Name;
                }

                var typeMappings = new Dictionary<string, string>
                {
                    { "System.Int16", "number" },
                    { "System.Int32", "number" },
                    { "System.Int64", "number" },
                    { "System.UInt16", "number" },
                    { "System.UInt32", "number" },
                    { "System.UInt64", "number" },
                    { "System.Decimal", "number" },
                    { "System.Single", "number" },
                    { "System.Double", "number" },
                    { "System.Char", "string" },
                    { "System.String", "string" },
                    { "System.Guid", "string" },
                    { "System.Boolean", "boolean" },
                    { "System.DateTime", "Date" }
                };

                var underlyingTypeName = type.ToString();

                // Check for nullables
                if (nullableUnderlyingType != null)
                {
                    underlyingTypeName = nullableUnderlyingType.ToString();
                }

                if (typeMappings.ContainsKey(underlyingTypeName))
                {
                    return typeMappings[underlyingTypeName];
                }

                return "any";
            }
            else if (family == TypeFamily.Collection)
            {
                var elementType = type.GetGenericArguments().FirstOrDefault();

                return string.Format("{0}[]", elementType.Name);
            }

            // Single relationship to another model
            return type.Name;
        }

        private static Type[] GetReferencedTypes(Type type)
        {
            var result = new List<Type>();

            if (type.BaseType != null
                && type.BaseType != typeof(object))
            {
                result.Add(type.BaseType);
            }

            foreach (var property in type.GetProperties())
            {
                var family = GetTypeFamily(property.PropertyType);

                if (family == TypeFamily.Model)
                {
                    result.Add(property.PropertyType);
                }
                else if (family == TypeFamily.Collection)
                {
                    result.Add(property.PropertyType.GetGenericArguments().Single());
                }
            }

            return result.Distinct().ToArray();
        }

        private static TypeFamily GetTypeFamily(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var isString = (type == typeof(string));
            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
            var isDictionary = type.FullName.StartsWith(typeof(IDictionary).FullName)
                || type.FullName.StartsWith(typeof(IDictionary<,>).FullName)
                || type.FullName.StartsWith(typeof(Dictionary<,>).FullName);
            var isClr = (type.Module.ScopeName == "CommonLanguageRuntimeLibrary");

            if (!isString && !isDictionary && isEnumerable)
            {
                return TypeFamily.Collection;
            }
            else if (type.Module.ScopeName == "CommonLanguageRuntimeLibrary" || type.IsEnum)
            {
                return TypeFamily.SystemOrEnum;
            }

            // Fallback when type is not recognised.
            return TypeFamily.Model;
        }

        private enum TypeFamily
        {
            SystemOrEnum,
            Model,
            Collection
        }

    }
}
