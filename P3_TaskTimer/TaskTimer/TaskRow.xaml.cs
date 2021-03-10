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
using System.Text.RegularExpressions;

namespace TaskTimer
{
    /// <summary>
    /// Interaction logic for TaskRow.xaml
    /// </summary>
    public partial class TaskRow : UserControl
    {
        static private Regex regrex = new Regex(@"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$");
        public Action<TaskRow> OnStartButtonClickedEvent;
        public Action<TaskRow> OnRemoveButtonClickedEvent;
        int prevLine = 0;
        TaskModel data;
        bool isRunning;
        SolidColorBrush colorBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 215, 240, 215));
        Thickness oneThick = new Thickness(1);
        Thickness zeroThick = new Thickness(0);
        public TaskModel Data { get => data;}

        public TaskRow() { }
        public TaskRow(TaskModel data)
        {
            InitializeComponent();

            this.data = data;

            taskDesc.Text = data.Description;
            timeSpent.Text = data.TimeSpent;
            timeGoal.Text = data.TimeGoal;

            progressBar.Value = data.CompletedPercent * 100.0f;

            taskDesc.IsReadOnly = true;
            timeSpent.IsReadOnly = true;
            timeGoal.IsReadOnly = true;

            taskDesc.BorderThickness = zeroThick;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = taskDesc;
            int lines = tb.GetLastVisibleLineIndex() - tb.GetFirstVisibleLineIndex();
            if (lines != prevLine)
            {   
                WrapText();
                prevLine = lines;
            }
            data.Description = tb.Text;
        }
        
        public void WrapText()
        {
            TextBox tb = taskDesc;
            this.Height = Math.Max(tb.ExtentHeight,this.Height);
            tb.Height = tb.ExtentHeight;
        }

        private void taskDesc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            taskDesc.IsReadOnly = false;
            taskDesc.BorderThickness = oneThick;
            taskDesc.Focus();
        }

        private void taskDesc_LostFocus(object sender, RoutedEventArgs e)
        {
            WrapText();
            taskDesc.IsReadOnly = true;
            taskDesc.BorderThickness = zeroThick;
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
                    Background = colorBrush;
                }
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            data.CurrentState = TaskModel.State.REMOVED;
            OnRemoveButtonClickedEvent(this);
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            data.Reset();
            timeSpent.Text = data.TimeSpent;
            progressBar.Value = 0;
        }
        string tempString;
        private void timeSpent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tempString = timeSpent.Text;
            ChangeTextBoxState(timeSpent, true);
        }

        private void timeGoal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tempString = timeGoal.Text;
            ChangeTextBoxState(timeGoal, true);
        }

        private void ChangeTextBoxState(TextBox tb,bool editable)
        {
            if (editable)
            {
                tb.IsReadOnly = false;
                tb.BorderThickness = oneThick;
            }
            else
            {
                tb.IsReadOnly = true;
                tb.BorderThickness = zeroThick;
            }
        }

        private void timeSpent_TextChanged(object sender, TextChangedEventArgs e)
        {
            //data.TimeSpent = timeSpent.Text;
        }

        private void timeGoal_TextChanged(object sender, TextChangedEventArgs e)
        {
            //data.TimeGoal = timeGoal.Text;
        }

        private void timeSpent_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeTextBoxState(timeSpent, false);
            if (!regrex.IsMatch(timeSpent.Text))
            {
                timeSpent.Text = Utils.FormatTime(tempString);
            }
            else
            {
                timeSpent.Text = Utils.FormatTime(timeSpent.Text);
                data.TimeSpent = timeSpent.Text;
            }
        }

        private void timeGoal_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeTextBoxState(timeGoal, false);
            if (!regrex.IsMatch(timeGoal.Text))
            {
                timeGoal.Text = Utils.FormatTime(tempString);
            }
            else
            {
                timeGoal.Text = Utils.FormatTime(timeGoal.Text);
                data.TimeGoal = timeGoal.Text;
            }
        }

        //private string FormatTime(string s)
        //{
        //    string[] p = s.Split(':');
        //    return string.Format("{0:00}:{1:00}:{2:00}", int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]));
        //}
    }
}
