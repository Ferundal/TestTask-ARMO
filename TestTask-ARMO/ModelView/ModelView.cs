namespace TestTask_ARMO
{
    internal class ModelView<TView, TModel>
        where TView : IVeiw, new()
        where TModel : IModel, new()
    {
        private TView _veiw;
        public TView View
        {
            get => _veiw;
            private set
            {
                if (_veiw != null)
                {
                    _veiw.OnSearchStart -= StartSearch;
                    _veiw.OnSearchPause -= PauseSearch;
                }
                _veiw = value;
                _veiw.OnSearchStart += StartSearch;
                _veiw.OnSearchPause += PauseSearch;
                SetUpView();
            }
        }
        public TModel _model;
        public TModel Model
        {
            get => _model;
            private set
            {
                if (_model != null)
                {
                    _model.OnSearchSuccess -= ProcessSearchSuccess;
                    _model.OnFileProcessed -= ProcessFileProcessed;
                    _model.OnSearchDirectoryChanged -= ProcessSearchDirectoryChanged;
                    _model.OnSearchEnd -= ProcessSearchEnd;
                    _model.OnNewSearchStart -= ProcessNewSearchStart;
                    _model.OnTimeUpdate -= ProcessTimeUpdate;
                }
                _model = value;
                _model.OnSearchSuccess += ProcessSearchSuccess;
                _model.OnFileProcessed += ProcessFileProcessed;
                _model.OnSearchDirectoryChanged += ProcessSearchDirectoryChanged;
                _model.OnSearchEnd += ProcessSearchEnd;
                _model.OnNewSearchStart += ProcessNewSearchStart;
                _model.OnTimeUpdate += ProcessTimeUpdate;
            }
        }

        public ModelView()
        {
            Model = new TModel();
            View = new TView();
        }

        private void SetUpView()
        {
            View.SearchQuery = Model.SearchQuery;
            View.SearchDirectory = Model.SearchDirectory;
        }

        private void StartSearch()
        {
            Model.SearchQuery = View.SearchQuery;
            Model.SearchDirectory = View.SearchDirectory;
            View.IsOptionsEditable = false;
            Model.IsSearchActive = true;
        }

        private void PauseSearch()
        {
            Model.IsSearchActive = false;
            View.IsOptionsEditable = true;
        }

        private void ProcessSearchSuccess(string searchResult)
        {
            View.FilesFound += 1;
            View.AddFile(searchResult);
        }

        private void ProcessFileProcessed()
        {
            View.FilesProcessed += 1;
        }

        private void ProcessSearchDirectoryChanged(string searchDirectory)
        {
            View.CurrentSearchDirectory = searchDirectory;
        }

        private void ProcessSearchEnd()
        {
            View.IsOptionsEditable = true;
        }

        private void ProcessNewSearchStart()
        {
            View.SearchTime = TimeSpan.Zero;
            View.FilesProcessed = 0;
            View.FilesFound = 0;
            View.RemoveAllFiles();
        }

        private void ProcessTimeUpdate(TimeSpan timeSpan)
        {
            View.SearchTime = timeSpan;
        }
    }
}
