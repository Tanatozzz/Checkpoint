using Checkpoint.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Checkpoint.Pages
{
    /// <summary>
    /// Interaction logic for CheckpointPage.xaml
    /// </summary>
    public partial class CheckpointPage : Page
    {
        public CheckpointPage()
        {
            InitializeComponent();
            CheckpointLV.ItemsSource = AllCheckpointsSingleton.Instance.Checkpoints;
        }
    }
}
