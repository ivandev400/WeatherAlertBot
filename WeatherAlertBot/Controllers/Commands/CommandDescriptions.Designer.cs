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
        ///   Looks up a localized string similar to Дозволяє відправляти погоду на встановлений час зранку.
        /// </summary>
        public static string AnableNotification {
            get {
                return ResourceManager.GetString("AnableNotification", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🌍 Змінює місто, погоду якого ви хочете відстежити. (Set the location from where you want know the weather) 🌎.
        /// </summary>
        public static string ChangeLocation {
            get {
                return ResourceManager.GetString("ChangeLocation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🏜️Змінює час ранку, встановлюється для того, щоб на визначений час кожен ранок надсилати погоду на день. Set morning time to be aware of weather every day automatically. .
        /// </summary>
        public static string ChangeMorningTime {
            get {
                return ResourceManager.GetString("ChangeMorningTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🌞Надсилає погоду. (Return the weather).
        /// </summary>
        public static string CurrentWeather {
            get {
                return ResourceManager.GetString("CurrentWeather", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🙈Дає опис всіх доступних команд. (Show all awailable commands).
        /// </summary>
        public static string HelpCommand {
            get {
                return ResourceManager.GetString("HelpCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Встановлює мову. Set the language.
        /// </summary>
        public static string LanguageCommand {
            get {
                return ResourceManager.GetString("LanguageCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ⚙️Показує ваші налаштування. (Show your settings).
        /// </summary>
        public static string Settings {
            get {
                return ResourceManager.GetString("Settings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 🚀Запускає бота. (Lauching the bot).
        /// </summary>
        public static string StartCommand {
            get {
                return ResourceManager.GetString("StartCommand", resourceCulture);
            }
        }
    }
}
