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
            controller = new ElevatorController(pnlElevator, lblDisplay, logger, pnlFloor0Doors, pnlFloor1Doors);

            // Attach button events
            btnFloor0.Click += (s, e) => controller.GoToFloor(0);
            btnFloor1.Click += (s, e) => controller.GoToFloor(1);

            btnRequest0.Click += (s, e) => controller.GoToFloor(0);
            btnRequest1.Click += (s, e) => controller.GoToFloor(1);

            btnShowLog.Click += (s, e) => logger.ShowLogs();
            btnClearLogs.Click += (s, e) => logger.ClearAllLogs();

            // Add door control buttons
            btnOpenDoors.Click += (s, e) => controller.ManualOpenDoors();
            btnCloseDoors.Click += (s, e) => controller.ManualCloseDoors();

            // Set correct Z-order - elevator BETWEEN shaft background and floor doors
            SetCorrectZOrder();
        }

        private void SetCorrectZOrder()
        {
            // The key is the ORDER OF CONTROLS in pnlElevatorShaft
            // Controls are drawn in the order they appear in the Controls collection
            // First = Backmost, Last = Frontmost

            // Current order in pnlElevatorShaft:
            // 1. pnlElevator (added first - backmost)
            // 2. btnRequest0, pnlFloor0Doors, btnRequest1, lblFloor1, lblFloor0, pnlFloor1Doors (added later - frontmost)

            // This gives us:
            // BACK: pnlElevatorShaft background
            // MIDDLE: pnlElevator (moving cabin)
            // FRONT: pnlFloor0Doors, pnlFloor1Doors, labels, buttons

            // Make sure floor doors are in front
            pnlFloor0Doors.BringToFront();
            pnlFloor1Doors.BringToFront();

            // Make sure labels and buttons are in front
            lblFloor0.BringToFront();
            lblFloor1.BringToFront();
            btnRequest0.BringToFront();
            btnRequest1.BringToFront();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Ensure Z-order is maintained after load
            SetCorrectZOrder();
        }

        private void BtnShowLog_Click(object sender, EventArgs e) { }

        private void PnlElevator_Paint(object sender, PaintEventArgs e) { }

        private void GrpControlPanel_Enter(object sender, EventArgs e) { }

        private void PnlFloor1Doors_Paint(object sender, PaintEventArgs e) { }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            controller?.Dispose();
            base.OnFormClosed(e);
        }
    }
}