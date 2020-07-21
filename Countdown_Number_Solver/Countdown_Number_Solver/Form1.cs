using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

/*  By Jordan Wright, GitHub ackar89124
 * 
 * 
 * 
 * */

namespace Countdown_Number_Solver
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        int target = 000;
        List<int> numbers = new List<int> { 0, 0, 0, 0, 0, 0 };

        int timeLeft;

        public Form1()
        {
            InitializeComponent();
            SixNumGen(4, 2);
            TarNumGen();
            
        }

        // Buttons

        private void button1_Click(object sender, EventArgs e)
        {
            TarNumGen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SixNumGen(6, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SixNumGen(5, 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SixNumGen(4, 2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SixNumGen(3, 3);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SixNumGen(2, 4);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(textBox1.Text, out n))
            {
                if (n > 0 && n < 1000)
                {
                    label2.Text = n.ToString();
                    target = n;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timeLeft = 30;
            label9.Text = "30";
            timer1.Start();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Solve(target, numbers);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SixNumGen();
        }

        // Generate Numbers

        public void SixNumGen(int small, int large)
        {
            List<int> lray = new List<int> { 25, 50, 75, 100 };
            List<int> lray2 = new List<int> { 12, 37, 62, 87 };     // Can be added
            List<int> sray = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> results = new List<int>();
            int random;

            for (int i = 0; i < large; i++)
            {
                random = r.Next(0, lray.Count);
                results.Add(lray[random]);
                lray.RemoveAt(random);
            }
            for (int i = 0; i < small; i++)
            {
                random = r.Next(0, sray.Count);
                results.Add(sray[random]);
                sray.RemoveAt(random);
            }

            numbers = results;

            label3.Text = results[0].ToString();
            label4.Text = results[1].ToString();
            label5.Text = results[2].ToString();
            label6.Text = results[3].ToString();
            label7.Text = results[4].ToString();
            label8.Text = results[5].ToString();
            textBox2.Text = results[0].ToString();
            textBox3.Text = results[1].ToString();
            textBox4.Text = results[2].ToString();
            textBox5.Text = results[3].ToString();
            textBox6.Text = results[4].ToString();
            textBox7.Text = results[5].ToString();
        }

        public void SixNumGen()
        {
            List<int> results = new List<int>();
            int n;
            if (int.TryParse(textBox2.Text, out n))
            {
                if (n > 0 && n <= 100)
                {
                    results.Add(n);
                }
            }
            if (int.TryParse(textBox3.Text, out n))
            {
                if (n > 0 && n <= 100)
                {
                    results.Add(n);
                }
            }
            if (int.TryParse(textBox4.Text, out n))
            {
                if (n > 0 && n <= 100)
                {
                    results.Add(n);
                }
            }
            if (int.TryParse(textBox5.Text, out n))
            {
                if (n > 0 && n <= 100)
                {
                    results.Add(n);
                }
            }
            if (int.TryParse(textBox6.Text, out n))
            {
                if (n > 0 && n <= 100)
                {
                    results.Add(n);
                }
            }
            if (int.TryParse(textBox7.Text, out n))
            {
                if (n > 0 && n <= 100)
                {
                    results.Add(n);
                }
            }
            if (results.Count == 6)
            {
                label3.Text = results[0].ToString();
                label4.Text = results[1].ToString();
                label5.Text = results[2].ToString();
                label6.Text = results[3].ToString();
                label7.Text = results[4].ToString();
                label8.Text = results[5].ToString();
                numbers = results;
            }
        }

        public void TarNumGen()
        {
            int number = r.Next(101, 1000);
            target = number;
            label2.Text = number.ToString();
        }

        // Timer

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                label9.Text = timeLeft.ToString();
            }
            else
            {
                timer1.Stop();
                label9.Text = "Finished";
            }
        }
        
        // Output

        public void OutputResults(List<string> results)
        {
            string OutStr = "";
            foreach (string s in results.Reverse<string>())
            {
                OutStr += s;
                OutStr += "\n";
            }
            label11.Text = OutStr;
        }

        public string OutputList(List<int> list)
        {
            string s = "";
            foreach (int i in list)
            {
                s += i.ToString() + "   ";
            }
            return s;
        }

        // Search Algorithm

        public void Solve(int tar, List<int> num)
        {
            int closest = 0;
            List<string> results = new List<string> { "test", "test2", "test3" };
            List<int> winning = new List<int>();
            List<Data> searched = new List<Data>();
            List<Data> searching = new List<Data>();

            Data data = new Data(num);
            Data best = data;
            searching.Add(data);
            int temp = 0;
            bool mark = true;
            string text = "";

            List<int> record = new List<int> { 0, 0, 0, 0, 0 };
            
            while (searching.Count > 0 && closest != tar)   // Checks each unsearched list until they run out or find the target
            {
                temp = searching[0].After.Count;
                for (int i = 0; i < temp; i++)          // For each initial number
                {
                    for (int n = 0; n < temp; n++)      // Checking each secondary number
                    {
                        if (i != n)                     // Removing any instance of checking against itself
                        {
                            for (int o = 0; o < 4; o++) // And applies each operator to creat the a new list
                            {
                                // Apply calculation
                                data = new Data(searching[0].After, searching[0].After[n], searching[0].After[i], o);
                                if(n > i && (o < 2))
                                {
                                    mark = false;
                                }
                                else if (0 == data.N3 || data.N1 == data.N3 || data.N2 == data.N3)
                                {
                                    mark = false;
                                }
                                else
                                {
                                    mark = true;
                                }
                                // if the newly created list has already been created
                                foreach(Data d in searching)
                                {
                                    if (d.After.All(data.After.Contains))
                                    {
                                        mark = false;
                                    }
                                    if (!mark)
                                    {
                                        break;
                                    }
                                    record[4]++;
                                }
                                foreach (Data d in searched)
                                {
                                    if (d.After.All(data.After.Contains))
                                    {
                                        mark = false;
                                    }
                                    if (!mark)
                                    {
                                        break;
                                    }
                                    record[4]++;
                                }
                                if (mark)   // If new, add to list
                                {
                                    searching.Add(data);
                                }
                                if(data.N3 == tar)  // If the target has been located, finish
                                {
                                    closest = data.N3;
                                    best = data;
                                }
                                else if (Math.Abs(closest - tar) > Math.Abs(data.N3 - tar)) // if result is closet, update
                                {
                                    closest = data.N3;
                                    best = data;
                                }
                                record[3]++;

                            }
                        }
                        record[2]++;
                    }

                    record[1]++;
                }
                record[0]++;
                searched.Add(searching[0]);     // Add new list
                searching.RemoveAt(0);          // Move list to searched
            }
            text = "Record:";   // Output number of searches
            text += "\n" + record[0].ToString();
            text += "\n" + record[1].ToString();
            text += "\n" + record[2].ToString();
            text += "\n" + record[3].ToString();
            text += "\n" + record[4].ToString();
            label12.Text = text;
            
            // Out Data
            results = new List<string>();
            results.Add("End: " + closest);
            while (!best.After.All(num.Contains))
            {
                foreach (Data d in searched)
                {
                    if (best.Before.All(d.After.Contains))
                    {
                        results.Add(best.Log);
                        best = d;
                        break;
                    }
                }
            }
            results.Add("Start: " + OutputList(num));

            label10.Text = closest.ToString();
            OutputResults(results);
        }

    }

    // Contains data regarding each list
    public struct Data 
    {
        public Data(List<int> a, int n1, int n2, int op)
        {
            N1 = n1;
            N2 = n2;
            Op = op;
            Log = n1.ToString();
            if (op == 0)
            {
                Log += " added too ";
                N3 = n1 + n2;
            }
            else if (op == 1)
            {
                Log += " multiplyed by ";
                N3 = n1 * n2;
            }
            else if (op == 2)
            {
                Log += " subtracted from ";
                N3 = n1 - n2;
            }
            else
            {
                Log += " divided by ";
                N3 = n1 / n2;
                if (n1%n2 != 0)     //remove if a decimal number is produced
                {
                    N3 = 0;
                }
            }
            Log += n2.ToString() + " equals " + N3.ToString();
            Before = new List<int>(a);
            After = new List<int>(a);
            After.Remove(N1);
            After.Remove(N2);
            After.Add(N3);
        }
        public Data(List<int> a)
        {
            Before = a;
            After = a;
            N1 = 0;
            N2 = 0;
            N3 = 0;
            Op = 10;
            Log = "Start";
        }

        public string Log { get; }
        public int N1 { get; }
        public int N2 { get; }
        public int N3 { get; }
        public List<int> Before { get; }
        public List<int> After { get; }
        public int Op { get; }
    }
    
}
