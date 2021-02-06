using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FindTheBulk.ClassLibrary;
using static System.Console;

namespace FindTheBulk.ConsoleApp
{
    internal static class Program
    {
        private const string TITLE_BASE = "FindTheBulk -";
        private static ConcurrentDictionary<int, FileInfo> _files;
        private static FileInfo[] _sortedFiles;
        private static FileScanner _scanner;

        internal static void Main(string[] args)
        {
            _files = new ConcurrentDictionary<int, FileInfo>();

            Title = $"{TITLE_BASE} Find Large Files";

            var options = MainMenu();
            BeginFileScannerAsync(options).GetAwaiter().GetResult();

            Task.Delay(-1);
        }

        #region Event Subscribers
        
        private static void ScannerSearchFinished(object sender, SearchFinishedEventArgs e) => FileSelectionMenu();
        private static void PrintScannerFileFound(object sender, FileFoundEventArgs e)
        {
            _files.TryAdd(_files.Count, e.File);
            ShowFilesCount();
        }

        #endregion

        private static UserOptions MainMenu()
        {
            var options = new UserOptions();

            Write("Starting Directory: ");
            options.RootDirectory = new DirectoryInfo(ReadLine().Trim());

            Write("Search Directory Recursively? (Y/N): ");
            var input = ReadKey().KeyChar;

            options.SearchRecursively = char.ToUpper(input) == 'Y';

            return options;
        }

        private static void FileSelectionMenu()
        {
            while (true)
            {
                const int max_allowable = 10;

                Clear();

                _sortedFiles = _files.Values.OrderByDescending(f => f.Length).ToArray();

                if (_sortedFiles.Length <= max_allowable)
                {
                    for (int i = 0; i < _sortedFiles.Length; i++)
                    {
                        WriteLine(
                            $"[{i + 1}/{_sortedFiles.Length}] {_sortedFiles[i].FullName} ({_sortedFiles[i].GetFileSizeString()})");
                    }
                }
                else
                {
                    for (int i = 0; i < max_allowable; i++)
                    {
                        WriteLine(
                            $"[{i + 1}/{_sortedFiles.Length}] {_sortedFiles[i].FullName} ({_sortedFiles[i].GetFileSizeString()})");
                    }
                }

                bool valid;
                int index;

                do
                {
                    Write("Enter # of file to see details of: ");
                    valid = int.TryParse(ReadLine(), out index);
                    index--; // Fix index, since it's shown as key + 1

                    if (valid && (index < 0 || index >= _sortedFiles.Length))
                        valid = false;

                } while (!valid);

                ShowSpecificFileMenu(index);
            }
        }

        private static void ShowSpecificFileMenu(int index)
        {
            Clear();
            
            WriteLine(_sortedFiles[index].PrintProperties());

            Write("(O)pen file or any other other key to go back: ");
            var input = char.ToUpper(ReadKey().KeyChar);

            if (input != 'O') return;

            try
            {
                Process.Start("explorer.exe", _sortedFiles[index].DirectoryName);
            }
            catch (Win32Exception e)
            {
                WriteLine($"Whoops! That didn't work. {e.Message}");
                Debug.Write($"Failed to access {_sortedFiles[index].DirectoryName}");
            }
        }

        private static async Task BeginFileScannerAsync(UserOptions options)
        {
            _scanner = new FileScanner(options.RootDirectory, options.SearchRecursively);
            _scanner.FileFoundEventHandler += PrintScannerFileFound;
            _scanner.SearchFinishedEventHandler += ScannerSearchFinished;
            await _scanner.StartAsync();
        }
        
        private static void ShowFilesCount()
        {
            Title =$"{TITLE_BASE} Files Found: {_files.Count}";
        }
    }

    internal class UserOptions
    {
        internal DirectoryInfo RootDirectory { get; set; }
        internal bool SearchRecursively { get; set; }
    }
}
