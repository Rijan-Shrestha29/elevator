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
            btnRequest0 = new Button();
            pnlFloor0Doors = new Panel();
            btnRequest1 = new Button();
            lblFloor1 = new Label();
            lblFloor0 = new Label();
            pnlFloor1Doors = new Panel();
            pnlElevator = new Panel();
            elevatorDoorLeft = new Panel();
            elevatorDoorRight = new Panel();
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
            pnlElevatorShaft.BackgroundImage = (Image)resources.GetObject("pnlElevatorShaft.BackgroundImage");
            pnlElevatorShaft.Controls.Add(btnRequest0);
            pnlElevatorShaft.Controls.Add(pnlFloor0Doors);
            pnlElevatorShaft.Controls.Add(btnRequest1);
            pnlElevatorShaft.Controls.Add(lblFloor1);
            pnlElevatorShaft.Controls.Add(lblFloor0);
            pnlElevatorShaft.Controls.Add(pnlFloor1Doors);
            pnlElevatorShaft.Controls.Add(pnlElevator);
            pnlElevatorShaft.Location = new Point(248, 50);
            pnlElevatorShaft.Name = "pnlElevatorShaft";
            pnlElevatorShaft.Size = new Size(260, 540);
            pnlElevatorShaft.TabIndex = 0;
            // 
            // btnRequest0
            // 
            btnRequest0.BackgroundImage = (Image)resources.GetObject("btnRequest0.BackgroundImage");
            btnRequest0.BackgroundImageLayout = ImageLayout.Zoom;
            btnRequest0.Location = new Point(216, 399);
            btnRequest0.Name = "btnRequest0";
            btnRequest0.Size = new Size(40, 40);
            btnRequest0.TabIndex = 3;
            btnRequest0.UseVisualStyleBackColor = true;
            // 
            // pnlFloor0Doors
            // 
            pnlFloor0Doors.BackgroundImage = (Image)resources.GetObject("pnlFloor0Doors.BackgroundImage");
            pnlFloor0Doors.BackgroundImageLayout = ImageLayout.Stretch;
            pnlFloor0Doors.Location = new Point(50, 337);
            pnlFloor0Doors.Name = "pnlFloor0Doors";
            pnlFloor0Doors.Size = new Size(160, 170);
            pnlFloor0Doors.TabIndex = 3;
            // 
            // btnRequest1
            // 
            btnRequest1.BackgroundImage = (Image)resources.GetObject("btnRequest1.BackgroundImage");
            btnRequest1.BackgroundImageLayout = ImageLayout.Zoom;
            btnRequest1.Location = new Point(216, 122);
            btnRequest1.Name = "btnRequest1";
            btnRequest1.Size = new Size(40, 40);
            btnRequest1.TabIndex = 2;
            btnRequest1.UseVisualStyleBackColor = true;
            // 
            // lblFloor1
            // 
            lblFloor1.AutoSize = true;
            lblFloor1.Location = new Point(102, 14);
            lblFloor1.Name = "lblFloor1";
            lblFloor1.Size = new Size(55, 20);
            lblFloor1.TabIndex = 0;
            lblFloor1.Text = "Floor 1";
            // 
            // lblFloor0
            // 
            lblFloor0.AutoSize = true;
            lblFloor0.Location = new Point(102, 273);
            lblFloor0.Name = "lblFloor0";
            lblFloor0.Size = new Size(55, 20);
            lblFloor0.TabIndex = 1;
            lblFloor0.Text = "Floor 0";
            // 
            // pnlFloor1Doors
            // 
            pnlFloor1Doors.BackgroundImage = (Image)resources.GetObject("pnlFloor1Doors.BackgroundImage");
            pnlFloor1Doors.BackgroundImageLayout = ImageLayout.Stretch;
            pnlFloor1Doors.Location = new Point(50, 60);
            pnlFloor1Doors.Name = "pnlFloor1Doors";
            pnlFloor1Doors.Size = new Size(160, 170);
            pnlFloor1Doors.TabIndex = 2;
            // 
            // pnlElevator
            // 
            pnlElevator.BackColor = SystemColors.ActiveCaptionText;
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
            elevatorDoorLeft.BackgroundImage = (Image)resources.GetObject("elevatorDoorLeft.BackgroundImage");
            elevatorDoorLeft.BackgroundImageLayout = ImageLayout.Stretch;
            elevatorDoorLeft.Location = new Point(0, 0);
            elevatorDoorLeft.Name = "elevatorDoorLeft";
            elevatorDoorLeft.Size = new Size(80, 170);
            elevatorDoorLeft.TabIndex = 0;
            // 
            // elevatorDoorRight
            // 
            elevatorDoorRight.BackgroundImage = (Image)resources.GetObject("elevatorDoorRight.BackgroundImage");
            elevatorDoorRight.BackgroundImageLayout = ImageLayout.Stretch;
            elevatorDoorRight.Location = new Point(80, 0);
            elevatorDoorRight.Name = "elevatorDoorRight";
            elevatorDoorRight.Size = new Size(80, 170);
            elevatorDoorRight.TabIndex = 1;
            // 
            // grpControlPanel
            // 
            grpControlPanel.BackColor = SystemColors.ActiveCaption;
            grpControlPanel.Controls.Add(lblDisplay);
            grpControlPanel.Controls.Add(btnFloor1);
            grpControlPanel.Controls.Add(btnFloor0);
            grpControlPanel.Location = new Point(560, 50);
            grpControlPanel.Name = "grpControlPanel";
            grpControlPanel.Size = new Size(300, 260);
            grpControlPanel.TabIndex = 1;
            grpControlPanel.TabStop = false;
            grpControlPanel.Text = "Control Panel";
            // 
            // lblDisplay
            // 
            lblDisplay.BackColor = SystemColors.ActiveCaptionText;
            lblDisplay.ForeColor = SystemColors.ControlLightLight;
            lblDisplay.Location = new Point(38, 23);
            lblDisplay.Name = "lblDisplay";
            lblDisplay.Size = new Size(220, 60);
            lblDisplay.TabIndex = 0;
            lblDisplay.Text = "Floor 0";
            lblDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnFloor1
            // 
            btnFloor1.BackColor = SystemColors.ActiveCaptionText;
            btnFloor1.ForeColor = SystemColors.Control;
            btnFloor1.Location = new Point(75, 112);
            btnFloor1.Name = "btnFloor1";
            btnFloor1.Size = new Size(60, 60);
            btnFloor1.TabIndex = 1;
            btnFloor1.Text = "1";
            btnFloor1.UseVisualStyleBackColor = false;
            // 
            // btnFloor0
            // 
            btnFloor0.BackColor = SystemColors.ActiveCaptionText;
            btnFloor0.ForeColor = SystemColors.Control;
            btnFloor0.Location = new Point(156, 112);
            btnFloor0.Name = "btnFloor0";
            btnFloor0.Size = new Size(60, 60);
            btnFloor0.TabIndex = 2;
            btnFloor0.Text = "0";
            btnFloor0.UseVisualStyleBackColor = false;
            // 
            // btnShowLog
            // 
            btnShowLog.Location = new Point(560, 340);
            btnShowLog.Name = "btnShowLog";
            btnShowLog.Size = new Size(300, 45);
            btnShowLog.TabIndex = 4;
            btnShowLog.Text = "📋 Show Logs";
            btnShowLog.UseVisualStyleBackColor = true;
            // 
            // dgvLogs
            // 
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvLogs.ColumnHeadersHeight = 35;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvLogs.DefaultCellStyle = dataGridViewCellStyle2;
            dgvLogs.Location = new Point(560, 400);
            dgvLogs.Name = "dgvLogs";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
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
            pnlBuildingBackground.BackgroundImage = (Image)resources.GetObject("pnlBuildingBackground.BackgroundImage");
            pnlBuildingBackground.BackgroundImageLayout = ImageLayout.Stretch;
            pnlBuildingBackground.Controls.Add(pnlElevatorShaft);
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
            ClientSize = new Size(980, 740);
            Controls.Add(grpControlPanel);
            Controls.Add(btnShowLog);
            Controls.Add(dgvLogs);
            Controls.Add(pnlBuildingBackground);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Elevator Control System";
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