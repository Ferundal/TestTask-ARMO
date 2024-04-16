using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Configuration;

namespace TestTask_ARMO
{
    internal class Model : IModel
    {
        private readonly string _filePath = "settings.txt";
        private readonly int _timeUpdateDelay = 100;
        private ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private Stopwatch _stopwatch = new Stopwatch();

        private bool _isActive = false;
        public bool IsSearchActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (_isActive == value) return;
                if (value)
                {
                    StartSearch();
                }
                else
                {
                    PauseSearch();
                }
            }
        }

        private string _searchQuery = String.Empty;
        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }
            set
            {
                if (value.Equals(_searchQuery)) return;

                StopSearch();
                _searchQuery = value;
            }
        }

        private string _searchDirectory = String.Empty;

        public string SearchDirectory
        {
            get
            {
                return _searchDirectory;
            }
            set
            {
                if (value.Equals(_searchDirectory)) return;

                StopSearch();
                _searchDirectory = value;
            }
        }

        public Model()
        {
            Load();
        }

        public event SearchSuccessHandler OnSearchSuccess;
        public event SearchEndHandler OnSearchEnd;
        public event FileProcessedHandler OnFileProcessed;
        public event SearchDirectoryChangedHandler OnSearchDirectoryChanged;
        public event NewSearchStartHandler OnNewSearchStart;
        public event TimeUpdateHandler OnTimeUpdate;

        private void StartSearch()
        {
            if (_pauseEvent.IsSet)
            {
                OnNewSearchStart?.Invoke();
                Task.Run(() => { AsynchTimeUpdate(_cancellationTokenSource.Token, _pauseEvent); });
                Task.Run(() => { AsynchSearch(_cancellationTokenSource.Token, _pauseEvent); });
            }
            else
            {
                _pauseEvent.Set();
            }
            _stopwatch.Start();
            _isActive = true;
            Save();
        }

        private void PauseSearch()
        {
            _pauseEvent.Reset();
            _stopwatch.Stop();
            _isActive = false;
        }

        private void StopSearch()
        {
            _cancellationTokenSource.Cancel();
            _pauseEvent.Set();
            _cancellationTokenSource = new CancellationTokenSource();
            _pauseEvent = new ManualResetEventSlim(true);
            _stopwatch.Reset();
            _isActive = false;
            OnSearchEnd?.Invoke();
        }

        private void AsynchSearch(
            CancellationToken cancellationToken, 
            ManualResetEventSlim pauseEvent)
        {
            try
            {
                // Initialize a stack to store subdirectories to be processed
                Stack<string> directoryPathsToProcess = new Stack<string>();
                directoryPathsToProcess.Push(SearchDirectory);

                while (directoryPathsToProcess.Count > 0)
                {
                    string currentDirectoryPath = directoryPathsToProcess.Pop();

                    if (cancellationToken.IsCancellationRequested) return;
                    OnSearchDirectoryChanged?.Invoke(currentDirectoryPath);
                    string[] filePaths;

                    // Process files in the current directory
                    try
                    {
                        filePaths = Directory.GetFiles(currentDirectoryPath);
                    }
                    catch (Exception exeption)
                    {
                        continue;
                    }
                    foreach (string filePath in filePaths)
                    {
                        pauseEvent.Wait();
                        if (Regex.IsMatch(Path.GetFileName(filePath), SearchQuery))
                        {
                            if (cancellationToken.IsCancellationRequested) return;
                            OnSearchSuccess?.Invoke(Path.GetRelativePath(SearchDirectory, filePath));
                        }
                        if (cancellationToken.IsCancellationRequested) return;
                        OnFileProcessed?.Invoke();
                    }

                    // Get subdirectories of the current directory
                    string[] subdirectoryEntries = Directory.GetDirectories(currentDirectoryPath);
                    foreach (string subdirectory in subdirectoryEntries)
                    {
                        // Add subdirectories to the stack for further processing
                        directoryPathsToProcess.Push(subdirectory);

                        // Process directories
                        Console.WriteLine($"Entering directory: {subdirectory}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message );
            }
            StopSearch();
        }

        private async Task AsynchTimeUpdate(
            CancellationToken cancellationToken,
            ManualResetEventSlim pauseEvent)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                pauseEvent.Wait();
                OnTimeUpdate?.Invoke(_stopwatch.Elapsed);
                await Task.Delay(_timeUpdateDelay);
            }
        }

        private void Load()
        {
            if (File.Exists(_filePath))
            {
                // Если файл существует, читаем из него данные
                string[] lines = File.ReadAllLines(_filePath);
                if (lines.Length >= 2)
                {
                    _searchQuery = lines[0];
                    _searchDirectory = lines[1];
                }
            }
            else
            {
                // Если файл не существует, оставляем значения по умолчанию
                _searchQuery = string.Empty;
                _searchDirectory = string.Empty;
            }
        }

        private void Save()
        {
            File.WriteAllLines(_filePath, new[] { _searchQuery, _searchDirectory });
        }
    }
}
