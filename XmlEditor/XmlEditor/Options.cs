using System;
using CommandLine;
using CommandLine.Text;

namespace XmlEditor
{
    public class Options
    {
        [Option('p', "path", Required = true, HelpText = "Xml file path")]
        public string FilePath { get; set; }

        [Option('n', "namespace", HelpText = "Namespace, format: {key}={namespace}")]
        public string Namespace { get; set; }

//        [Option('n', "namespace-value", HelpText = "Namespace value")]
//        public string NamespaceValue { get; set; }

        [Option('x', "xpath", Required = true, HelpText = "XPath")]
        public string XPath { get; set; }

        [Option('v', "value", Required = true, HelpText = "Value of element")]
        public string Value { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}

