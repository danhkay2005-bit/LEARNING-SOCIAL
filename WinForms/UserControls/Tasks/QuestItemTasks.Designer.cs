namespace WinForms.UserControls.Tasks
{
    partial class QuestItemTasks
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
            lblIcon = new Label();
            lblTen = new Label();
            lblMoTa = new Label();
            pbTienDo = new ProgressBar();
            lblTienDo = new Label();
            btnAction = new Button();
            SuspendLayout();
            // 
            // lblIcon
            // 
            lblIcon.AutoSize = true;
            lblIcon.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblIcon.Location = new Point(15, 20);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(42, 30);
            lblIcon.TabIndex = 0;
            lblIcon.Text = "📋";
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTen.ForeColor = Color.Navy;
            lblTen.Location = new Point(80, 15);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(41, 21);
            lblTen.TabIndex = 1;
            lblTen.Text = "Tên:";
            // 
            // lblMoTa
            // 
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new Point(80, 45);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(41, 15);
            lblMoTa.TabIndex = 2;
            lblMoTa.Text = "Mô tả:";
            // 
            // pbTienDo
            // 
            pbTienDo.Location = new Point(430, 30);
            pbTienDo.Name = "pbTienDo";
            pbTienDo.Size = new Size(150, 15);
            pbTienDo.TabIndex = 3;
            // 
            // lblTienDo
            // 
            lblTienDo.AutoSize = true;
            lblTienDo.Location = new Point(480, 50);
            lblTienDo.Name = "lblTienDo";
            lblTienDo.Size = new Size(40, 15);
            lblTienDo.TabIndex = 4;
            lblTienDo.Text = "\"0/10\"";
            // 
            // btnAction
            // 
            btnAction.BackColor = Color.Gold;
            btnAction.FlatStyle = FlatStyle.Flat;
            btnAction.Location = new Point(600, 30);
            btnAction.Name = "btnAction";
            btnAction.Size = new Size(100, 40);
            btnAction.TabIndex = 5;
            btnAction.Text = "NHẬN";
            btnAction.UseVisualStyleBackColor = false;
            btnAction.Click += btnAction_Click;
            // 
            // QuestItemTasks
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(btnAction);
            Controls.Add(lblTienDo);
            Controls.Add(pbTienDo);
            Controls.Add(lblMoTa);
            Controls.Add(lblTen);
            Controls.Add(lblIcon);
            Name = "QuestItemTasks";
            Size = new Size(748, 108);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblIcon;
        private Label lblTen;
        private Label lblMoTa;
        private ProgressBar pbTienDo;
        private Label lblTienDo;
        private Button btnAction;
    }
}
