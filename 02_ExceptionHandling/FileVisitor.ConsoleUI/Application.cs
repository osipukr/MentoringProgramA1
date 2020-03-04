﻿using System;
using System.Globalization;
using System.IO;
using System.Threading;
using FileVisitor.ConsoleUI.Resources.Resources;
using FileVisitor.ConsoleUI.Settings;
using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Models.EventArgs;

namespace FileVisitor.ConsoleUI
{
    public class Application : IDisposable
    {
        private readonly IFileSystemVisitor _visitor;
        private readonly AppSettings _appSettings;
        private readonly UserSettings _userSettings;

        public Application(IFileSystemVisitor visitor, AppSettings appSettings, UserSettings userSettings)
        {
            _visitor = visitor;
            _appSettings = appSettings;
            _userSettings = userSettings;

            _visitor.Started += OnStarted_Handler;
            _visitor.Finished += OnFinished_Handler;
            _visitor.FileFinded += OnFileFinded_Handler;
            _visitor.DirectoryFinded += OnDirectoryFinded_Handler;
            _visitor.FilteredFileFinded += OnFilteredFileFinded_Handler;
            _visitor.FilteredDirectoryFinded += OnFilteredDirectoryFinded_Handler;

            CultureInfo.CurrentCulture = _appSettings.Language;
            CultureInfo.CurrentUICulture = _appSettings.Language;
        }

        public void Run()
        {
            Console.WriteLine(Resource.Application_Run_File_Visitor___console_application_, DateTime.UtcNow);

            var files = _visitor.Visit(_userSettings.DirectoryPath);

            foreach (var file in files)
            {
                Console.Write(file.Name);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void OnStarted_Handler(object sender, VisitorStartEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine(Resource.Application_OnStarted_Handler__SEARCH_HAS_STARTED_);
        }

        private void OnFinished_Handler(object sender, VisitorFinishEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(Resource.Application_OnFinished_Handler__SEARCH_HAS_FINISHED_);
        }

        private void OnFileFinded_Handler(object sender, VisitorItemFindedEventArgs<FileInfo> e)
        {
            Thread.Sleep(50);
            Console.WriteLine();
            Console.Write(Resource.Application_OnFileFinded_Handler__FILE_FINDED__);
        }

        private void OnDirectoryFinded_Handler(object sender, VisitorItemFindedEventArgs<DirectoryInfo> e)
        {
            Thread.Sleep(50);
            Console.WriteLine();
            Console.Write(Resource.Application_OnDirectoryFinded_Handler__DIRECTORY_FINDED__);
        }

        private void OnFilteredFileFinded_Handler(object sender, VisitorItemFindedEventArgs<FileInfo> e)
        {
            Console.Write(Resource.Application_OnFilteredFileFinded_Handler__FILTERED__);

            if (e.FindedItem.Name.EndsWith("Core.dll"))
            {
                //e.ActionType = ActionTypeEnum.SkipElement;
            }
        }

        private void OnFilteredDirectoryFinded_Handler(object sender, VisitorItemFindedEventArgs<DirectoryInfo> e)
        {
            Console.Write(Resource.Application_OnFilteredDirectoryFinded_Handler__FILTERED__);
        }

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _visitor.Started -= OnStarted_Handler;
                    _visitor.Finished -= OnFinished_Handler;
                    _visitor.FileFinded -= OnFileFinded_Handler;
                    _visitor.DirectoryFinded -= OnDirectoryFinded_Handler;
                    _visitor.FilteredFileFinded -= OnFilteredFileFinded_Handler;
                    _visitor.FilteredDirectoryFinded -= OnFilteredDirectoryFinded_Handler;
                }

                _isDisposed = true;
            }
        }

        ~Application()
        {
            Dispose(false);
        }
    }
}