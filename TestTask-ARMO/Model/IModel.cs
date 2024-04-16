namespace TestTask_ARMO
{
    internal interface IModel
    {
        public bool IsSearchActive { get; set; }
        public string SearchQuery { get; set; }
        public string SearchDirectory { get; set; }

        public event SearchSuccessHandler OnSearchSuccess;
        public event SearchDirectoryChangedHandler OnSearchDirectoryChanged;
        public event FileProcessedHandler OnFileProcessed;
        public event SearchEndHandler OnSearchEnd;
        public event NewSearchStartHandler OnNewSearchStart;
        public event TimeUpdateHandler OnTimeUpdate;
    }

    public delegate void SearchSuccessHandler(string searchResult);
    public delegate void SearchDirectoryChangedHandler(string searchDirectory);
    public delegate void FileProcessedHandler();
    public delegate void SearchEndHandler();
    public delegate void NewSearchStartHandler();
    public delegate void TimeUpdateHandler(TimeSpan stopwatch);
}
