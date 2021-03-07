using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskTimer
{
    public class TaskModel
    {
        TimeSpan timeSpent;
        TimeSpan realTime;
        string timeGoal;
        string description;

        //For calculation
        DateTime timeStart;        
        double destTimeValue;
        double completedPercent = 0;

        State currentState;
        public enum State : int
        {
            NONE,
            RUNNING,
            STOP,
            COMPLETED,
            REMOVED
        }

        public TaskModel()
        {
            timeSpent = TimeSpan.Parse("00:00:00");
            timeGoal = "00:00:00";
        }

        public void Reset()
        {
            timeSpent = TimeSpan.Parse("00:00:00");
            timeStart = DateTime.Now;
        }

        public string TimeGoal 
        {
            get => timeGoal;
            set {
                timeGoal = value;                
                TimeSpan duration = TimeSpan.Parse(timeGoal);
                destTimeValue = duration.TotalSeconds;
            } 
        }
        public string Description { get => description; set => description = value; }
        public string TimeSpent
        {
            get
            {   
                TimeSpan t = timeSpent.Add(realTime);
                return string.Format("{0:00}:{1:00}:{2:00}", t.Hours, t.Minutes, t.Seconds);
            }
        }
        public DateTime TimeStart { get => timeStart;}
        
        public double CompletedPercent { get => completedPercent;}
        public State CurrentState { get => currentState; set => currentState = value; }

        public void Stop()
        {
            currentState = State.STOP;
            timeSpent = timeSpent.Add(realTime);
        }
        public void Start()
        {
            timeStart = DateTime.Now;
            currentState = State.RUNNING;
            completedPercent = 0;
        }
        public void Update()
        {
            if(currentState == State.RUNNING)
            {
                if (completedPercent < 1)
                {
                    realTime = DateTime.Now.Subtract(timeStart);

                    if (destTimeValue != 0)
                    {
                        completedPercent = ((realTime.Seconds + timeSpent.Seconds) / destTimeValue);
                        completedPercent = Math.Min(1, CompletedPercent);
                    }
                }
                else
                {
                    currentState = State.COMPLETED;
                }
            }
        }
    }
}
