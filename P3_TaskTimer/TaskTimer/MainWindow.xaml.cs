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
using System.Text.RegularExpressions;
using System.Timers;
using System.Threading;

namespace TaskTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Regex regrex = new Regex(@"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$");        
        
        private bool isUIUpdateRunning;        
        private List<TaskRow> taskRowList = new List<TaskRow>();

        Thread uiThread;
        
        public MainWindow()
        {
            InitializeComponent();

            taskGoal.Text = "00:00:00";

            uiThread = new Thread(HandleUIUpdate);
            uiThread.IsBackground = true;
            uiThread.Start();

            this.StateChanged += new EventHandler(HandleWindowStateChangeEvent);
        }

        private void HandleWindowStateChangeEvent(object sender, EventArgs e)
        {
            Thread updateContentSize = new Thread(new ThreadStart(() => {
                Thread.Sleep(5);
                UpdateContentSize();
            }));
            updateContentSize.Start();
        }

        private void HandleUIUpdate()
        {
            isUIUpdateRunning = true;
            
            while(isUIUpdateRunning)
            {
                //Update UI
                for (int i = 0; i < taskRowList.Count; ++i)
                {   
                    TaskRow row = taskRowList[i];                    
                    row.Dispatcher.Invoke(row.Update);
                }
                Thread.Sleep(500);
            }
        }

        private void TaskRow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            TaskModel task = new TaskModel();
            task.TimeGoal = taskGoal.Text;
            task.Description = taskDesc.Text;

            TaskRow row = new TaskRow(task);
            this.TaskList.Children.Add(row);
            row.OnStartButtonClickedEvent = OnTaskRowStartEvenHandler;
            row.OnRemoveButtonClickedEvent = OnRemoveButtonClickedEvenHandler;
            
            this.taskRowList.Add(row);

            HandleWindowStateChangeEvent(null, null);
        }

        private void OnRemoveButtonClickedEvenHandler(TaskRow obj)
        {   
            this.taskRowList.Remove(obj);
            this.TaskList.Children.Remove(obj);

            obj = null;
        }

        private void OnTaskRowStartEvenHandler(TaskRow obj)
        {
            
        }

        private void taskGoal_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!regrex.IsMatch(taskGoal.Text))
            {
                taskGoal.Text = "00:00:00";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {   
            isUIUpdateRunning = false;
            Save();
        }

        public void Load()
        {
            TaskArray tasks = DataSaver.LoadFromFile<TaskArray>("tasks.json");
            if (tasks!=null && tasks.Collection!=null)
            {
                for(int i = 0; i < tasks.Collection.Length; ++i)
                {
                    TaskModel task = tasks.Collection[i];
                    TaskRow row = new TaskRow(task);
                    this.TaskList.Children.Add(row);
                    row.OnStartButtonClickedEvent = OnTaskRowStartEvenHandler;
                    row.OnRemoveButtonClickedEvent = OnRemoveButtonClickedEvenHandler;

                    this.taskRowList.Add(row);
                }
            }
        }

        public void Save()
        {
            List<TaskModel> list = new List<TaskModel>();
            for(int i = 0; i < taskRowList.Count; ++i)
            {
                taskRowList[i].Data.Stop();
                list.Add(taskRowList[i].Data);
            }
            TaskArray array = new TaskArray();
            array.Collection = list.ToArray<TaskModel>();
            DataSaver.WriteToFile<TaskArray>(array, "tasks.json");
        }

        private void UpdateContentSize()
        {   
            for (int i = 0; i < taskRowList.Count; ++i)
            {
                TaskRow r = taskRowList[i];
                r.Dispatcher.Invoke(r.WrapText);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();            
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {   
            UpdateContentSize();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateContentSize();
        }
    }
}
