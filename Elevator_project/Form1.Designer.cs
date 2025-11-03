namespace Elevator_project
{
    partial class MainForm : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pnlElevatorShaft = new Panel();
            lblFloor1 = new Label();
            lblFloor0 = new Label();
            pnlFloor1Doors = new Panel();
            pnlFloor0Doors = new Panel();
            pnlElevator = new Panel();
            elevatorDoorLeft = new Panel();
            elevatorDoorRight = new Panel();
            btnRequest1 = new Button();
            btnRequest0 = new Button();
            grpControlPanel = new GroupBox();
            lblDisplay = new Label();
            btnFloor1 = new Button();
            btnFloor0 = new Button();
            btnShowLog = new Button();
            dgvLogs = new DataGridView();
            timerElevator = new System.Windows.Forms.Timer(components);
            pnlBuildingBackground = new Panel();
            pnlElevatorShaft.SuspendLayout();
            pnlElevator.SuspendLayout();
            grpControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            pnlBuildingBackground.SuspendLayout();
            SuspendLayout();
            // 
            // pnlElevatorShaft
            // 
            pnlElevatorShaft.BackColor = Color.Transparent;
            pnlElevatorShaft.BackgroundImageLayout = ImageLayout.Stretch;
            pnlElevatorShaft.BorderStyle = BorderStyle.FixedSingle;
            pnlElevatorShaft.Controls.Add(lblFloor1);
            pnlElevatorShaft.Controls.Add(lblFloor0);
            pnlElevatorShaft.Controls.Add(pnlFloor1Doors);
            pnlElevatorShaft.Controls.Add(pnlFloor0Doors);
            pnlElevatorShaft.Controls.Add(pnlElevator);
            pnlElevatorShaft.Location = new Point(260, 50);
            pnlElevatorShaft.Name = "pnlElevatorShaft";
            pnlElevatorShaft.Size = new Size(260, 540);
            pnlElevatorShaft.TabIndex = 0;
            // 
            // lblFloor1
            // 
            lblFloor1.AutoSize = true;
            lblFloor1.BackColor = Color.Transparent;
            lblFloor1.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblFloor1.ForeColor = Color.White;
            lblFloor1.Location = new Point(90, 20);
            lblFloor1.Name = "lblFloor1";
            lblFloor1.Size = new Size(84, 30);
            lblFloor1.TabIndex = 0;
            lblFloor1.Text = "Floor 1";
            // 
            // lblFloor0
            // 
            lblFloor0.AutoSize = true;
            lblFloor0.BackColor = Color.Transparent;
            lblFloor0.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblFloor0.ForeColor = Color.White;
            lblFloor0.Location = new Point(90, 280);
            lblFloor0.Name = "lblFloor0";
            lblFloor0.Size = new Size(84, 30);
            lblFloor0.TabIndex = 1;
            lblFloor0.Text = "Floor 0";
            // 
            // pnlFloor1Doors
            // 
            pnlFloor1Doors.BackColor = Color.Transparent;
            pnlFloor1Doors.BackgroundImage = (Image)resources.GetObject("pnlFloor1Doors.BackgroundImage");
            pnlFloor1Doors.BackgroundImageLayout = ImageLayout.Stretch;
            pnlFloor1Doors.Location = new Point(50, 60);
            pnlFloor1Doors.Name = "pnlFloor1Doors";
            pnlFloor1Doors.Size = new Size(160, 170);
            pnlFloor1Doors.TabIndex = 2;
            // 
            // pnlFloor0Doors
            // 
            pnlFloor0Doors.BackColor = Color.Transparent;
            pnlFloor0Doors.BackgroundImageLayout = ImageLayout.Stretch;
            pnlFloor0Doors.Location = new Point(50, 340);
            pnlFloor0Doors.Name = "pnlFloor0Doors";
            pnlFloor0Doors.Size = new Size(160, 170);
            pnlFloor0Doors.TabIndex = 3;
            // 
            // pnlElevator
            // 
            pnlElevator.BackColor = Color.Transparent;
            pnlElevator.BackgroundImageLayout = ImageLayout.Stretch;
            pnlElevator.Controls.Add(elevatorDoorLeft);
            pnlElevator.Controls.Add(elevatorDoorRight);
            pnlElevator.Location = new Point(50, 340);
            pnlElevator.Name = "pnlElevator";
            pnlElevator.Size = new Size(160, 170);
            pnlElevator.TabIndex = 4;
            // 
            // elevatorDoorLeft
            // 
            elevatorDoorLeft.BackColor = Color.Transparent;
            elevatorDoorLeft.BackgroundImageLayout = ImageLayout.Stretch;
            elevatorDoorLeft.Location = new Point(0, 0);
            elevatorDoorLeft.Name = "elevatorDoorLeft";
            elevatorDoorLeft.Size = new Size(80, 170);
            elevatorDoorLeft.TabIndex = 0;
            // 
            // elevatorDoorRight
            // 
            elevatorDoorRight.BackColor = Color.Transparent;
            elevatorDoorRight.BackgroundImageLayout = ImageLayout.Stretch;
            elevatorDoorRight.Location = new Point(80, 0);
            elevatorDoorRight.Name = "elevatorDoorRight";
            elevatorDoorRight.Size = new Size(80, 170);
            elevatorDoorRight.TabIndex = 1;
            // 
            // btnRequest1
            // 
            btnRequest1.BackColor = Color.FromArgb(80, 80, 120);
            btnRequest1.FlatAppearance.BorderSize = 0;
            btnRequest1.FlatStyle = FlatStyle.Flat;
            btnRequest1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnRequest1.ForeColor = Color.White;
            btnRequest1.Location = new Point(80, 100);
            btnRequest1.Name = "btnRequest1";
            btnRequest1.Size = new Size(120, 40);
            btnRequest1.TabIndex = 2;
            btnRequest1.Text = "↓  Call";
            btnRequest1.UseVisualStyleBackColor = false;
            // 
            // btnRequest0
            // 
            btnRequest0.BackColor = Color.FromArgb(80, 80, 120);
            btnRequest0.FlatAppearance.BorderSize = 0;
            btnRequest0.FlatStyle = FlatStyle.Flat;
            btnRequest0.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnRequest0.ForeColor = Color.White;
            btnRequest0.Location = new Point(80, 500);
            btnRequest0.Name = "btnRequest0";
            btnRequest0.Size = new Size(120, 40);
            btnRequest0.TabIndex = 3;
            btnRequest0.Text = "↑  Call";
            btnRequest0.UseVisualStyleBackColor = false;
            // 
            // grpControlPanel
            // 
            grpControlPanel.BackColor = Color.Transparent;
            grpControlPanel.BackgroundImageLayout = ImageLayout.Stretch;
            grpControlPanel.Controls.Add(lblDisplay);
            grpControlPanel.Controls.Add(btnFloor1);
            grpControlPanel.Controls.Add(btnFloor0);
            grpControlPanel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            grpControlPanel.ForeColor = Color.White;
            grpControlPanel.Location = new Point(560, 50);
            grpControlPanel.Name = "grpControlPanel";
            grpControlPanel.Size = new Size(300, 260);
            grpControlPanel.TabIndex = 1;
            grpControlPanel.TabStop = false;
            grpControlPanel.Text = "Control Panel";
            // 
            // lblDisplay
            // 
            lblDisplay.BackColor = Color.FromArgb(40, 40, 60);
            lblDisplay.Font = new Font("Consolas", 22F, FontStyle.Bold);
            lblDisplay.ForeColor = Color.FromArgb(100, 200, 255);
            lblDisplay.Location = new Point(40, 60);
            lblDisplay.Name = "lblDisplay";
            lblDisplay.Size = new Size(220, 60);
            lblDisplay.TabIndex = 0;
            lblDisplay.Text = "Floor 0";
            lblDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnFloor1
            // 
            btnFloor1.BackColor = Color.FromArgb(80, 80, 120);
            btnFloor1.FlatAppearance.BorderSize = 0;
            btnFloor1.FlatStyle = FlatStyle.Flat;
            btnFloor1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnFloor1.ForeColor = Color.White;
            btnFloor1.Location = new Point(70, 150);
            btnFloor1.Name = "btnFloor1";
            btnFloor1.Size = new Size(60, 60);
            btnFloor1.TabIndex = 1;
            btnFloor1.Text = "1";
            btnFloor1.UseVisualStyleBackColor = false;
            // 
            // btnFloor0
            // 
            btnFloor0.BackColor = Color.FromArgb(80, 80, 120);
            btnFloor0.FlatAppearance.BorderSize = 0;
            btnFloor0.FlatStyle = FlatStyle.Flat;
            btnFloor0.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnFloor0.ForeColor = Color.White;
            btnFloor0.Location = new Point(150, 150);
            btnFloor0.Name = "btnFloor0";
            btnFloor0.Size = new Size(60, 60);
            btnFloor0.TabIndex = 2;
            btnFloor0.Text = "0";
            btnFloor0.UseVisualStyleBackColor = false;
            // 
            // btnShowLog
            // 
            btnShowLog.BackColor = Color.FromArgb(100, 80, 140);
            btnShowLog.FlatAppearance.BorderSize = 0;
            btnShowLog.FlatStyle = FlatStyle.Flat;
            btnShowLog.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnShowLog.ForeColor = Color.White;
            btnShowLog.Location = new Point(560, 340);
            btnShowLog.Name = "btnShowLog";
            btnShowLog.Size = new Size(300, 45);
            btnShowLog.TabIndex = 4;
            btnShowLog.Text = "📋 Show Logs";
            btnShowLog.UseVisualStyleBackColor = false;
            // 
            // dgvLogs
            // 
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.BackgroundColor = Color.FromArgb(220, 220, 240);
            dgvLogs.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(80, 80, 120);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(60, 60, 100);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvLogs.ColumnHeadersHeight = 35;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(50, 50, 80);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(180, 180, 220);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(50, 50, 80);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvLogs.DefaultCellStyle = dataGridViewCellStyle2;
            dgvLogs.EnableHeadersVisualStyles = false;
            dgvLogs.Location = new Point(560, 400);
            dgvLogs.Name = "dgvLogs";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(240, 240, 255);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(50, 50, 80);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(180, 180, 220);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(50, 50, 80);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvLogs.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvLogs.RowHeadersWidth = 25;
            dgvLogs.Size = new Size(370, 180);
            dgvLogs.TabIndex = 5;
            // 
            // timerElevator
            // 
            timerElevator.Interval = 30;
            // 
            // pnlBuildingBackground
            // 
            pnlBuildingBackground.BackColor = Color.Transparent;
            pnlBuildingBackground.BackgroundImageLayout = ImageLayout.Stretch;
            pnlBuildingBackground.Controls.Add(pnlElevatorShaft);
            pnlBuildingBackground.Controls.Add(btnRequest1);
            pnlBuildingBackground.Controls.Add(btnRequest0);
            pnlBuildingBackground.Dock = DockStyle.Left;
            pnlBuildingBackground.Location = new Point(0, 0);
            pnlBuildingBackground.Name = "pnlBuildingBackground";
            pnlBuildingBackground.Size = new Size(540, 740);
            pnlBuildingBackground.TabIndex = 6;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(230, 230, 250);
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(980, 740);
            Controls.Add(grpControlPanel);
            Controls.Add(btnShowLog);
            Controls.Add(dgvLogs);
            Controls.Add(pnlBuildingBackground);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(50, 50, 80);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Elevator Control System - Modern Building";
            pnlElevatorShaft.ResumeLayout(false);
            pnlElevatorShaft.PerformLayout();
            pnlElevator.ResumeLayout(false);
            grpControlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            pnlBuildingBackground.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlElevatorShaft;
        private Panel pnlFloor0Doors;
        private Panel pnlFloor1Doors;
        private Label lblFloor1;
        private Button btnRequest1;
        private Label lblFloor0;
        private Button btnRequest0;
        private GroupBox grpControlPanel;
        private Label lblDisplay;
        private Button btnFloor1;
        private Button btnFloor0;
        private Button btnShowLog;
        private DataGridView dgvLogs;
        private System.Windows.Forms.Timer timerElevator;
        private Panel pnlElevator;
        private Panel elevatorDoorLeft;
        private Panel elevatorDoorRight;
        private Panel pnlBuildingBackground;
    }
}