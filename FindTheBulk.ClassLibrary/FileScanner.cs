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
        private string _searchPattern;

        public EventHandler<FileFoundEventArgs> FileFoundEventHandler;
        public EventHandler<SearchFinishedEventArgs> SearchFinishedEventHandler;

        public FileScanner(DirectoryInfo rootDirectory, bool recurseDirectories, string searchPattern = "*")
        {
            _rootDirectory = rootDirectory;
            _recurseDirectories = recurseDirectories;
            _searchPattern = searchPattern;
        }

        private void PushFileFoundEvent(FileFoundEventArgs e) => FileFoundEventHandler?.Invoke(this, e);
        private void PushSearchFinishedEvent(SearchFinishedEventArgs e) => SearchFinishedEventHandler?.Invoke(this, e);

        public async Task StartAsync()
        {
            await RecursiveFileScanAsync(_rootDirectory);
            PushSearchFinishedEvent(new SearchFinishedEventArgs());
        }
        
        private async Task RecursiveFileScanAsync(DirectoryInfo lastDirectory)
        {
            try
            {
                foreach (var fileInfo in lastDirectory.GetFiles(_searchPattern, SearchOption.TopDirectoryOnly))
                    PushFileFoundEvent(new FileFoundEventArgs { File = fileInfo });

                if (!_recurseDirectories)
                    return;

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
