using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace PowerPortalWebAPIHelper
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Power Portal WebAPI Helper"),
        ExportMetadata("Description", "Enable/disable Entities for Web API and Generate Code Snippets"),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAQAAADZc7J/AAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAAAmJLR0QAAKqNIzIAAAAJcEhZcwAAD8cAAA/HAZ8nTvUAAAAHdElNRQfkCQ4REi4VCFItAAACsElEQVRIx+3TXWiWdRgG8N/zPK9r7jWcH8NXN9rItZaBYsUKpS94QwuhFO1ACWxBOoIcFTGwk6hFCR4EfSCRBzM6aFKsDqQPkT4UfWEji5UrFCJzNqzmnHPV9j4d7Gk9vE6y1WHXfXDD/b+u677//+e5uTQ2e89sf4PMJU8qbHSl8X9iEFqkVuS4H1DlOl2Gp9TEUxlXa/eNIQVbhVjuSzdexKr1hLfdW1oObdCjaFS7hclUD+s0o4S3Uo/YUY1YrcPLGiDrAafFYm8qT6iRPVpL5DUKYqfkwZO+s9X8jJxnbUqE5bJCD5njXXX2aFMlTmY8ZIGbsFuvZ8xSp9Vh5zJesk6AQaw1otUJ91tqSIWnZCf7Z5UJ0GjMHHm7DHjLtowPrJRT1OGcFpvwmM887XPLUnLmKYK8ZbbJqtJhiSsiPb53q/cNyvtQnVtUK2j2jntcmzL40TErUG6Fm9V50F1OeCUj1qlf3qNmi3RaY7mNIgWRJrlE/pMX5RSFqFefVLucihAb8byrkNPvBQs1GHbMp751m1kYtV2fnealJirap9uFTPLRyjDqgi/UWCtw3pAu+z1ih0qv+cjupO+vfhY6rVvgcV9NuOX0iXW53tWes9OQM96wzy6VajWqsVecxKuu0ahSq9ig29O7MKjXDXqcV+11TZqtMqbNb3ZYl3AOancyvUWl29jga+t9YkBokcAW47o1C0C/7Yk8tQVpzDTXL5Y4YoFeZxBpcZ/Dye3bfTzJjf+aIHSHSizWYqm5FrtTraO61SBjtYNGVDgpsDmZpagJZe6e6Htg8omKqTxeUi1Onqai9A2CVA5LqoEpEPqX+N/gPzOIp62PQ/zu7LQNhkOMKUzb4MhEqtd78U96GTFgzZ9Oq/RNQ75FFCUGxx0y03wzjF1WnHVAm73G/wCLwhyIyVY55gAAACV0RVh0ZGF0ZTpjcmVhdGUAMjAyMC0wOS0xNFQxNzoxODo0Ni0wNDowMA3MS3sAAAAldEVYdGRhdGU6bW9kaWZ5ADIwMjAtMDktMTRUMTc6MTg6NDYtMDQ6MDB8kfPHAAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jnm+48GgAAAABJRU5ErkJggg=="),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "Lavender"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class PortalAPIHelperPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new PortalAPIHelperPluginControl();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public PortalAPIHelperPlugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }

        /// <summary>
        /// Event fired by CLR when an assembly reference fails to load
        /// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
        /// For example, a folder named Sample.XrmToolBox.MyPlugin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }
    }
}