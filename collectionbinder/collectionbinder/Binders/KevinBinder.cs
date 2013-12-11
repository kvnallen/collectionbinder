using System.Web.Mvc;

namespace collectionbinder.Binders
{
    public class KevinBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valor = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valor == null)
                base.BindModel(controllerContext, bindingContext);

            return null;
        }
    }
}