using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WordToJson.View
{
    /// <summary>
    /// Description for WordManageView.
    /// </summary>
    public partial class WordManageView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the WordManageView class.
        /// </summary>
        public WordManageView()
        {
            InitializeComponent();            
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }

    }
}