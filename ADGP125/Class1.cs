using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ADGP125
{
    //Contains a bool to check the actions of a Player/Enemy
    //Public interface for abilities
    public interface IAbilities<T>
    {
        bool Combat(T u);

    }
    //creates a public interface for stats for Player and Enemy
    public interface IStats
    {
        //Upgrade property
        int Upgrade { get; set; }
        //Level Property
        int Lvl { get; set; }
        //Armor Property
        int Armor { get; set; }
        //Dmg Property
        int Dmg { get; set; }
        //Experience Property
        int XP { get; set; }
        //Health Property
        int HP { get; set; }
        //Max Health Property
        int MHP { get; set; }
        string Type { get; set; }
        //Name Property
        string Name { get; set; }
    }
    //Public interface for Controlling the game
    public interface IControl<T>
    {
        //Checks to see if player is victorious.
        bool Victorious(T p, T e);
        //Displays player object stats in the game.
        void Objectstats(T u);

    }
    public sealed class ControlPanel : IControl<List<Unit>>
    {
        //Constructor for Control Panel
        ControlPanel() { }

        //Private class variable
        static private ControlPanel _instance;

        //Public class varible
        static public ControlPanel instance
        {
            get
            {
                //Checks to see if instance exist
                if (_instance == null)
                {   // if it doesnt exist it creates one
                    _instance = new ControlPanel();
                }
                //returns instance.
                return _instance;
            }
        }
        public string winText;

        //Keeps track of all stats on player objects.
        public string stats;
        //checks to see who attacks first.
        public void Objectstats(List<Unit> ulist)
        {
            for (int i = 0; i < ulist.Count; i++)
            {
                //keeps up to date with player stats and updates them accordingly.

                stats += "Level: " + ulist[i].Lvl + "Health: " + ulist[i].HP + "Armor: " + ulist[i].Armor + "Exp: " + ulist[i].XP;
            }
        }

        public bool Victorious(List<Unit> listp, List<Unit> liste)
        {
            int count = 0;
            int counts = 0;

            foreach (Unit p in listp)
            {
                if (p.Life == false)
                {
                    count++;
                    if (listp.Count == count)
                    {
                        winText = "You have been Defeated!\n";
                        return true;
                    }
                }
            }
                //checks through each enemy in the list and checks to see if they are still alive.
                foreach (Unit e in liste)
                {
                    if (e.Life == false)
                    {
                        counts++;
                        if (liste.Count == count)
                        {
                            winText = "You are Victorious";
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public class Unit : IStats, IAbilities<Unit>
        {
          
            private int m_uLvl;
            private int m_uArmor;
            private int m_uXP;
            private int m_uDmg;
            private string m_uType;
            private List<Unit> m_member = new List<Unit>();
            private Unit m_uTarget;
            private bool m_uLife;
            private int m_uMHP;
            private int m_uHP;
            private int m_uSpd;
            public string m_uName;
            [XmlAnyAttribute]
            public string stuff;

            public Unit()
            {

            }
            public Unit(string name, int HP, int Armor, int dmg, int Spd, int XP, string type)
            {
                m_uName = name;
                m_uHP = HP;
                m_uArmor = Armor;
                m_uDmg = dmg;
                m_uSpd = Spd;
                m_uXP = XP;
                m_uLife = true;
                m_uLvl = 1;
            }

            //string property
            public string Name
            {
                get
                {
                    return m_uName;
                }
                set
                {
                    m_uName = value;
                }
            }
            //Max HP int property
            public int MHP
            {
                get
                {
                    return m_uMHP;
                }
                set
                {
                    m_uMHP = value;
                }
            }
            //Dmg int property
            public int Dmg
            {
                get
                {
                    return m_uDmg;
                }
                set
                {
                    m_uDmg = value;
                }
            }
            //Spd int property
            public int Spd
            {
                get
                {
                    return m_uSpd;
                }
                set
                {
                    m_uSpd = value;
                }
            }
            //Lvl int property
            public int Lvl
            {
                get
                {
                    return m_uLvl;
                }
                set
                {
                    m_uLvl = value;
                }
            }
            //bool life property
            public bool Life
            {
                get
                {
                    return m_uLife;
                }
                set
                {
                    m_uLife = value;
                }
            }


            public Unit Target
            {
                get
                {
                    return m_uTarget;
                }
                set
                {
                    m_uTarget = value;
                }
            }
        //HP int property
            public int HP
            {
                get
                {
                    return m_uHP;
                }
                set
                {
                    m_uHP = value;
                }
            }
        //Upgrade int property
            public int Upgrade
            {
                get
                {
                    return m_uLvl;
                }
                set
                {
                    m_uLvl = value;
                }
            }
        //Armor int property
            public int Armor
            {
                get
                {
                    return m_uArmor;
                }
                set
                {
                    m_uArmor = value;
                }
            }
        //Experience int property
            public int XP
            {
                get
                {
                    return m_uXP;
                }
                set
                {
                    m_uXP = value;
                }
            }
        //String type property
            public string Type
            {
                get
                {
                    return m_uType;
                }
                set
                {
                    m_uType = value;
                }
            }
            public bool Combat(Unit u)
            {
                if (u == null)
                {//prints data
                    stuff = this.Name + "Failed to connect with target\n";
                    //return false
                    return false;
                }
                //if hp is greater than 0
                if (u.HP > 0)
                {
                    //blocks dmg base on armor
                    float avg = u.Armor * 0.25f;
                    u.HP -= Dmg;
                    stuff = this.Name + " is in Combat " + u.Name + "\n";
                    stuff += u.Name + "took" + Dmg + "damage!\n\n";
                    if (u.HP <= 0)
                    {
                        stuff += u.Name + "has been slain\n";

                        u.Life = false;

                        u.HP = 0;
                        if (this.Type == "Player")
                        {
                            this.XP += u.XP;
                            this.LvlUP();

                        }
                        return false;
                    }
                    return true;
                }
                else
                {
                    stuff += u.Name + "has been defeated";
                    u.Life = false;
                    if (this.Type == "Player")
                    {
                        this.XP += u.XP;
                        this.LvlUP();
                    }
                    if (u.HP < 0)
                    {
                        u.HP = 0;
                    }
                    return false;
                }

            }
            public List<Unit> member
            {
                get
                {
                    return m_member;
                }
                set
                {
                    m_member = value;
                }
            }
            //Indicate she enemies hp and checks to see if dead or alive.
            //public Unit Indicator(List<Unit> EP)
            //{
            //    string Input;

            //    Console.WriteLine("Chose a target: \n");
            //    for (int i = 0; i < EP.Count; i ++)
            //    {
            //        Console.WriteLine(EP.ElementAt(i).Name);
            //    }

            //    Input = Console.ReadLine();
            //    for (int i = 0; i < EP.Count; i++)
            //    {
            //        if (Input == EP.ElementAt(i).Name && EP.ElementAt(i).Life == true)
            //        {
            //            Target = EP.ElementAt(i);
            //        }
            //        else if (Input == EP.ElementAt(i).Name && EP.ElementAt(i).Life == false)
            //        {
            //            //Has Detected that the target is dead.
            //            Console.WriteLine(EP.ElementAt(i).Name + "Target is Dead\n");
            //            Indicator(EP);
            //        }
            //    }
            //    return Target;
            public void LvlUP()
            {
                if (this.XP == 50)
                {
                    stuff += "\n" + this.Name + "Leveled Up!\n";
                    this.Lvl++;
                    this.XP = 0;
                    this.m_uHP += 15;
                    this.Armor += 5;
                    this.Spd += 2;
                }
            }
            public Unit encounter(List<Unit> party)
            {
                Random r = new Random();

                int index = r.Next(0, party.Count);
                Unit victim = party[index];
                if (victim.Life)
                {
                    return victim;
                }
                else
                {

                }
                return null;
            }
        }
        [XmlRoot("Party")]
        public class Party
        {
            public Party()
            {
                _units = new List<Unit>();
            }

            private List<Unit> _units;
            [XmlArray("Party"), XmlArrayItem(typeof(Unit), ElementName = "Unit")]
            public List<Unit> units
            {
                get
                {
                    return _units;
                }
                set
                {
                    _units = value;
                }
            }
        }
    }
