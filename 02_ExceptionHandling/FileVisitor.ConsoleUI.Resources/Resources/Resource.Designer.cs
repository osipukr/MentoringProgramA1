namespace FileVisitor.ConsoleUI.Resources.Resources
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource
    {
        private static global::System.Resources.ResourceManager resourceMan;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FileVisitor.ConsoleUI.Resources.Resources.Resource", typeof(Resource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture { get; set; }

        /// <summary>
        ///   Looks up a localized string similar to [DIRECTORY FINDED] .
        /// </summary>
        public static string Application_OnDirectoryFinded_Handler__DIRECTORY_FINDED__ => ResourceManager.GetString("Application_OnDirectoryFinded_Handler__DIRECTORY_FINDED__", Culture);

        /// <summary>
        ///   Looks up a localized string similar to [FILE FINDED] .
        /// </summary>
        public static string Application_OnFileFinded_Handler__FILE_FINDED__ => ResourceManager.GetString("Application_OnFileFinded_Handler__FILE_FINDED__", Culture);

        /// <summary>
        ///   Looks up a localized string similar to [FILTERED] .
        /// </summary>
        public static string Application_OnFilteredDirectoryFinded_Handler__FILTERED__ => ResourceManager.GetString("Application_OnFilteredDirectoryFinded_Handler__FILTERED__", Culture);

        /// <summary>
        ///   Looks up a localized string similar to [FILTERED] .
        /// </summary>
        public static string Application_OnFilteredFileFinded_Handler__FILTERED__ => ResourceManager.GetString("Application_OnFilteredFileFinded_Handler__FILTERED__", Culture);

        /// <summary>
        ///   Looks up a localized string similar to [SEARCH HAS FINISHED].
        /// </summary>
        public static string Application_OnFinished_Handler__SEARCH_HAS_FINISHED_ => ResourceManager.GetString("Application_OnFinished_Handler__SEARCH_HAS_FINISHED_", Culture);

        /// <summary>
        ///   Looks up a localized string similar to [SEARCH HAS STARTED].
        /// </summary>
        public static string Application_OnStarted_Handler__SEARCH_HAS_STARTED_ => ResourceManager.GetString("Application_OnStarted_Handler__SEARCH_HAS_STARTED_", Culture);

        /// <summary>
        ///   Looks up a localized string similar to File Visitor - console application - {0}.
        /// </summary>
        public static string Application_Run_File_Visitor___console_application_ => ResourceManager.GetString("Application_Run_File_Visitor___console_application_", Culture);
    }
}