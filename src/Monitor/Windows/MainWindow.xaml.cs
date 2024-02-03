using gTools.WPF;
using MainApplication.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace MainApplication.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : gWindowBase
    {
        #region Initialization

        private MainWindowViewModel _viewModel = new MainWindowViewModel();       

        public MainWindow()
        {
            InitializeComponent();

            this.IsLocationArchivized = true;
            this.Config = App.Config;            
            this.DataContext = _viewModel.Model;                        

            _viewModel.Model.PropertyChanged += Model_PropertyChanged;            
        }        

        #endregion        

        #region Events

        protected override void OnContentRendered(EventArgs e)
        {
            LogsView.Initialize();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            LogsView.Cleanup();
        }              

        private void ExpandButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ExpandPanel();            
        }

        private void Start_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.Model.IsServiceInstalled && _viewModel.Model.IsServiceStopped;
        }

        private void Start_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.StartService();
        }
        
        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.Model.IsServiceInstalled && _viewModel.Model.IsServiceRunning;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.StopService();
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //refresh routed ui commands
            if (e.PropertyName == nameof(_viewModel.Model.IsServiceRunning))
            {                
                Dispatcher.Invoke(() =>
                {              
                    CommandManager.InvalidateRequerySuggested();
                });
            }
        }

        #endregion        
    }
}
