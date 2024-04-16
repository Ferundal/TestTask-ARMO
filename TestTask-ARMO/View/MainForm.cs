namespace TestTask_ARMO
{
    public partial class MainForm : Form, IVeiw
    {
        private TreeViewAsyncController _controller;

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            _controller = new TreeViewAsyncController(SearchResultsTreeView);
            SearchStartButton.Click += SearchStartButtonClick;
            SearchPauseButton.Click += SearchPauseButtonClick;
            IsOptionsEditable = true;
        }

        public string SearchDirectory
        {
            get => DirectoryLabelName.Text;
            set => DirectoryLabelName.Text = new string(value);
        }

        private TimeSpan _timespan;
        public TimeSpan SearchTime
        {
            get => _timespan;
            set
            {
                _timespan = value;
                SearchTimeValueLabel.Text = _timespan.ToString(@"hh\:mm\:ss\.fff");
            }
        }
        public string SearchQuery
        {
            get => SearchQueryTextBox.Text;
            set => SearchQueryTextBox.Text = new string(value);
        }

        public string CurrentSearchDirectory
        {
            get => CurrenSearchDirectory.Text;
            set => CurrenSearchDirectory.Text = new string(value);
        }

        private bool _isOptionsEditable = false;

        public bool IsOptionsEditable
        {
            set
            {
                if (_isOptionsEditable == value) return;

                _isOptionsEditable = value;

                SearchStartButton.Enabled = value;
                SearchPauseButton.Enabled = !value;
                SelectDirectoryButton.Enabled = value;
                SearchQueryTextBox.Enabled = value;
            }
        }

        private int _filesProcessed = 0;
        public int FilesProcessed
        {
            get => _filesProcessed;
            set
            {
                _filesProcessed = value;
                FilesTotalValueLabel.Text = _filesProcessed.ToString();
            }
        }

        private int _filesFound = 0;
        public int FilesFound
        {
            get => _filesFound;
            set
            {
                _filesFound = value;
                FilesFoundValueLabel.Text = _filesFound.ToString();
            }
        }

        public event SearchStartHandler OnSearchStart;
        public event SearchPauseHandler OnSearchPause;

        public void AddFile(string filePath)
        {
            _controller.AddFile(filePath);
        }


        private void SearchPauseButtonClick(object? sender, EventArgs e)
        {
            OnSearchPause?.Invoke();
        }

        private void SearchStartButtonClick(object? sender, EventArgs e)
        {
            OnSearchStart?.Invoke();
        }

        private void SelectDirectoryButtonClick(object sender, EventArgs e)
        {
            // Create a new instance of FolderBrowserDialog
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                // Set the description of the dialog
                folderBrowserDialog.Description = "Select a directory";

                // Initial directory
                folderBrowserDialog.SelectedPath = Application.StartupPath;

                // Show the dialog
                DialogResult result = folderBrowserDialog.ShowDialog();

                // Check the result of the dialog
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    DirectoryLabelName.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        public void RemoveAllFiles()
        {
            _controller.ClearTreeAndQueue();
        }
    }
}
