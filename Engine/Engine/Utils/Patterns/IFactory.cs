using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.Utils.Patterns
{
    public interface IFactory<E>
    {
        E CreateProduct();
    }

    public class InstanceFactory<E> : IFactory<E>
    {
        private E inst;

        public InstanceFactory(E instance)
        {
            this.inst = instance;
        }

        public static InstanceFactory<E> Create(E instance)
        {
            return new InstanceFactory<E>(instance);
        }

        public E CreateProduct()
        {
            return inst;
        }
    }
}