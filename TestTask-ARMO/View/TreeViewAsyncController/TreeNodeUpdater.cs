using System.Collections.Concurrent;

namespace TestTask_ARMO
{
    internal class TreeNodeUpdater
    {
        private TreeView _treeView;
        private ConcurrentQueue<KeyValuePair<TreeNodeCollection, TreeNode>> _updates = new();
        private CancellationToken _cancellationToken;
        static object _lockObject = new object();

        public bool HasUpdates { get => !_updates.IsEmpty; }

        public TreeNodeUpdater(TreeView treeView, CancellationToken cancellationToken)
        {
            _treeView = treeView;
            _cancellationToken = cancellationToken;
        }

        public void UpdateOnce()
        {
            lock (_lockObject)
            {
                if (!_updates.TryDequeue(out var update)) return;

                Action<TreeNode> addNodeAction = node => update.Key.Add(node);

                if (_cancellationToken.IsCancellationRequested) throw new OperationCanceledException();

                IAsyncResult result = _treeView.BeginInvoke(addNodeAction, update.Value);

                _treeView.EndInvoke(result);
            }
        }

        public void AddToUpdates(string filePath)
        {
            lock (_lockObject)
            {
                var currentNodes = _treeView.Nodes;
                TreeNode currentNode;
                bool isCurrentNodesInUpdate = false;

                while (TryGetTopLevelDirectoryName(filePath, out var newFilePath, out var topLevelDirectoryName))
                {
                    if (_cancellationToken.IsCancellationRequested) throw new OperationCanceledException();

                    filePath = newFilePath;
                    currentNode = null;

                    currentNode = FindDirectoryNodeInCurrentCollection(currentNodes, topLevelDirectoryName);

                    if (currentNode == null)
                    {
                        currentNode = FindNodeInUpdates(_updates, currentNodes, topLevelDirectoryName);

                        if (currentNode == null)
                        {
                            currentNode = CreateNewNodeAndAdd(currentNodes, _updates, topLevelDirectoryName, isCurrentNodesInUpdate);
                        }
                    }

                    currentNodes = currentNode.Nodes;
                }
                CreateNewNodeAndAdd(currentNodes, _updates, filePath, isCurrentNodesInUpdate);
            }
        }

        private bool TryGetTopLevelDirectoryName(string filePathInput, out string filePathOutput, out string topLevelDirectoryName)
        {
            topLevelDirectoryName = null;
            filePathOutput = null;

            try
            {
                // Get the first directory in the path
                int index = filePathInput.IndexOf(Path.DirectorySeparatorChar);
                if (index == -1)
                    index = filePathInput.IndexOf(Path.AltDirectorySeparatorChar);

                if (index != -1)
                {
                    // Extract the top-level directory name
                    topLevelDirectoryName = filePathInput.Substring(0, index);

                    // Remove the top-level directory from the original path
                    filePathOutput = filePathInput.Substring(index + 1);

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if there are errors while extracting the top-level directory name
                Console.WriteLine($"An error occurred while trying to get the top level directory name: {ex.Message}");
            }

            return false; // Return false if the path points only to a file
        }

        private TreeNode FindDirectoryNodeInCurrentCollection(TreeNodeCollection nodes, string text)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text.Equals(text))
                {
                    return node;
                }
            }
            return null;
        }

        private TreeNode FindNodeInUpdates(
            ConcurrentQueue<KeyValuePair<TreeNodeCollection, TreeNode>> updates,
            TreeNodeCollection nodes,
            string nodeText)
        {
            foreach (var update in updates)
            {
                if (update.Key != nodes) continue;
                if (update.Value.Text.Equals(nodeText))
                {
                    return update.Value;
                }
            }
            return null;
        }

        private TreeNode CreateNewNodeAndAdd(
            TreeNodeCollection nodes,
            ConcurrentQueue<KeyValuePair<TreeNodeCollection, TreeNode>> updates,
            string newNodeText, 
            bool isCurrentNodesInUpdate)
        {
            var newNode = new TreeNode(newNodeText);
            if (isCurrentNodesInUpdate)
            {
                nodes.Add(newNode);
            }
            else
            {
                updates.Enqueue(new KeyValuePair<TreeNodeCollection, TreeNode>(nodes, newNode));
            }
            return newNode;
        }
    }
}
