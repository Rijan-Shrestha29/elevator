using System;
using System.Windows.Forms;
using Elevator_project.Models;
using Elevator_project.Utils;

namespace Elevator_project
{
    public partial class MainForm : Form
    {
        private ElevatorController controller;
        private Logger logger;

        public MainForm()
        {
            InitializeComponent();
            InitializeSimulation();
        }

        private void InitializeSimulation()
        {
            logger = new Logger(dgvLogs);
            controller = new ElevatorController(pnlElevator, lblDisplay, logger);

            // Attach button events
            btnFloor0.Click += (s, e) => controller.GoToFloor(0);
            btnFloor1.Click += (s, e) => controller.GoToFloor(1);

            btnRequest0.Click += (s, e) => controller.GoToFloor(0);
            btnRequest1.Click += (s, e) => controller.GoToFloor(1);

            btnShowLog.Click += (s, e) => Logger.ShowLogs();
        }

        private void MainForm_Load(object sender, EventArgs e) { }

        private void BtnShowLog_Click(object sender, EventArgs e) { }

        private void PnlElevator_Paint(object sender, PaintEventArgs e) { }

        private void GrpControlPanel_Enter(object sender, EventArgs e) { }

        private void PnlFloor1Doors_Paint(object sender, EventArgs e) { }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            controller?.Dispose();
            base.OnFormClosed(e);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}