using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FantasyLeague.FEPLObjects;
using FantasyLeague.Comparators;
using System.Threading;

//TODO: Make one unified pull in players and pull in teams method
//TODO: Move this out into its own class making it easier to see what calls what
//TODO: Move all objects into their own files
//TODO: Finish Algorithm
namespace FantasyLeague
{
    public partial class Form1 : Form
    {
        public const int SEASON = 4;
        // Timer timer;
        public delegate void UpdateRunAllPlayerStatus(double status);
        public UpdateRunAllPlayerStatus UpdateRunAllPlayerStatusDelegate;


        public Form1()
        {
            InitializeComponent();
            UpdateRunAllPlayerStatusDelegate = new UpdateRunAllPlayerStatus(UpdateRunAllPlayerStatusMethod);

        }
        private void btn_Test_Click(object sender, EventArgs e)
        {
            List<Player> players = DataAccess.Instance.GetPlayers("Active = 1");
            players.Sort(new PointsComparator());

            MyMax max = new MyMax();
            max.MAX = 0;
            int x = -1;
            foreach (Player p in players)
            {
                ProcessThread pt = new ProcessThread()
                {
                    Max = max,
                    Deck = players,
                    StartIndex = x++
                };
                Thread thread = new Thread(pt.RunAlgorithm)
                {
                    Name = "StartIndex: " + x,
                    Priority = ThreadPriority.Lowest
                };
                thread.Start();
            }

            Console.Write("Done");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int gameWeek;
            if (int.TryParse(txtGW.Text, out gameWeek))
            {
                RunAllPlayerThread RAPT = new RunAllPlayerThread(gameWeek, this);
                Thread RAPTThread = new Thread(RAPT.Run);
                RAPTThread.Start();             
            }
            else MessageBox.Show("Gameweek is null or not a number");
        }

        public void UpdateRunAllPlayerStatusMethod(double status)
        {
            lbl_Status.Text = String.Format("{0:0.00}", status);
        }

    }
}
