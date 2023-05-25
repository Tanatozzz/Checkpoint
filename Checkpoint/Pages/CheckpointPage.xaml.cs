using Checkpoint.API;
using Checkpoint.Classes;
using Checkpoint.Models;
using Checkpoint.Singletons;
using Checkpoint.Windows;
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
    public partial class CheckpointPage : Page, IListViewSearchable
    {
        UpdateData updateData = new UpdateData();
        public CheckpointPage()
        {
            InitializeComponent();
            DataRefresh();
        }
        public void SearchListView(string searchText)
        {
            // Фильтрация списка сотрудников по введенному тексту
            var filteredEmployees = AllCheckpointsSingleton.Instance.Checkpoints.Where(chk =>
                chk.Title.Contains(searchText));

            // Обновление источника данных для ListView
            CheckpointLV.ItemsSource = filteredEmployees;
        }
        private async void DataRefresh()
        {
            var allCheckpoints = AllCheckpointsSingleton.Instance.Checkpoints;
            await updateData.Update();
            CheckpointLV.ItemsSource = allCheckpoints;
        }
        private async void CheckpointLV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Checkpointt selectedCheckpoint)
            {
                var editEmployeeWindow = new AddNewCheckpoint(selectedCheckpoint, true);
                editEmployeeWindow.AddButton.Content = "Изменить";

                editEmployeeWindow.ShowDialog();

                if (editEmployeeWindow.DialogResult.HasValue && editEmployeeWindow.DialogResult.Value)
                {
                    HttpQuery httpmanager = new HttpQuery();
                    await httpmanager.UpdateCheckpoint(selectedCheckpoint);
                }
            }
            DataRefresh();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var addCheckpointWindow = new AddNewCheckpoint();
            addCheckpointWindow.ShowDialog();
            DataRefresh();
        }
    }
}
