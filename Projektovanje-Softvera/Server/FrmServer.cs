namespace Server
{
    public partial class FrmServer : Form
    {
        private Server server;

        public FrmServer()
        {
            InitializeComponent();
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                server = new Server();
                server.Start();
                UpdateControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri pokretanju servera: {ex.Message}",
                    "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                server.Stop();
                UpdateControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Server nije uspeo da se zaustavi: {ex.Message}",
                    "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateControls()
        {
            btnStart.Enabled = !server.IsRunning;
            btnStop.Enabled = server.IsRunning;
            txtStatus.Text = server.IsRunning ? "Server je pokrenut." : "Server je zaustavljen.";
        }

        private void FrmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
