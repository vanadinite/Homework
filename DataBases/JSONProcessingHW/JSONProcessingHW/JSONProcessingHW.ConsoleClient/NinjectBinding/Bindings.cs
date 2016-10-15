﻿using Ninject;
using Ninject.Modules;

using JSONProcessingHW.Logic;
using JSONProcessingHW.Logic.ConfigurationReaders;
using JSONProcessingHW.Logic.ConfigurationReaders.Contracts;
using JSONProcessingHW.Logic.DataServices;
using JSONProcessingHW.Logic.DataServices.Contracts;
using JSONProcessingHW.Logic.HtmlGenerator;
using JSONProcessingHW.Logic.HtmlGenerator.Contracts;
using JSONProcessingHW.Logic.Parsers;
using JSONProcessingHW.Logic.Parsers.Contracts;

namespace JSONProcessingHW.ConsoleClient.NinjectBinding
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IHtmlFileCreator>().To<HtmlFileCreator>();
            this.Bind<IHtmlGenerator>().To<HtmlGenerator>();
            this.Bind<IJsonParser>().To<JsonParser>();
            this.Bind<IXmlToJsonConverter>().To<XmlToJsonConverter>();
            this.Bind<IXmlDocumentProvider>().To<XmlDocumentProvider>();
            this.Bind<IDataService>().To<WebClientDataService>();
            this.Bind<IConfigurationReader>().To<AppConfigConfigurationReader>();
            this.Bind<IDataParser>()
                .To<DataParser>()
                .WithConstructorArgument("xmlDocumentProvider", ctx => ctx.Kernel.Get<IXmlDocumentProvider>())
                .WithConstructorArgument("xmlToJsonConverter", ctx => ctx.Kernel.Get<IXmlToJsonConverter>())
                .WithConstructorArgument("jsonParser", ctx => ctx.Kernel.Get<IJsonParser>())
                .WithConstructorArgument("htmlGenerator", ctx => ctx.Kernel.Get<IHtmlGenerator>())
                .WithConstructorArgument("htmlCreator", ctx => ctx.Kernel.Get<IHtmlFileCreator>());
        }
    }
}
