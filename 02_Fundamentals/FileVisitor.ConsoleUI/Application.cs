using System;
using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Models.EventArgs;
using FileVisitor.Core.Services;

namespace FileVisitor.ConsoleUI
{
    public class Application
    {
        private readonly IVisitor _visitor;

        public Application()
        {
            _visitor = new FileSystemVisitor(@"E:\Programming\C-sharp\MentoringProgramA1\02_Fundamentals",
                new FileSystemVisitorOptions
                {
                    Filter = file => true
                });

            _visitor.Started += StartedSearching_Handler;
            _visitor.Finished += FinishedSearching_Handler;
        }

        private void StartedSearching_Handler(object sender, VisitorStartEventArgs e)
        {
            Console.WriteLine("Print");
        }

        private void FinishedSearching_Handler(object sender, VisitorFinishEventArgs e)
        {
            Console.WriteLine("Print finish");
        }

        public void Run()
        {
            var files = _visitor.Visit();

            foreach (var file in files)
            {
                Console.WriteLine(file.FullName);
            }
        }
    }
}