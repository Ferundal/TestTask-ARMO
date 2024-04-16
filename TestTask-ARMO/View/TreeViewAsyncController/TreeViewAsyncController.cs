using System.Collections.Concurrent;
using System.Threading;
using TestTask_ARMO;

namespace TestTask_ARMO
{
    internal class TreeViewAsyncController
    {
        private const int _updateTimeDelay = 1000;
        private TreeView _treeView;
        private ConcurrentQueue<string> filePaths = new ();
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        private TreeNodeUpdater _treeUpdater;

        private bool IsProcessingQueue { get; set; } = false;
        public TreeViewAsyncController(TreeView treeView)
        {
            _treeView = treeView;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _treeUpdater = new TreeNodeUpdater(_treeView, _cancellationToken);
        }

        public void AddFile(string filePath)
        {
            filePaths.Enqueue(filePath);

            if (!IsProcessingQueue)
            {
                IsProcessingQueue = true;
                Task.Run(() => ProcessQueueAsync());
                Task.Run(() => Update(_treeUpdater));
            }
        }

        public void ClearTreeAndQueue()
        {
            // Отменить текущую операцию, если она запущена
            _cancellationTokenSource?.Cancel();

            // Очистить дерево
            _treeView.Nodes.Clear();

            // Очистить очередь и остановить операции добавления
            filePaths = new ConcurrentQueue<string>();

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _treeUpdater = new TreeNodeUpdater(_treeView, _cancellationToken);
            IsProcessingQueue = false;
        }

        private async Task ProcessQueueAsync()
        {

            while (filePaths.TryDequeue(out string filePath))
            {
                try
                {
                    _treeUpdater.AddToUpdates(filePath);
                }
                catch (OperationCanceledException)
                {
                    IsProcessingQueue = false;
                    return;
                }
            }
            IsProcessingQueue = false;
        }

        private async Task Update(TreeNodeUpdater updater)
        {
            while (true)
            {
                try
                {
                    updater.UpdateOnce();
                    await Task.Delay(_updateTimeDelay);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
    }
}
