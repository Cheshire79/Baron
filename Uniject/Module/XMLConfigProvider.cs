using Ninject.Activation;
using Uniject.Configuration;

namespace Uniject.Impl
{
    /// <summary>
    ///     Provides XML backed primitive types including string, float and double.
    /// </summary>
    public class XMLConfigProvider<T>
    {
        private readonly XMLConfigManager manager;

        public XMLConfigProvider(XMLConfigManager manager)
        {
            this.manager = manager;
        }

        public T CreateInstance(IContext context)
        {
            var value = Scoping.getContextAttribute<XMLConfigValue>(context);
            if (value == null)
            {
                return default(T);
            }

            return manager.getValue<T>(value.file, value.xpath);
        }
    }
}