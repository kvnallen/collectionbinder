using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace collectionbinder.Binders
{
    public class CollectionBinder<T> : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var propriedades = new List<PropertyInfo>();

            string collectionName = bindingContext.ModelName;

            var tipo = typeof(T);
            var props = tipo.GetProperties();

            var form = controllerContext.HttpContext.Request.Form;

            var count = (int)GetLenghtValue(bindingContext, collectionName, props);

            var valores = new List<string[]>();

            foreach (var prop in props)
            {
                var valoresRetornados = GetValue(bindingContext, collectionName, prop);

                if (valoresRetornados != null)
                {
                    valores.Add(valoresRetornados);

                    propriedades.Add(prop);
                }

            }

            var listType = typeof(List<>);

            var constructedListType = listType.MakeGenericType(typeof(T));

            var instance = (IList)Activator.CreateInstance(constructedListType);

            for (int i = 0; i < count; i++)
            {
                var instance2 = Activator.CreateInstance(typeof(T));

                for (int j = 0; j < valores.Count; j++)
                {
                    TrySetProperty(instance2, propriedades[j], valores[j][i]);
                }

                instance.Add(instance2);
            }

            return instance;

        }

        private void TrySetProperty(object obj, PropertyInfo property, object value)
        {
            if (property != null && property.CanWrite)
            {
                Type tipo = property.PropertyType;
                var val = ConvertValue(value, tipo);
                property.SetValue(obj, val, null);
            }

        }
        public static object ConvertValue(object value, Type tipo)
        {
            return Convert.ChangeType(value, tipo);
        }

        private string[] GetValue(ModelBindingContext bindingContext, string collectionName, PropertyInfo key)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(String.Concat(collectionName, ".", key.Name));

            if (valueResult == null)
            {
                return null;
            }

            return (string[])valueResult.ConvertTo(typeof(string[]));
        }

        private int? GetLenghtValue(ModelBindingContext bindingContext, string collectionName, PropertyInfo[] keys)
        {
            int? lenght = null;

            foreach (var key in keys)
            {
                var valueResult = bindingContext.ValueProvider.GetValue(String.Concat(collectionName, ".", key.Name));

                if (valueResult != null)
                {
                    var valores = valueResult.ConvertTo(typeof(string[])) as string[];
                    lenght = valores.Length;

                    return lenght;
                }

            }

            return lenght;
        }
    }
}