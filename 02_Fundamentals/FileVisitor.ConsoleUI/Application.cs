using System;
using System.IO;
using System.Threading;
using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Models.EventArgs;
using FileVisitor.Core.Services;

namespace FileVisitor.ConsoleUI
{
    public class Application
    {
        private readonly IFileSystemVisitor _visitor;

        public Application()
        {
            var path = Environment.CurrentDirectory;
            var visitorOptions = new FileSystemVisitorOptions
            {
                //SearchFilter = _ => true,
                SearchFilter = file => file.Name.Contains("Core"),
                SearchPattern = "*.*",
                SearchOption = SearchOption.AllDirectories
            };

            _visitor = new FileSystemVisitor(path, visitorOptions);

            _visitor.Started += StartedSearching_Handler;
            _visitor.Finished += FinishedSearching_Handler;
            _visitor.FileFinded += FileFinded_Handler;
            _visitor.DirectoryFinded += DirectoryFinded_Handler;
            _visitor.FilteredFileFinded += FilteredFileFinded_Handler;
            _visitor.FilteredDirectoryFinded += FilteredDirectoryFinded_Handler;
        }

        public void Run()
        {
            var files = _visitor.Visit();

            foreach (var file in files)
            {
                Console.WriteLine(file.Name);
                Console.WriteLine();
            }
        }

        private void StartedSearching_Handler(object sender, VisitorStartEventArgs e)
        {
            Console.WriteLine("[SEARCH HAS STARTED]");
        }

        private void FinishedSearching_Handler(object sender, VisitorFinishEventArgs e)
        {
            Console.WriteLine("[SEARCH HAS FINISHED]");
        }

        private void FileFinded_Handler(object sender, VisitorItemFindedEventArgs<FileInfo> e)
        {
            Thread.Sleep(50);
            Console.WriteLine("[FILE FINDED]");
        }

        private void DirectoryFinded_Handler(object sender, VisitorItemFindedEventArgs<DirectoryInfo> e)
        {
            Thread.Sleep(50);
            Console.WriteLine("[DIRECTORY FINDED]");
        }

        private void FilteredFileFinded_Handler(object sender, VisitorItemFindedEventArgs<FileInfo> e)
        {
            Console.Write("[FILTERED] ");

            if (e.FindedItem.Name.EndsWith("Core.dll"))
            {
                // uncomment this line
                //e.ActionType = ActionTypeEnum.SkipElement;
            }
        }

        private void FilteredDirectoryFinded_Handler(object sender, VisitorItemFindedEventArgs<DirectoryInfo> e)
        {
            Console.Write("[FILTERED] ");
        }
    }
}