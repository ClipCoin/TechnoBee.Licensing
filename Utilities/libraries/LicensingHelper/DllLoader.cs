using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.LicensingHelper {
    public class DllLoader {
        public event EventHandler<Assembly> OnAssemblyLoaded;

        public void LoadAssemblyFromResource(string assembly) {
            var thisAssembly = Assembly.GetExecutingAssembly();
            
            var assemblyName = new AssemblyName(assembly);
            var dllName = assemblyName.Name + ".dll";

            var resources = thisAssembly.GetManifestResourceNames().Where(s => s.EndsWith(dllName)).ToList();
            if (resources.Any()) {
                var resourceName = resources.FirstOrDefault();
                if (resourceName == null)
                    throw new FileNotFoundException("The resource with a given key was not found");

                using (var stream = thisAssembly.GetManifestResourceStream(resourceName)) {
                    if (stream == null)
                        throw new InvalidOperationException("Could't read stream associated with a given resource"); ;

                    try {
                        var block = new byte[stream.Length];
                        stream.Read(block, 0, block.Length);

                        if (OnAssemblyLoaded != null) {
                            OnAssemblyLoaded(this, Assembly.Load(block));
                        }

                    }
                    catch (IOException) {
                        // log exception here
                        throw;
                    }
                    catch (BadImageFormatException) {
                        // log exception here
                        throw;
                    }
                }
            }
        }

        public Assembly LoadAssembly(string assemblyName) {
            try {
                var asm = Assembly.LoadFile(assemblyName);
                return asm;
            }
            catch(Exception ex) {
                throw;
            }
            

        }

    }
}
