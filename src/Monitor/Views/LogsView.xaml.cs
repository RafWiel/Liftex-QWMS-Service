using MainApplication.ViewModels;
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

namespace MainApplication.Views
{
    /// <summary>
    /// Interaction logic for LogsView.xaml
    /// </summary>
    public partial class LogsView : UserControl
    {
        #region Initialization      

        private LogsViewModel _viewModel = new LogsViewModel();

        public LogsView()
        {
            InitializeComponent();

            _viewModel.ViewDispatcher = this.Dispatcher;
            _viewModel.ScrollToLastDataGridItemEvent += _viewModel_ScrollToLastDataGridItemEvent;
            _viewModel.UnselectDataGridItemEvent += _viewModel_UnselectDataGridItemEvent;

            DataContext = _viewModel;
        }

        #endregion

        #region Events

        private void _viewModel_UnselectDataGridItemEvent()
        {
            if (DataGrid.SelectedIndex == 0)
                DataGrid.SelectedItem = null;
        }

        private void _viewModel_ScrollToLastDataGridItemEvent()
        {
            DataGrid.ScrollIntoView(_viewModel.Items.Last());
        }

        private void DataGrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            //zaznacz caly tekst
            if (e.EditingElement.GetType().Equals(typeof(TextBox)))
            {
                var textbox = (TextBox)e.EditingElement;
                textbox.SelectAll();
            }
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            _viewModel.Initialize();
        }

        public void Cleanup()
        {
            _viewModel.Cleanup();
        }

        #endregion
    }
}
