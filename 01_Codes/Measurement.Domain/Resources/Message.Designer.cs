﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Measurement.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Message {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public Message() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Measurement.Domain.Resources.Message", typeof(Message).Assembly);
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
        ///   Looks up a localized string similar to Error change data !.
        /// </summary>
        public static string ErrorChangeData {
            get {
                return ResourceManager.GetString("ErrorChangeData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Acttion to document is [{0}] status only !.
        /// </summary>
        public static string MSG_ACTION_STATUS_ONLY {
            get {
                return ResourceManager.GetString("MSG_ACTION_STATUS_ONLY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Document has RunningNo does not remove !.
        /// </summary>
        public static string MSG_CANNOT_REMOVE_HAS_NO {
            get {
                return ResourceManager.GetString("MSG_CANNOT_REMOVE_HAS_NO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ProcessPlan not found !.
        /// </summary>
        public static string MSG_PROCESS_PLAN_NOT_FOUND {
            get {
                return ResourceManager.GetString("MSG_PROCESS_PLAN_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This ProcessPlan has been already used !.
        /// </summary>
        public static string MSG_PROCESS_PLAN_USED {
            get {
                return ResourceManager.GetString("MSG_PROCESS_PLAN_USED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SeqNo of weight not map SeqNo of process plan !.
        /// </summary>
        public static string MSG_SEQ_NOT_MAP {
            get {
                return ResourceManager.GetString("MSG_SEQ_NOT_MAP", resourceCulture);
            }
        }
    }
}
