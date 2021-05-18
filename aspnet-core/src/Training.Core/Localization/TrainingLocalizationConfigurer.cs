using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Training.Localization
{
    public static class TrainingLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(TrainingConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(TrainingLocalizationConfigurer).GetAssembly(),
                        "Training.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
