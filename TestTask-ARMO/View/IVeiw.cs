namespace TestTask_ARMO
{
    interface IVeiw
    {   
        public string SearchQuery { get; set; }
        public string SearchDirectory { get; set; }

        public string CurrentSearchDirectory { get; set; }
        public TimeSpan SearchTime { get; set; }
        public int FilesFound { get; set; }
        public int FilesProcessed { get; set; }
        public bool IsOptionsEditable { set; }

        public event SearchStartHandler OnSearchStart;
        public event SearchPauseHandler OnSearchPause;

        public void AddFile(string filePath);
        public void RemoveAllFiles();
    }

    public delegate void SearchStartHandler();
    public delegate void SearchPauseHandler();
}
