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
            pnlElevator = new Panel();
            elevatorDoorRight = new Panel();
            elevatorDoorLeft = new Panel();
            btnRequest0 = new Button();
            pnlFloor0Doors = new Panel();
            floor0DoorRight = new Panel();
            floor0DoorLeft = new Panel();
            btnRequest1 = new Button();
            lblFloor1 = new Label();
            lblFloor0 = new Label();
            pnlFloor1Doors = new Panel();
            floor1DoorRight = new Panel();
            floor1DoorLeft = new Panel();
            grpControlPanel = new GroupBox();
            btnCloseDoors = new Button();
            btnOpenDoors = new Button();
            lblDisplay = new Label();
            btnFloor1 = new Button();
            btnFloor0 = new Button();
            btnShowLog = new Button();
            dgvLogs = new DataGridView();
            timerElevator = new System.Windows.Forms.Timer(components);
            pnlBuildingBackground = new Panel();
            pnlElevatorShaft.SuspendLayout();
            pnlElevator.SuspendLayout();
            pnlFloor0Doors.SuspendLayout();
            pnlFloor1Doors.SuspendLayout();
            grpControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            pnlBuildingBackground.SuspendLayout();
            SuspendLayout();
            // 
            // pnlElevatorShaft
            // 
            pnlElevatorShaft.BackgroundImage = (Image)resources.GetObject("pnlElevatorShaft.BackgroundImage");
            pnlElevatorShaft.Controls.Add(pnlElevator);
            pnlElevatorShaft.Controls.Add(btnRequest0);
            pnlElevatorShaft.Controls.Add(pnlFloor0Doors);
            pnlElevatorShaft.Controls.Add(btnRequest1);
            pnlElevatorShaft.Controls.Add(lblFloor1);
            pnlElevatorShaft.Controls.Add(lblFloor0);
            pnlElevatorShaft.Controls.Add(pnlFloor1Doors);
            pnlElevatorShaft.Location = new Point(248, 80);
            pnlElevatorShaft.Name = "pnlElevatorShaft";
            pnlElevatorShaft.Size = new Size(260, 540);
            pnlElevatorShaft.TabIndex = 0;
            // 
            // pnlElevator
            // 
            pnlElevator.BackColor = Color.Silver;
            pnlElevator.BackgroundImageLayout = ImageLayout.Stretch;
            pnlElevator.Controls.Add(elevatorDoorRight);
            pnlElevator.Controls.Add(elevatorDoorLeft);
            pnlElevator.Location = new Point(50, 340);
            pnlElevator.Name = "pnlElevator";
            pnlElevator.Size = new Size(160, 170);
            pnlElevator.TabIndex = 5;
            // 
            // elevatorDoorRight
            // 
            elevatorDoorRight.BackColor = Color.Transparent;
            elevatorDoorRight.BackgroundImage = (Image)resources.GetObject("elevatorDoorRight.BackgroundImage");
            elevatorDoorRight.BackgroundImageLayout = ImageLayout.Stretch;
            elevatorDoorRight.Location = new Point(80, 0);
            elevatorDoorRight.Name = "elevatorDoorRight";
            elevatorDoorRight.Size = new Size(80, 170);
            elevatorDoorRight.TabIndex = 1;
            // 
            // elevatorDoorLeft
            // 
            elevatorDoorLeft.BackColor = Color.Transparent;
            elevatorDoorLeft.BackgroundImage = (Image)resources.GetObject("elevatorDoorLeft.BackgroundImage");
            elevatorDoorLeft.BackgroundImageLayout = ImageLayout.Stretch;
            elevatorDoorLeft.Location = new Point(0, 0);
            elevatorDoorLeft.Name = "elevatorDoorLeft";
            elevatorDoorLeft.Size = new Size(80, 170);
            elevatorDoorLeft.TabIndex = 0;
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
            pnlFloor0Doors.Controls.Add(floor0DoorRight);
            pnlFloor0Doors.Controls.Add(floor0DoorLeft);
            pnlFloor0Doors.Location = new Point(50, 340);
            pnlFloor0Doors.Name = "pnlFloor0Doors";
            pnlFloor0Doors.Size = new Size(160, 170);
            pnlFloor0Doors.TabIndex = 3;
            // 
            // floor0DoorRight
            // 
            floor0DoorRight.BackgroundImage = (Image)resources.GetObject("floor0DoorRight.BackgroundImage");
            floor0DoorRight.BackgroundImageLayout = ImageLayout.Stretch;
            floor0DoorRight.Location = new Point(80, 0);
            floor0DoorRight.Name = "floor0DoorRight";
            floor0DoorRight.Size = new Size(80, 170);
            floor0DoorRight.TabIndex = 1;
            // 
            // floor0DoorLeft
            // 
            floor0DoorLeft.BackgroundImageLayout = ImageLayout.Stretch;
            floor0DoorLeft.Location = new Point(0, 0);
            floor0DoorLeft.Name = "floor0DoorLeft";
            floor0DoorLeft.Size = new Size(80, 170);
            floor0DoorLeft.TabIndex = 0;
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
            lblFloor1.BackColor = Color.Transparent;
            lblFloor1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFloor1.ForeColor = Color.White;
            lblFloor1.Location = new Point(102, 30);
            lblFloor1.Name = "lblFloor1";
            lblFloor1.Size = new Size(66, 23);
            lblFloor1.TabIndex = 0;
            lblFloor1.Text = "Floor 1";
            // 
            // lblFloor0
            // 
            lblFloor0.AutoSize = true;
            lblFloor0.BackColor = Color.Transparent;
            lblFloor0.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFloor0.ForeColor = Color.White;
            lblFloor0.Location = new Point(102, 290);
            lblFloor0.Name = "lblFloor0";
            lblFloor0.Size = new Size(66, 23);
            lblFloor0.TabIndex = 1;
            lblFloor0.Text = "Floor 0";
            // 
            // pnlFloor1Doors
            // 
            pnlFloor1Doors.BackgroundImage = (Image)resources.GetObject("pnlFloor1Doors.BackgroundImage");
            pnlFloor1Doors.BackgroundImageLayout = ImageLayout.Stretch;
            pnlFloor1Doors.Controls.Add(floor1DoorRight);
            pnlFloor1Doors.Controls.Add(floor1DoorLeft);
            pnlFloor1Doors.Location = new Point(50, 60);
            pnlFloor1Doors.Name = "pnlFloor1Doors";
            pnlFloor1Doors.Size = new Size(160, 170);
            pnlFloor1Doors.TabIndex = 2;
            // 
            // floor1DoorRight
            // 
            floor1DoorRight.BackgroundImage = (Image)resources.GetObject("floor1DoorRight.BackgroundImage");
            floor1DoorRight.BackgroundImageLayout = ImageLayout.Stretch;
            floor1DoorRight.Location = new Point(80, 0);
            floor1DoorRight.Name = "floor1DoorRight";
            floor1DoorRight.Size = new Size(80, 170);
            floor1DoorRight.TabIndex = 1;
            // 
            // floor1DoorLeft
            // 
            floor1DoorLeft.BackgroundImageLayout = ImageLayout.Stretch;
            floor1DoorLeft.Location = new Point(0, 0);
            floor1DoorLeft.Name = "floor1DoorLeft";
            floor1DoorLeft.Size = new Size(80, 170);
            floor1DoorLeft.TabIndex = 0;
            // 
            // grpControlPanel
            // 
            grpControlPanel.BackColor = Color.FromArgb(64, 64, 64);
            grpControlPanel.Controls.Add(btnCloseDoors);
            grpControlPanel.Controls.Add(btnOpenDoors);
            grpControlPanel.Controls.Add(lblDisplay);
            grpControlPanel.Controls.Add(btnFloor1);
            grpControlPanel.Controls.Add(btnFloor0);
            grpControlPanel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpControlPanel.ForeColor = Color.White;
            grpControlPanel.Location = new Point(560, 50);
            grpControlPanel.Name = "grpControlPanel";
            grpControlPanel.Size = new Size(300, 300);
            grpControlPanel.TabIndex = 1;
            grpControlPanel.TabStop = false;
            grpControlPanel.Text = "Elevator Control Panel";
            // 
            // btnCloseDoors
            // 
            btnCloseDoors.BackColor = Color.FromArgb(192, 0, 0);
            btnCloseDoors.BackgroundImage = (Image)resources.GetObject("btnCloseDoors.BackgroundImage");
            btnCloseDoors.BackgroundImageLayout = ImageLayout.Stretch;
            btnCloseDoors.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCloseDoors.ForeColor = Color.White;
            btnCloseDoors.Location = new Point(156, 200);
            btnCloseDoors.Name = "btnCloseDoors";
            btnCloseDoors.Size = new Size(60, 60);
            btnCloseDoors.TabIndex = 4;
            btnCloseDoors.UseVisualStyleBackColor = false;
            // 
            // btnOpenDoors
            // 
            btnOpenDoors.BackColor = Color.FromArgb(0, 192, 0);
            btnOpenDoors.BackgroundImage = (Image)resources.GetObject("btnOpenDoors.BackgroundImage");
            btnOpenDoors.BackgroundImageLayout = ImageLayout.Stretch;
            btnOpenDoors.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOpenDoors.ForeColor = Color.White;
            btnOpenDoors.Location = new Point(75, 200);
            btnOpenDoors.Name = "btnOpenDoors";
            btnOpenDoors.Size = new Size(60, 60);
            btnOpenDoors.TabIndex = 3;
            btnOpenDoors.UseVisualStyleBackColor = false;
            // 
            // lblDisplay
            // 
            lblDisplay.BackColor = Color.Black;
            lblDisplay.Font = new Font("Consolas", 16F, FontStyle.Bold);
            lblDisplay.ForeColor = Color.Lime;
            lblDisplay.Location = new Point(6, 40);
            lblDisplay.Name = "lblDisplay";
            lblDisplay.Size = new Size(288, 50);
            lblDisplay.TabIndex = 0;
            lblDisplay.Text = "Floor 0";
            lblDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnFloor1
            // 
            btnFloor1.BackColor = Color.FromArgb(50, 50, 50);
            btnFloor1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnFloor1.ForeColor = Color.White;
            btnFloor1.Location = new Point(75, 120);
            btnFloor1.Name = "btnFloor1";
            btnFloor1.Size = new Size(60, 60);
            btnFloor1.TabIndex = 1;
            btnFloor1.Text = "1";
            btnFloor1.UseVisualStyleBackColor = false;
            // 
            // btnFloor0
            // 
            btnFloor0.BackColor = Color.FromArgb(50, 50, 50);
            btnFloor0.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnFloor0.ForeColor = Color.White;
            btnFloor0.Location = new Point(156, 120);
            btnFloor0.Name = "btnFloor0";
            btnFloor0.Size = new Size(60, 60);
            btnFloor0.TabIndex = 2;
            btnFloor0.Text = "0";
            btnFloor0.UseVisualStyleBackColor = false;
            // 
            // btnShowLog
            // 
            btnShowLog.BackColor = Color.FromArgb(70, 130, 180);
            btnShowLog.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnShowLog.ForeColor = Color.White;
            btnShowLog.Location = new Point(560, 370);
            btnShowLog.Name = "btnShowLog";
            btnShowLog.Size = new Size(300, 45);
            btnShowLog.TabIndex = 4;
            btnShowLog.Text = "📋 Show Logs";
            btnShowLog.UseVisualStyleBackColor = false;
            // 
            // dgvLogs
            // 
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvLogs.BackgroundColor = Color.FromArgb(45, 45, 48);

            // Column header style
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(70, 130, 180);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(50, 100, 150);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvLogs.ColumnHeadersHeight = 35;

            // Cell style
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(70, 130, 180);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True; // enable wrapping
            dgvLogs.DefaultCellStyle = dataGridViewCellStyle2;

            dgvLogs.EnableHeadersVisualStyles = false;
            dgvLogs.GridColor = Color.FromArgb(80, 80, 80);
            dgvLogs.Location = new Point(560, 430);
            dgvLogs.Name = "dgvLogs";

            // Row header style
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(70, 130, 180);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvLogs.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;

            dgvLogs.RowHeadersWidth = 25;
            dgvLogs.Size = new Size(523, 298);
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
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1095, 740);
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
            pnlFloor0Doors.ResumeLayout(false);
            pnlFloor1Doors.ResumeLayout(false);
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
        private Button btnOpenDoors;
        private Button btnCloseDoors;
        private Panel floor0DoorLeft;
        private Panel floor0DoorRight;
        private Panel floor1DoorLeft;
        private Panel floor1DoorRight;
    }
}