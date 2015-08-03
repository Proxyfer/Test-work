using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cash_Machine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int stroke = new int();
        public int all = 0;
        public string[] values = new string[5] {"$1=", "$5=", "$10=", "$20=", "$100="};
        public int[] cash = new int[5];
                                    /*  0 = 1$
                                        1 = 5$
                                        2 = 10$
                                        3 = 20$
                                        4 = 100$  */

        public void Cash_Show(int index)
        {
            switch (index)
            {
                case 1:
                    listBox1.Items.Add("Inventory:");
                    for (int i = 0; i < 5; i++) listBox1.Items.Add(values[i] + cash[i]);
                    /*
                     * cash[0]=value;
                     * cash[1]=value;
                     * cash[2]=value;
                     * cash[3]=value;
                     * cash[4]=value; 
                     */
                      
                    all = cash[0] + cash[1] * 5 + cash[2] * 10 + cash[3] * 20 + cash[4] * 100;
                    break;                    
                case 2:
                    listBox1.Items.Add("You enter invalid bet, all bets must be in increments of $1 and more than 0"); 
                    break;
                case 3: 
                    listBox1.Items.Add("Theare not enough money in ATM, please enter another value");
                    break;
            }
            
            textBox1.Text = "";
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.SelectedItem = null;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Cash_out=0;
            try
            {
                Cash_out = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
            }

            if (Cash_out <= 0)
                Cash_Show(2);
            else if (Cash_out > all)
                Cash_Show(3);
            else if (Cash_out == all)
            {
                for (int i = 0; i < 5; i++) cash[i] = 0;
                Cash_Show(1);
            }
            else
            {
                int[] additional_cash = new int[5];
                Array.Copy(cash,additional_cash, 5);
                while (Cash_out >= 1)
                {
                    while ((Cash_out >= 5)&(additional_cash[1] > 0))
                    {
                        while ((Cash_out >= 10)&(additional_cash[2] > 0))
                        {
                            while ((Cash_out >= 20) & (additional_cash[3] > 0))
                            {
                                while ((Cash_out >= 100) & (additional_cash[4] > 0))
                                {
                                        additional_cash[4] -= 1;
                                        Cash_out -= 100;
                                        
                                }
                                if (Cash_out >= 20)
                                {
                                    additional_cash[3] -= 1;
                                    Cash_out -= 20;
                                }
                               
                            }
                            if (Cash_out >= 10)
                            {
                                additional_cash[2] -= 1;
                                Cash_out -= 10;
                            }
                         
                        }
                        if (Cash_out >= 5)
                        {
                            additional_cash[1] -= 1;
                            Cash_out -= 5;
                        }
                                               
                    }
                    if (Cash_out >= 1)
                    {
                        additional_cash[0] -= 1;
                        Cash_out -= 1;
                    }
                }

                if (additional_cash[0] < 0) Cash_Show(3);
                else
                {
                    Array.Copy(additional_cash, cash, 5);
                    Cash_Show(1);
                }
                
            }
                        
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)  cash[i] = 10;
            Cash_Show(1); 
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);
        }
    }
}
