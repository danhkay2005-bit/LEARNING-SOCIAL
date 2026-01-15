namespace WinForms.UserControls.Quiz
{
    partial class GhepCapEditorControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblTitle = new Label();
            dgvPairs = new DataGridView();
            lblHuongDan = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPairs).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(13, 56, 56);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(295, 31);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Thiết lập các cặp ghép nối";
            // 
            // dgvPairs
            // 
            dgvPairs.AllowUserToResizeRows = false;
            dgvPairs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPairs.BackgroundColor = Color.White;
            dgvPairs.BorderStyle = BorderStyle.Fixed3D;
            dgvPairs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(13, 56, 56);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(13, 56, 56);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvPairs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvPairs.ColumnHeadersHeight = 40;
            dgvPairs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(232, 240, 240);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvPairs.DefaultCellStyle = dataGridViewCellStyle2;
            dgvPairs.EnableHeadersVisualStyles = false;
            dgvPairs.GridColor = Color.FromArgb(224, 224, 224);
            dgvPairs.Location = new Point(25, 70);
            dgvPairs.MultiSelect = false;
            dgvPairs.Name = "dgvPairs";
            dgvPairs.RowHeadersVisible = false;
            dgvPairs.RowHeadersWidth = 51;
            dgvPairs.RowTemplate.Height = 35;
            dgvPairs.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvPairs.Size = new Size(750, 310);
            dgvPairs.TabIndex = 1;
            // 
            // lblHuongDan
            // 
            lblHuongDan.AutoSize = true;
            lblHuongDan.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblHuongDan.ForeColor = Color.DimGray;
            lblHuongDan.Location = new Point(25, 390);
            lblHuongDan.Name = "lblHuongDan";
            lblHuongDan.Size = new Size(590, 20);
            lblHuongDan.TabIndex = 2;
            lblHuongDan.Text = "* Gợi ý: Nhập nội dung vào hàng trống để thêm mới. Nhấn phím 'Delete' trên phím để xóa hàng.";
            // 
            // GhepCapEditorControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblHuongDan);
            Controls.Add(dgvPairs);
            Controls.Add(lblTitle);
            Name = "GhepCapEditorControl";
            Size = new Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)dgvPairs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvPairs;
        private Label lblHuongDan;
    }
}