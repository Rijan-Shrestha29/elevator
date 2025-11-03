using System;
using System.Drawing;
using System.IO;
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

            // Create and load images immediately
            CreateAndLoadImages();

            InitializeSimulation();
        }

        private void CreateAndLoadImages()
        {
            string resourcesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
            }

            // Create all required images with proper sizes
            CreateImage(Path.Combine(resourcesPath, "building_background.png"), 980, 740, Color.FromArgb(210, 220, 240), "Building Background");
            CreateImage(Path.Combine(resourcesPath, "elevator_shaft.png"), 260, 540, Color.FromArgb(180, 180, 200), "Elevator Shaft");
            CreateImage(Path.Combine(resourcesPath, "elevator_interior.png"), 260, 540, Color.FromArgb(160, 170, 190), "Shaft Interior");
            CreateImage(Path.Combine(resourcesPath, "elevator_cab.png"), 160, 170, Color.FromArgb(100, 100, 130), "Elevator Cab");
            CreateImage(Path.Combine(resourcesPath, "elevator_door.png"), 80, 170, Color.FromArgb(200, 200, 220), "Elevator Door");
            CreateImage(Path.Combine(resourcesPath, "floor_door.png"), 160, 170, Color.FromArgb(120, 120, 150), "Floor Door");
            CreateImage(Path.Combine(resourcesPath, "control_panel.png"), 300, 260, Color.FromArgb(220, 220, 240), "Control Panel");

            // Load images immediately after creating them
            LoadImagesImmediately();
        }

        private void CreateImage(string filePath, int width, int height, Color color, string text)
        {
            try
            {
                using Bitmap bmp = new(width, height);
                using Graphics g = Graphics.FromImage(bmp);
                using SolidBrush brush = new(color);
                using Font font = new("Arial", Math.Min(width / 15, 12)); // Dynamic font size
                using Pen borderPen = new(Color.DarkGray, 2);
                using StringFormat sf = new()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                // Fill background
                g.FillRectangle(brush, 0, 0, width, height);

                // Draw border
                g.DrawRectangle(borderPen, 1, 1, width - 3, height - 3);

                // Draw text
                g.DrawString(text, font, Brushes.Black, new RectangleF(10, 10, width - 20, height - 20), sf);

                // Save image
                bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                Console.WriteLine($"Created image: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating image {filePath}: {ex.Message}");
            }
        }

        private void LoadImagesImmediately()
        {
            try
            {
                string resourcesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");

                // Load main form background
                string bgPath = Path.Combine(resourcesPath, "building_background.png");
                if (File.Exists(bgPath))
                {
                    this.BackgroundImage = new Bitmap(bgPath);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }

                // Load elevator shaft background
                string shaftPath = Path.Combine(resourcesPath, "elevator_shaft.png");
                if (File.Exists(shaftPath))
                {
                    pnlBuildingBackground.BackgroundImage = new Bitmap(shaftPath);
                    pnlBuildingBackground.BackgroundImageLayout = ImageLayout.Stretch;
                }

                // Load elevator interior
                string interiorPath = Path.Combine(resourcesPath, "elevator_interior.png");
                if (File.Exists(interiorPath))
                {
                    pnlElevatorShaft.BackgroundImage = new Bitmap(interiorPath);
                    pnlElevatorShaft.BackgroundImageLayout = ImageLayout.Stretch;
                }

                // Load elevator cabinet
                string cabPath = Path.Combine(resourcesPath, "elevator_cab.png");
                if (File.Exists(cabPath))
                {
                    pnlElevator.BackgroundImage = new Bitmap(cabPath);
                    pnlElevator.BackgroundImageLayout = ImageLayout.Stretch;
                }

                // Load elevator doors
                string doorPath = Path.Combine(resourcesPath, "elevator_door.png");
                if (File.Exists(doorPath))
                {
                    elevatorDoorLeft.BackgroundImage = new Bitmap(doorPath);
                    elevatorDoorLeft.BackgroundImageLayout = ImageLayout.Stretch;
                    elevatorDoorRight.BackgroundImage = new Bitmap(doorPath);
                    elevatorDoorRight.BackgroundImageLayout = ImageLayout.Stretch;
                }

                // Load floor doors
                string floorDoorPath = Path.Combine(resourcesPath, "floor_door.png");
                if (File.Exists(floorDoorPath))
                {
                    pnlFloor0Doors.BackgroundImage = new Bitmap(floorDoorPath);
                    pnlFloor0Doors.BackgroundImageLayout = ImageLayout.Stretch;
                    pnlFloor1Doors.BackgroundImage = new Bitmap(floorDoorPath);
                    pnlFloor1Doors.BackgroundImageLayout = ImageLayout.Stretch;
                }

                // Load control panel
                string controlPath = Path.Combine(resourcesPath, "control_panel.png");
                if (File.Exists(controlPath))
                {
                    grpControlPanel.BackgroundImage = new Bitmap(controlPath);
                    grpControlPanel.BackgroundImageLayout = ImageLayout.Stretch;
                }

                // Set all panels to transparent to show images
                SetPanelsTransparent();

                Console.WriteLine("All images loaded successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading images: {ex.Message}\nUsing default colors.", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetDefaultColors();
            }
        }

        private void SetPanelsTransparent()
        {
            pnlBuildingBackground.BackColor = Color.Transparent;
            pnlElevatorShaft.BackColor = Color.Transparent;
            pnlElevator.BackColor = Color.Transparent;
            elevatorDoorLeft.BackColor = Color.Transparent;
            elevatorDoorRight.BackColor = Color.Transparent;
            pnlFloor0Doors.BackColor = Color.Transparent;
            pnlFloor1Doors.BackColor = Color.Transparent;
            grpControlPanel.BackColor = Color.Transparent;
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

        private void SetDefaultColors()
        {
            // Fallback colors
            this.BackgroundImage = null;
            this.BackColor = Color.FromArgb(230, 230, 250);

            pnlBuildingBackground.BackgroundImage = null;
            pnlBuildingBackground.BackColor = Color.FromArgb(210, 220, 240);

            pnlElevatorShaft.BackgroundImage = null;
            pnlElevatorShaft.BackColor = Color.FromArgb(180, 180, 200);

            pnlElevator.BackgroundImage = null;
            pnlElevator.BackColor = Color.FromArgb(100, 100, 130);

            elevatorDoorLeft.BackgroundImage = null;
            elevatorDoorLeft.BackColor = Color.FromArgb(200, 200, 220);

            elevatorDoorRight.BackgroundImage = null;
            elevatorDoorRight.BackColor = Color.FromArgb(200, 200, 220);

            pnlFloor0Doors.BackgroundImage = null;
            pnlFloor0Doors.BackColor = Color.FromArgb(120, 120, 150);

            pnlFloor1Doors.BackgroundImage = null;
            pnlFloor1Doors.BackColor = Color.FromArgb(120, 120, 150);

            grpControlPanel.BackgroundImage = null;
            grpControlPanel.BackColor = Color.FromArgb(220, 220, 240);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Force refresh to show images
            this.Refresh();
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