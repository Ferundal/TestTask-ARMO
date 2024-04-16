namespace TestTask_ARMO
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SelectDirectoryButton = new Button();
            DirectoryLabelName = new Label();
            SearchResultsTreeView = new TreeView();
            SearchStartButton = new Button();
            SearchQueryNameLabel = new Label();
            SearchQueryTextBox = new TextBox();
            FilesTotalTooltipLabel = new Label();
            FilesFoundTooltipLabel = new Label();
            FilesTotalValueLabel = new Label();
            FilesFoundValueLabel = new Label();
            SearchTimeValueLabel = new Label();
            SearchTimeTooltipLabel = new Label();
            SearchPauseButton = new Button();
            CurrenSearchDirectory = new Label();
            SuspendLayout();
            // 
            // SelectDirectoryButton
            // 
            SelectDirectoryButton.Location = new Point(12, 12);
            SelectDirectoryButton.Name = "SelectDirectoryButton";
            SelectDirectoryButton.Size = new Size(170, 36);
            SelectDirectoryButton.TabIndex = 0;
            SelectDirectoryButton.Text = "Select Directory";
            SelectDirectoryButton.UseVisualStyleBackColor = true;
            SelectDirectoryButton.Click += SelectDirectoryButtonClick;
            // 
            // DirectoryLabelName
            // 
            DirectoryLabelName.AutoSize = true;
            DirectoryLabelName.Location = new Point(197, 18);
            DirectoryLabelName.Name = "DirectoryLabelName";
            DirectoryLabelName.Size = new Size(63, 25);
            DirectoryLabelName.TabIndex = 1;
            DirectoryLabelName.Text = "empty";
            // 
            // SearchResultsTreeView
            // 
            SearchResultsTreeView.Location = new Point(12, 223);
            SearchResultsTreeView.Name = "SearchResultsTreeView";
            SearchResultsTreeView.Size = new Size(784, 315);
            SearchResultsTreeView.TabIndex = 2;
            // 
            // SearchStartButton
            // 
            SearchStartButton.Location = new Point(12, 85);
            SearchStartButton.Name = "SearchStartButton";
            SearchStartButton.Size = new Size(170, 36);
            SearchStartButton.TabIndex = 3;
            SearchStartButton.Text = "Search";
            SearchStartButton.UseVisualStyleBackColor = true;
            // 
            // SearchQueryNameLabel
            // 
            SearchQueryNameLabel.AutoSize = true;
            SearchQueryNameLabel.Location = new Point(65, 51);
            SearchQueryNameLabel.Name = "SearchQueryNameLabel";
            SearchQueryNameLabel.Size = new Size(117, 25);
            SearchQueryNameLabel.TabIndex = 4;
            SearchQueryNameLabel.Text = "Search Query";
            // 
            // SearchQueryTextBox
            // 
            SearchQueryTextBox.Location = new Point(188, 48);
            SearchQueryTextBox.Name = "SearchQueryTextBox";
            SearchQueryTextBox.Size = new Size(248, 31);
            SearchQueryTextBox.TabIndex = 5;
            // 
            // FilesTotalTooltipLabel
            // 
            FilesTotalTooltipLabel.AutoSize = true;
            FilesTotalTooltipLabel.Location = new Point(29, 124);
            FilesTotalTooltipLabel.Name = "FilesTotalTooltipLabel";
            FilesTotalTooltipLabel.Size = new Size(91, 25);
            FilesTotalTooltipLabel.TabIndex = 6;
            FilesTotalTooltipLabel.Text = "Files total:";
            // 
            // FilesFoundTooltipLabel
            // 
            FilesFoundTooltipLabel.AutoSize = true;
            FilesFoundTooltipLabel.Location = new Point(17, 149);
            FilesFoundTooltipLabel.Name = "FilesFoundTooltipLabel";
            FilesFoundTooltipLabel.Size = new Size(103, 25);
            FilesFoundTooltipLabel.TabIndex = 7;
            FilesFoundTooltipLabel.Text = "Files found:";
            // 
            // FilesTotalValueLabel
            // 
            FilesTotalValueLabel.AutoSize = true;
            FilesTotalValueLabel.Location = new Point(126, 124);
            FilesTotalValueLabel.Name = "FilesTotalValueLabel";
            FilesTotalValueLabel.Size = new Size(22, 25);
            FilesTotalValueLabel.TabIndex = 8;
            FilesTotalValueLabel.Text = "0";
            // 
            // FilesFoundValueLabel
            // 
            FilesFoundValueLabel.AutoSize = true;
            FilesFoundValueLabel.Location = new Point(126, 149);
            FilesFoundValueLabel.Name = "FilesFoundValueLabel";
            FilesFoundValueLabel.Size = new Size(22, 25);
            FilesFoundValueLabel.TabIndex = 9;
            FilesFoundValueLabel.Text = "0";
            // 
            // SearchTimeValueLabel
            // 
            SearchTimeValueLabel.AutoSize = true;
            SearchTimeValueLabel.Location = new Point(126, 174);
            SearchTimeValueLabel.Name = "SearchTimeValueLabel";
            SearchTimeValueLabel.Size = new Size(22, 25);
            SearchTimeValueLabel.TabIndex = 10;
            SearchTimeValueLabel.Text = "0";
            // 
            // SearchTimeTooltipLabel
            // 
            SearchTimeTooltipLabel.AutoSize = true;
            SearchTimeTooltipLabel.Location = new Point(12, 174);
            SearchTimeTooltipLabel.Name = "SearchTimeTooltipLabel";
            SearchTimeTooltipLabel.Size = new Size(108, 25);
            SearchTimeTooltipLabel.TabIndex = 11;
            SearchTimeTooltipLabel.Text = "Search time:";
            // 
            // SearchPauseButton
            // 
            SearchPauseButton.Location = new Point(188, 85);
            SearchPauseButton.Name = "SearchPauseButton";
            SearchPauseButton.Size = new Size(170, 36);
            SearchPauseButton.TabIndex = 12;
            SearchPauseButton.Text = "Pause";
            SearchPauseButton.UseVisualStyleBackColor = true;
            // 
            // CurrenSearchDirectory
            // 
            CurrenSearchDirectory.AutoSize = true;
            CurrenSearchDirectory.Location = new Point(364, 91);
            CurrenSearchDirectory.Name = "CurrenSearchDirectory";
            CurrenSearchDirectory.Size = new Size(0, 25);
            CurrenSearchDirectory.TabIndex = 13;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 662);
            Controls.Add(CurrenSearchDirectory);
            Controls.Add(SearchPauseButton);
            Controls.Add(SearchTimeTooltipLabel);
            Controls.Add(SearchTimeValueLabel);
            Controls.Add(FilesFoundValueLabel);
            Controls.Add(FilesTotalValueLabel);
            Controls.Add(FilesFoundTooltipLabel);
            Controls.Add(FilesTotalTooltipLabel);
            Controls.Add(SearchQueryTextBox);
            Controls.Add(SearchQueryNameLabel);
            Controls.Add(SearchStartButton);
            Controls.Add(SearchResultsTreeView);
            Controls.Add(DirectoryLabelName);
            Controls.Add(SelectDirectoryButton);
            Font = new Font("Segoe UI", 12.2264156F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "FileSearch";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SelectDirectoryButton;
        private Label DirectoryLabelName;
        private TreeView SearchResultsTreeView;
        private Button SearchStartButton;
        private Label SearchQueryNameLabel;
        private TextBox SearchQueryTextBox;
        private Label FilesTotalTooltipLabel;
        private Label FilesFoundTooltipLabel;
        private Label FilesTotalValueLabel;
        private Label FilesFoundValueLabel;
        private Label SearchTimeValueLabel;
        private Label SearchTimeTooltipLabel;
        private Button SearchPauseButton;
        private Label CurrenSearchDirectory;
    }
}
