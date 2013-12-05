using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace SpaceshipProfitMaker
{
    public partial class IseeProfit : Form
    {
        //Das Variables 
        double IsogenA = 0;
        double IsogenC = 0;
        double IsogenM = 0;
        double MegacyteA = 0;
        double MegacyteC = 0;
        double MegacyteM = 0;
        double MexallonA = 0;
        double MexallonC = 0;
        double MexallonM = 0;
        double NocxiumA = 0;
        double NocxiumC = 0;
        double NocxiumM = 0;
        double PyeriteA = 0;
        double PyeriteC = 0;
        double PyeriteM = 0;
        double TritaniumA = 0;
        double TritaniumC = 0;
        double TritaniumM = 0;
        double ZydrineA = 0;
        double ZydrineC = 0;
        double ZydrineM = 0;
        String Namee;
        double Number = 0;
        double Market = 0;
        double Profit = 0;
        double Total = 0;

        //startup stuff
        public IseeProfit()
        {
            InitializeComponent();

            //Check if whole file exists
            if (File.Exists("..\\..\\ShipNames.xml"))
            {
                //Try if the file is usable at all and can be used
                try
                {
                    //filling combobox
                    XElement xelement = XElement.Load("..\\..\\ShipNames.xml");
                    IEnumerable<XElement> ships = xelement.Elements();
                    foreach (var ship in ships)
                    {
                        comboBox1.Items.Add(ship.Element("Name").Value);
                    }
                }
                catch (Exception ex) { MessageBox.Show("There is something wrong with the xml file"); }
            }
            else { MessageBox.Show("ShipNames.xml not found."); }
        }



        //Close button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //Help Button
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1) Use , instead of ."+Environment.NewLine+"2) All fields accepts numbers not text"+Environment.NewLine+
                "3)When ready use Calculate -button"+Environment.NewLine+"4) Close -button really closes this");
        }


        // The button that does things actually
        private void button3_Click(object sender, EventArgs e)
        {
            Namee = comboBox1.Text;
            //Check if something is selected from combobox
            if (Namee != null)
            {
                //Check if the file still exists
                if (File.Exists("..\\..\\ShipNames.xml"))
                {
                    //try that conversions are possible.
                    try
                    {
                        Number = Convert.ToDouble(Numbers.Text);
                        Market = Convert.ToDouble(IMP.Text);

                        IsogenM = Convert.ToDouble(IsogenMarket.Text);
                        MegacyteM = Convert.ToDouble(MegacyteMarket.Text);
                        MexallonM = Convert.ToDouble(MexallonMarket.Text);
                        NocxiumM = Convert.ToDouble(NocxiumMarket.Text);
                        PyeriteM = Convert.ToDouble(PyeriteMarket.Text);
                        TritaniumM = Convert.ToDouble(TritaniumMarket.Text);
                        ZydrineM = Convert.ToDouble(ZydrineMarket.Text);
                    }
                    catch (Exception ex) { MessageBox.Show("Use Numbers and , instead of ."); }

                    //try that XML stuff works
                    try
                    {
                        XElement xelement = XElement.Load("..\\..\\ShipNames.xml");
                        var Ships = from Ship in xelement.Elements("Ship")
                                    where (string)Ship.Element("Name") == Namee
                                    select Ship;

                        foreach (XElement xEle in Ships)
                        {
                            //try for conversions
                            try
                            {
                                IsogenA = Convert.ToDouble(xEle.Element("Isogen").Value) * Number;
                                MegacyteA = Convert.ToDouble(xEle.Element("Megacyte").Value) * Number;
                                MexallonA = Convert.ToDouble(xEle.Element("Mexallon").Value) * Number;
                                NocxiumA = Convert.ToDouble(xEle.Element("Nocxium").Value) * Number;
                                PyeriteA = Convert.ToDouble(xEle.Element("Pyerite").Value) * Number;
                                TritaniumA = Convert.ToDouble(xEle.Element("Tritanium").Value) * Number;
                                ZydrineA = Convert.ToDouble(xEle.Element("Zydrine").Value) * Number;
                            }
                            catch (Exception ex) { MessageBox.Show("Grhm.. what the crap you try to feed?"); }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("There is a problem with your XML"); }

                    //Down to number grinding
                    //Mineral Costs
                    IsogenC = IsogenA * IsogenM * Number;
                    MegacyteC = MegacyteA * MegacyteM * Number;
                    MexallonC = MexallonA * MexallonM * Number;
                    NocxiumC = NocxiumA * NocxiumM * Number;
                    PyeriteC = PyeriteA * PyeriteM * Number;
                    TritaniumC = TritaniumA * TritaniumM * Number;
                    ZydrineC = ZydrineA * ZydrineM * Number;

                    //Total cost and Profit
                    Total = IsogenC + MegacyteC + MexallonC + NocxiumC + PyeriteC + TritaniumC + ZydrineC;
                    Profit = Market * Number - Total;

                    TotalCost.Text = (Total).ToString();
                    Cash.Text = Profit.ToString();

                    //Show Mineral needs
                    IsogenAmount.Text = IsogenA.ToString();
                    MegacyteAmount.Text = MegacyteA.ToString();
                    MexallonAmount.Text = MexallonA.ToString();
                    NocxiumAmount.Text = NocxiumA.ToString();
                    PyeriteAmount.Text = PyeriteA.ToString();
                    TritaniumAmount.Text = TritaniumA.ToString();
                    ZydrineAmount.Text = ZydrineA.ToString();

                    //Show Mineral costs
                    IsogenCost.Text = IsogenC.ToString();
                    MegacyteCost.Text = MegacyteC.ToString();
                    MexallonCost.Text = MexallonC.ToString();
                    NocxiumCost.Text = NocxiumC.ToString();
                    PyeriteCost.Text = PyeriteC.ToString();
                    TritaniumCost.Text = PyeriteC.ToString();
                    ZydrineCost.Text = ZydrineC.ToString();
                }
                else { MessageBox.Show("Get the damn file now"); }
            }
            else { MessageBox.Show("Select what you want to make"); }
        }   
    }
}
