using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkanoidWF.Rendering
{
    public partial class StartUC : UserControl
    {

        public event EventHandler? ButtonClicked;
        public StartUC()
        {
            InitializeComponent();
            CenteringButton();
            PlayButton.Click += OnButtonClicked;
        }

        private void OnButtonClicked(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
            Dispose();
        }

        private void CenteringButton()
        {
            PlayButton.Location = new Point(
                (PlayButton.ClientSize.Width - PlayButton.Width) / 2,
                (PlayButton.ClientSize.Height - PlayButton.Height) / 2
            );
        }
        private void StartUC_Resize(object sender, EventArgs e)
        {
            CenteringButton();
        }
    }
}
