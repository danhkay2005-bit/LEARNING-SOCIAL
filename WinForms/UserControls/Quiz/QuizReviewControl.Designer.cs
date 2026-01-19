namespace WinForms.UserControls.Quiz
{
    partial class QuizReviewControl
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

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblTitle = new Label();
            dgvReview = new DataGridView();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReview).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(800, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "CHI TIẾT BÀI LÀM";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvReview
            // 
            dgvReview.AllowUserToAddRows = false;
            dgvReview.AllowUserToDeleteRows = false;
            dgvReview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReview.BackgroundColor = Color.FromArgb(25, 45, 50);
            dgvReview.BorderStyle = BorderStyle.None;
            dgvReview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvReview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(40, 70, 80);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(40, 70, 80);
            dgvReview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvReview.ColumnHeadersHeight = 50;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(25, 45, 50);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(40, 70, 80);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvReview.DefaultCellStyle = dataGridViewCellStyle2;
            dgvReview.EnableHeadersVisualStyles = false;
            dgvReview.GridColor = Color.FromArgb(45, 65, 70);
            dgvReview.Location = new Point(20, 70);
            dgvReview.Name = "dgvReview";
            dgvReview.ReadOnly = true;
            dgvReview.RowHeadersVisible = false;
            dgvReview.RowTemplate.Height = 45;
            dgvReview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReview.Size = new Size(760, 420);
            dgvReview.TabIndex = 1;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(193, 225, 127);
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnBack.ForeColor = Color.FromArgb(13, 56, 56);
            btnBack.Location = new Point(300, 510);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 50);
            btnBack.TabIndex = 2;
            btnBack.Text = "XONG";
            btnBack.UseVisualStyleBackColor = false;
            // 
            // QuizReviewControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 56, 56);
            Controls.Add(btnBack);
            Controls.Add(dgvReview);
            Controls.Add(lblTitle);
            Name = "QuizReviewControl";
            Size = new Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)dgvReview).EndInit();
            ResumeLayout(false);
        }
        private Label lblTitle;
        private DataGridView dgvReview;
        private Button btnBack;
    }
}
