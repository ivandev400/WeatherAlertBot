﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherAlertBot.Controllers.Commands {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CommandDescriptions {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CommandDescriptions() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WeatherAlertBot.Controllers.Commands.CommandDescriptions", typeof(CommandDescriptions).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enables morning weather notifications.
        /// </summary>
        public static string AnableNotificationEN {
            get {
                return ResourceManager.GetString("AnableNotificationEN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🔔 Активує ранкові сповіщення про погоду.
        /// </summary>
        public static string AnableNotificationUA {
            get {
                return ResourceManager.GetString("AnableNotificationUA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🌍 Змінює місто, погоду якого ви хочете відстежити. (Changes the location from where you want know the weather) 🌎.
        /// </summary>
        public static string ChangeLocationUA {
            get {
                return ResourceManager.GetString("ChangeLocationUA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🏜️Змінює час ранку, встановлюється для того, щоб на визначений час кожен ранок надсилати погоду на день. (Changesmorning time to be aware of weather every day automatically).
        /// </summary>
        public static string ChangeMorningTimeUA {
            get {
                return ResourceManager.GetString("ChangeMorningTimeUA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🌞Надсилає поточну погоду. (Sends the current weather).
        /// </summary>
        public static string CurrentWeatherUA {
            get {
                return ResourceManager.GetString("CurrentWeatherUA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🙈Дає опис всіх доступних команд. (Shows all awailable commands).
        /// </summary>
        public static string HelpCommandUA {
            get {
                return ResourceManager.GetString("HelpCommandUA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🗣️Встановлює мову. (Sets the language).
        /// </summary>
        public static string LanguageCommandUA {
            get {
                return ResourceManager.GetString("LanguageCommandUA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ⚙️Показує ваші налаштування. (Shows your settings).
        /// </summary>
        public static string SettingsUA {
            get {
                return ResourceManager.GetString("SettingsUA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🚀Запускає бота. (Starts the bot).
        /// </summary>
        public static string StartCommandUA {
            get {
                return ResourceManager.GetString("StartCommandUA", resourceCulture);
            }
        }
    }
}
