using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace FindTheBulk.ClassLibrary
{
    public class FileScanner
    {
        private DirectoryInfo _rootDirectory;
        private bool _recurseDirectories;

        public EventHandler<FileFoundEventArgs> FileFoundEventHandler;
        public EventHandler<SearchFinishedEventArgs> SearchFinishedEventHandler;

        public FileScanner(DirectoryInfo rootDirectory, bool recurseDirectories)
        {
            _rootDirectory = rootDirectory;
            _recurseDirectories = recurseDirectories;
        }

        private void PushFileFoundEvent(FileFoundEventArgs e) => FileFoundEventHandler?.Invoke(this, e);
        private void PushSearchFinishedEvent(SearchFinishedEventArgs e) => SearchFinishedEventHandler?.Invoke(this, e);

        public async Task StartAsync()
        {
            if (_recurseDirectories)
            {
                await RecursiveFileScanAsync(_rootDirectory);
            }
            else
            {
                try
                {
                    foreach (var fileInfo in _rootDirectory.GetFiles("*", SearchOption.TopDirectoryOnly))
                        PushFileFoundEvent(new FileFoundEventArgs { File = fileInfo });
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }

            PushSearchFinishedEvent(new SearchFinishedEventArgs());
        }
        
        private async Task RecursiveFileScanAsync(DirectoryInfo lastDirectory)
        {
            try
            {
                foreach (var fileInfo in lastDirectory.GetFiles("*", SearchOption.TopDirectoryOnly))
                    PushFileFoundEvent(new FileFoundEventArgs { File = fileInfo });

                foreach (var dir in lastDirectory.GetDirectories())
                    await Task.Run(() => RecursiveFileScanAsync(dir));
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }

    public class SearchFinishedEventArgs : EventArgs
    {
    }

    public class FileFoundEventArgs : EventArgs
    {
        public FileInfo File { get; set; }
    }
}
