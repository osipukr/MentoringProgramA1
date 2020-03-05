using System.IO;

namespace FileVisitor.ConsoleUI.Settings
{
    public class UserSettings
    {
        public string DirectoryPath { get; set; }
        public string SearchPattern { get; set; }
        public SearchOption SearchOption { get; set; }
    }
}