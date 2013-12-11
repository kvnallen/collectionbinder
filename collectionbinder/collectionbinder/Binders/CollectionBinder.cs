using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace collectionbinder.Binders
{
    public class CollectionBinder<T> : ICollectionBinder
    {

        /// <summary>
        /// Default implementation of method of the interface IModelBinder
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            
            
            var properties = new List<PropertyInfo>();

            var collectionName = bindingContext.ModelName;
            var props = typeof(T).GetProperties();
            var count = GetLenghtValue(bindingContext, collectionName, props);

            var values = new List<string[]>();

            foreach (var prop in props)
            {
                var valoresRetornados = GetValue(bindingContext, collectionName, prop);

                if (valoresRetornados != null)
                {
                    values.Add(valoresRetornados);

                    properties.Add(prop);
                }

            }

            //A new instance of the Generic type of the collection
            //passed by the controller
            var listOfObjects = new List<T>();

            for (var i = 0; i < count; i++)
            {
                var genericObject = Activator.CreateInstance<T>();

                for (var j = 0; j < values.Count; j++)
                {
                    TrySetProperty(genericObject, properties[j], values[j][i]);
                }

                //Add the object created to the list
                listOfObjects.Add(genericObject);
            }


            //Return the list of objects with all the values
            return listOfObjects;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        private static void TrySetProperty(object obj, PropertyInfo property, object value)
        {
            if (property != null && property.CanWrite)
            {
                var val = Convert.ChangeType(value, property.PropertyType);
                property.SetValue(obj, val, null);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <param name="collectionName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string[] GetValue(ModelBindingContext bindingContext, string collectionName, PropertyInfo key)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(collectionName + "." + key.Name);

            if (valueResult == null)
            {
                return null;
            }

            return (string[])valueResult.ConvertTo(typeof(string[]));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <param name="collectionName"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        private static int? GetLenghtValue(ModelBindingContext bindingContext, string collectionName, IEnumerable<PropertyInfo> keys)
        {
            foreach (var key in keys)
            {
                var valueResult = bindingContext.ValueProvider.GetValue(collectionName + "." + key.Name);

                if (valueResult != null)
                {
                    var valores = valueResult.ConvertTo(typeof(string[])) as string[];

                    if (valores != null)
                    {
                        return valores.Length;
                    }
                }

            }

            return null;
        }
    }
}