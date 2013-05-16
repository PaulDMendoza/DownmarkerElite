using System.Windows.Forms;
using DownMarker.Core;

namespace DownMarker.WinForms
{
    public partial class LinkEditorView : Form
    {
        private readonly LinkEditorViewModel viewModel;

        public LinkEditorView(LinkEditorViewModel viewModel, bool image)
        {
            InitializeComponent();

            if (image)
            {
                Text = "Image Link Editor";
                descriptionLabel.Text = "Image Description";
                linkLabel.Text = "Image Link";
            }

            this.viewModel = viewModel;
           
            if (!this.viewModel.CreateTargetAvailable)
            {
               checkBoxCreateTarget.Checked = false;
               checkBoxCreateTarget.Enabled = false;
            }

            var events = new PropertyEvents<LinkEditorViewModel>(viewModel);
            events[x => x.LinkTarget] += delegate { this.linkTargetTextBox.Text = viewModel.LinkTarget; };
            events[x => x.LinkDescription] += delegate { this.linkDescriptionTextBox.Text = viewModel.LinkDescription; };
            events[x => x.LinkDescriptionError] += 
                delegate { errorProvider.SetError(linkDescriptionTextBox, viewModel.LinkDescriptionError); };
            events[x => x.LinkTargetError] +=
                delegate { errorProvider.SetError(linkTargetTextBox, viewModel.LinkTargetError); };
            events[x => x.CreateTarget] += delegate { checkBoxCreateTarget.Checked = viewModel.CreateTarget; };
            events[x => x.CreateTargetError] += 
                delegate { errorProvider.SetError(checkBoxCreateTarget, viewModel.CreateTargetError); };
            events[x => x.IsValid] += delegate { okButton.Enabled = viewModel.IsValid; };
            events.RaiseAll();
        }

        private void HandleLinkTargetTextBoxTextChanged(object sender, System.EventArgs e)
        {
            viewModel.LinkTarget = linkTargetTextBox.Text;
        }

        private void HandleLinkDescriptionTextBoxTextChanged(object sender, System.EventArgs e)
        {
            viewModel.LinkDescription = linkDescriptionTextBox.Text;
        }

        private void HandleBrowseButtonClick(object sender, System.EventArgs e)
        {
            viewModel.Browse();
        }

        private void HandleCreateTargetCheckedChanged(object sender, System.EventArgs e)
        {
           viewModel.CreateTarget = checkBoxCreateTarget.Checked;
        }

    }
}
