using System;
using System.Xml;

namespace XmlEditor
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Options options = new Options();

            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                ChangeXmlDocument(options);
                Environment.Exit(0);
            }
            else
                Environment.Exit(1);
        }

        private static void ChangeXmlDocument(Options options)
        {
            try
            {
                XmlNamespaceManager nsmgr = null;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(options.FilePath);

//                Console.WriteLine(xmlDoc.InnerXml);

                 if(!string.IsNullOrEmpty(options.Namespace))
                {
                    string[] strs = options.Namespace.Split(new [] {'='}, 2);

                    if(strs == null || strs.Length != 2)
                    {
                        Console.WriteLine("Namespace: '{0}' has wrong format.", options.Namespace);
                        Console.WriteLine(options.GetUsage());
                        Environment.Exit(1);
                    }

                    nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                    nsmgr.AddNamespace(strs[0], strs[1]);
                }

                var searchResult = nsmgr != null ? xmlDoc.SelectSingleNode(options.XPath, nsmgr) : xmlDoc.SelectSingleNode(options.XPath);

                if(searchResult == null)
                {
                    Console.WriteLine("Item '{0}' wasn't found.", options.XPath);
                    Environment.Exit(1);
                }

                if(searchResult is XmlAttribute)
                {
                    XmlAttribute attribute =  (XmlAttribute)searchResult;
                    attribute.Value = options.Value;
                }
                else
                {
                    XmlNode node = (XmlNode)searchResult;
                    node.InnerText = options.Value;
                }

                xmlDoc.Save(options.FilePath);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}