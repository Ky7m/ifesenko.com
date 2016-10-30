using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using PersonalWebApp.Extensions;
using PersonalWebApp.Services.Implementation.CloudStorageService.Model;
using PersonalWebApp.Services.Interfaces;
using PersonalWebApp.Settings;

namespace PersonalWebApp.Services.Implementation.CloudStorageService
{
    public sealed class CloudStorageService : IStorageService
    {
        private readonly AppSettings _appSettings;
        private readonly Lazy<CloudStorageAccount> _cloudStorageAccount;

        public CloudStorageService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _cloudStorageAccount = new Lazy<CloudStorageAccount>(() => CloudStorageAccount.Parse(_appSettings.StorageConnectionString));
        }

        public async Task<EventModel[]> RetrieveAllEventsAsync()
        {
            var events = PopulateEvents();
            return await Task.FromResult(events);
        }

        #region Events
        private static EventModel[] PopulateEvents() => Events;

        private static EventModel[] Events { get; } = {
            new EventModel
            {
                Title = "XP Days Ukraine",
                Link = "http://xpdays.com.ua",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Continuous Learning using Application Performance Management & Monitoring",
                    new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://doc.co/HvUtQo"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = "Kiev (Ukraine)",
                Date = new DateTime(2016, 11, 11)
            },
            new EventModel
            {
                Title = "Metrics Morning@Lohika",
                Link = "http://morning.lohika.com/news/metrics-morning",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Continuous Learning using Application Performance Management & Monitoring",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://doc.co/XZpwqc"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = "Lviv (Ukraine)",
                Date = new DateTime(2016, 11, 5)
            },
            new EventModel
            {
                Title = ".NET Framework Day 2016",
                Link = "https://frameworksdays.com/event/net-framework-day-2016",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Direction of C# as a High-Performance Language", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://doc.co/oRJMtu"] = CommonStrings.CollateralPowerpoint,
                        ["https://www.youtube.com/watch?v=rf6tZVog6LE"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = "Kiev (Ukraine)",
                Date = new DateTime(2016, 10, 22)
            },
            new EventModel
            {
                Title = "IT Weekend Ukraine 2016",
                Link = "https://ukraine.itweekend.ua/en/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Direction of C# as a High-Performance Language", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://doc.co/PDy6kY"] = CommonStrings.CollateralPowerpoint
                    })
                }),
                Location = "Kiev (Ukraine)",
                Date = new DateTime(2016, 9, 17)
            },
            new EventModel
            {
                Title = "AzureDay",
                Link = "http://azureday.net/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Workshop: Application Insights From The Ground Up", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://doc.co/vJQoPZ"] = CommonStrings.CollateralPowerpoint,
                        ["https://github.com/Ky7m/application-insights-workshop"] = CommonStrings.CollateralDemoCode
                    }),
                    new EventModelItem("Enabling DevTest in Microsoft Azure", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://doc.co/eJZL9s"] = CommonStrings.CollateralPowerpoint
                    }),
                    new EventModelItem("Channel 9: DevOps Video Lessons", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://channel9.msdn.com/Shows/DevOpsUA/Enabling-DevTest-in-Azure"] = CommonStrings.CollateralVideoRus,
                        ["https://channel9.msdn.com/Shows/DevOpsUA/Enabling-DevTest-in-Azure-demo"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = "Kiev (Ukraine)",
                Date = new DateTime(2016, 6, 18)
            },
            new EventModel
            {
                Title = "Application Innovation Strategy Briefing",
                Link = "https://www.microsoftevents.com/profile/form/index.cfm?PKformID=0x138568b7ed",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem(
                        "The Application Innovation Strategy Briefing is a mix of discussion, application architecture design, white boarding and hands-on labs. Your team will design the future of business with new functionalities that augment existing business applications and drive development and execution of new initiatives.")
                }),
                Location = "Cambridge, MA (USA)",
                Date = new DateTime(2016, 6, 9)
            },
            new EventModel
            {
                Title = "Modernize your business - a Modern Requirements and Microsoft Showcase",
                Link = "https://www.microsoftevents.com/profile/form/index.cfm?PKformID=0x148353d22c",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("OPTIMIZE your business by streamlining application lifecycle management process")
                }),
                Location = "San Francisco (USA)",
                Date = new DateTime(2016, 4, 28)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem(".NET Framework 461 & C# 67",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=auSzzmaUTWU"] = CommonStrings.CollateralVideoRus,
                            ["https://doc.co/RLDSpp"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2016, 4, 20)
            },
            new EventModel
            {
                Title = "Ciklum Lviv .Net Saturday",
                Link = "http://dou.ua/calendar/10488/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("The Present and Future of C# or I Know What You Did on Your Last Project",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://doc.co/ZVofdm"] = CommonStrings.CollateralPowerpoint,
                            ["https://ifesenko.blob.core.windows.net/assets/CiklumLvivDotNetSaturday.zip"] =
                            CommonStrings.CollateralDemoCode
                        })
                }),
                Location = "Lviv (Ukraine)",
                Date = new DateTime(2016, 4, 16)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Exception Handling - Advanced Tips & Tricks",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=_Gw_9KeJlbg"] = CommonStrings.CollateralVideoRus,
                            ["https://doc.co/NwAbvv"] = CommonStrings.CollateralPowerpoint,
                            ["https://ifesenko.blob.core.windows.net/assets/ExceptionHandlingAdvancedTipsAndTricks.zip"]
                            =
                            CommonStrings.CollateralDemoCode
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2016, 2, 24)
            },
            new EventModel
            {
                Title = "Web UI Community - SoftServe",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Web Development in Future",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=Om_NPxSuEf8"] = CommonStrings.CollateralVideoRus,
                            ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.CollateralSway
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2016, 2, 18)
            },
            new EventModel
            {
                Title = ".Net Morning@Lohika",
                Link = "http://morning.lohika.com/past-events/net-morninglohika",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=vUX2wFciHrs"] = CommonStrings.CollateralVideoRus,
                            ["https://doc.co/LEvVSM"] = CommonStrings.CollateralPowerpoint,
                            ["https://ifesenko.blob.core.windows.net/assets/EffectiveMemoryManagement.zip"] =
                            CommonStrings.CollateralDemoCode
                        })
                }),
                Location = "Lviv (Ukraine)",
                Date = new DateTime(2016, 2, 13)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("How C# 6.0 Simplifies Your Code",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=gUfrK1rGWB0"] = CommonStrings.CollateralVideoRus
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2016, 2, 10)
            },
            new EventModel
            {
                Title = "Enable Application Innovation Immersion",
                Link = "https://www.microsoftevents.com/profile/form/index.cfm?PKformID=0x40756dbc3",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem(
                        "The Enable Application Innovation Immersion is a mix of discussion, application architecture design, white boarding and hands-on labs. Your team will design the future of business with new functionalities that augment existing business applications and drive development and execution of new initiatives.")
                }),
                Location = "Dallas (USA)",
                Date = new DateTime(2016, 1, 26)
            },
            new EventModel
            {
                Title = "Tech#Skills_Day 1.2.",
                Link = "https://www.facebook.com/events/1650103461936579/1654170548196537/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://doc.co/LEvVSM"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = "Lviv (Ukraine)",
                Date = new DateTime(2015, 12, 21)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("C# GTD : Get Things Done in Bloody Enterprise",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=hOjNpHeMvvw"] = CommonStrings.CollateralVideoRus,
                            ["https://doc.co/2EFqPy"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 12, 16)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("C# : Hack yourself",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=WHwy0vDd8Tg"] = CommonStrings.CollateralVideoRus,
                            ["https://doc.co/13tWp4"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 11, 19)
            },
            new EventModel
            {
                Title = "IT Weekend Kharkiv",
                Link = "https://itweekend.ua/en/announcements/itw-kh-nov/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Web Development in Future",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.CollateralSway
                        })
                }),
                Location = "Kharkiv (Ukraine)",
                Date = new DateTime(2015, 11, 14)
            },
            new EventModel
            {
                Title = "IT Weekend Ivano-Frankivsk",
                Link = "https://itweekend.ua/en/announcements/itw-if-nov/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://doc.co/LEvVSM"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = "Ivano-Frankivsk (Ukraine)",
                Date = new DateTime(2015, 11, 7)
            },
            new EventModel
            {
                Title = "IT Weekend Rivne II",
                Link = "https://itweekend.ua/en/announcements/itw-rv-oct/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Web Development in Future",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.CollateralSway
                        })
                }),
                Location = "Rivne (Ukraine)",
                Date = new DateTime(2015, 10, 24)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Findings from .NET Microsoft Open Source Projects",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=m9MFhqq0g3k"] = CommonStrings.CollateralVideoRus,
                            ["https://doc.co/PU9YJa"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 10, 21)
            },
            new EventModel
            {
                Title = "Pacemaker: .NET - SoftServe",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=jpOgZBGL66g"] = CommonStrings.CollateralVideoRus,
                            ["https://doc.co/LEvVSM"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = "Chernivtsi (Ukraine)",
                Date = new DateTime(2015, 10, 17)
            },
            new EventModel
            {
                Title = "Pacemaker: WebUI - SoftServe",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Web Development in Future",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://doc.co/k8w18R"] = CommonStrings.CollateralSway
                        })
                }),
                Location = "Kyiv (Ukraine)",
                Date = new DateTime(2015, 9, 5)
            },
            new EventModel
            {
                Title = "Web UI Community - SoftServe",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Typescript",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=UqustygnUgk"] = CommonStrings.CollateralVideoRus,
                            ["https://doc.co/k8w18R"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 7, 24)
            },
            new EventModel
            {
                Title = "Development Process Community - SoftServe",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Git workflows for enterprise teams",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=RlJOX5bbh1U"] = CommonStrings.CollateralVideoRus,
                            ["https://doc.co/6XH7Kv"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 6, 2)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Making .Net Applications Faster",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=Rgvr1hynOmE"] = CommonStrings.CollateralVideoRus,
                            ["https://doc.co/8VaeeJ"] = CommonStrings.CollateralPowerpoint,
                            ["https://github.com/Ky7m/MakingDotNETApplicationsFaster"] =
                            CommonStrings.CollateralDemoCode
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 2, 11)
            }
        };

        #endregion Events
    }
}
