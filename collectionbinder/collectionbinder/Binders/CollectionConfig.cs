using System.Collections.Generic;

namespace collectionbinder.Binders
{
    public static class CollectionConfig
    {
        public static void Add<T>()
        {
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(IEnumerable<T>), new CollectionBinder<T>());
        }
    }
}