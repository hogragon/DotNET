using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Threading;

namespace TaskTimer
{
    /// <summary>
    /// Interaction logic for TaskRow.xaml
    /// </summary>
    public partial class TaskRow : UserControl
    {
        public Action<TaskRow> OnStartButtonClickedEvent;
        public Action<TaskRow> OnRemoveButtonClickedEvent;
        int prevLine = 0;
        TaskModel data;
        bool isRunning;

        public TaskModel Data { get => data;}

        public TaskRow() { }
        public TaskRow(TaskModel data)
        {
            InitializeComponent();

            this.data = data;

            taskDesc.Text = data.Description;
            timeSpent.Text = data.TimeSpent;
            timeGoal.Text = data.TimeGoal;

            taskDesc.IsReadOnly = true;
            timeSpent.IsReadOnly = true;
            timeGoal.IsReadOnly = true;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = taskDesc;
            int lines = tb.GetLastVisibleLineIndex() - tb.GetFirstVisibleLineIndex();            
            if (lines != prevLine)
            {
                this.Height = tb.ExtentHeight;
                tb.Height = tb.ExtentHeight;
                prevLine = lines;
            }
            
        }

        private void taskDesc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            taskDesc.IsReadOnly = false;
            taskDesc.Focus();
        }

        private void taskDesc_LostFocus(object sender, RoutedEventArgs e)
        {
            taskDesc.IsReadOnly = true;
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (data.CurrentState == TaskModel.State.RUNNING)
            {
                data.Stop();
                isRunning = false;
                Button btn = sender as Button;
                btn.Content = ">";

            }
            else if(data.CurrentState == TaskModel.State.NONE || data.CurrentState == TaskModel.State.STOP)
            {
                data.Start();
                isRunning = true;
                Button btn = sender as Button;
                btn.Content = "||";
            }
            
            OnStartButtonClickedEvent?.Invoke(this);
        }
        
        public void Update()
        {
            if (isRunning)
            {
                data.Update();

                timeGoal.Text = data.TimeGoal;
                timeSpent.Text = data.TimeSpent;
                progressBar.Value = data.CompletedPercent * 100.0f;

                if(data.CurrentState == TaskModel.State.COMPLETED)
                {
                    isRunning = false;
                    buttonStart.Content = ">";
                }
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            data.CurrentState = TaskModel.State.REMOVED;
            OnRemoveButtonClickedEvent(this);
        }
    }
}
