using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Clients.Utilities
{
    public class TestContext
        : IDisposable
    {
        public T Attach<T>(T obj)
            where T: IDisposable
        {
            _objects.Add(obj);

            return obj;
        }

        public void Dispose()
        {
            foreach (IDisposable obj in _objects)
            {
                obj.Dispose();
            }
        }

        private readonly List<IDisposable> _objects = new List<IDisposable>();
    }
}
