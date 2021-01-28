using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;

namespace Nomproject
{
    public class GreenBox
    {
        public BlockReference selRef = null;
        public string Nu;
        public string Qte;
        public string Nbr;
        public string HA;
        public string Long;
        public string Esp;
        public string g1;
        public string g2;
        public string g3;

        /*GreenBox(BlockReference a)
        { }*/

        public GreenBox() //green box block
        {
            setdata(selRef);
        }

        //vzima danni ot bloka i nastroiva globalnite parametri
        public void setdata(BlockReference GBb)
        {
            if (GBb != null)
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Editor ed = doc.Editor;
                Database db = HostApplicationServices.WorkingDatabase;
                Transaction tr = db.TransactionManager.StartTransaction();

                AttributeCollection attCol = GBb.AttributeCollection;

                using (tr)
                {
                    bool flag = false; //ima dva taga LONGUEUR zatova e tozi flag
                    if (GBb.Name == "Spécial") flag = true;
                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForRead);

                        if (attRef.Tag == "NUMERO") Nu = attRef.TextString;
                        if (attRef.Tag == "QUANTITE") Qte = attRef.TextString;
                        if (attRef.Tag == "NOMBRE") Nbr = attRef.TextString;
                        if (attRef.Tag == "NUANCE") HA = attRef.TextString;
                        if (attRef.Tag == "LONGUEUR" && flag) Long = attRef.TextString;
                        if (attRef.Tag == "ESPACEMENTS") Esp = attRef.TextString;
                        if (GBb.Name == "Spécial")
                        {
                        }
                        if (GBb.Name == "Barre libre")
                        {
                            if (attRef.Tag == "Longueur" && !flag)
                            {
                                g2 = attRef.TextString;
                                flag = true;
                            }
                        }
                        if (GBb.Name == "U")
                        {
                            if (attRef.Tag == "Longueur" && !flag)
                            {
                                g2 = attRef.TextString;
                                flag = true;
                            }
                            if (attRef.Tag == "Largeur") g3 = attRef.TextString;
                        }
                        if (GBb.Name == "Epingle")
                        {
                            if (attRef.Tag == "Longueur" && !flag)
                            {
                                g2 = attRef.TextString;
                                flag = true;
                            }
                        }
                        if (GBb.Name == "Cadre")
                        {
                            if (attRef.Tag == "Longueur" && !flag)
                            {
                                g2 = attRef.TextString;
                                flag = true;
                            }
                            if (attRef.Tag == "Largeur") g3 = attRef.TextString;
                        }
                        if (GBb.Name == "1cr45")
                        {
                            if (attRef.Tag == "Crosse gauche") g1 = attRef.TextString;
                            if (attRef.Tag == "LONGUEUR" && !flag)
                            {
                                g2 = attRef.TextString;
                                flag = true;
                            }
                        }
                        if (GBb.Name == "1cr90")
                        {
                            if (attRef.Tag == "Crosse gauche") g1 = attRef.TextString;
                            if (attRef.Tag == "LONGUEUR" && !flag)
                            {
                                g2 = attRef.TextString;
                                flag = true;
                            }
                        }
                        if (GBb.Name == "2cr45")
                        {
                            if (attRef.Tag == "Crosse gauche") g1 = attRef.TextString;
                            if (attRef.Tag == "LONGEUR" && !flag)
                            {
                                g2 = attRef.TextString;
                                flag = true;
                            }
                            if (attRef.Tag == "Crosse droite") g3 = attRef.TextString;
                        }
                        if (GBb.Name == "2cr90")
                        {
                            if (attRef.Tag == "Crosse gauche") g1 = attRef.TextString;
                            if (attRef.Tag == "LONGUEUR" && !flag)
                            {
                                g2 = attRef.TextString;
                                flag = true;
                            }
                            if (attRef.Tag == "Crosse droite") g3 = attRef.TextString;
                        }
                    }
                    tr.Commit();
                }
            }
            else
            {
                 Nu = "";
                 Qte="";
                 Nbr="";
                 HA="";
                 Long="";
                 Esp="";
                 g1="";
                 g2="";
                 g3="";
            }
            
        }

        //convert string to double
        private double stod(string str)
        {
            if (str == "")
            {
                return -1;
            }
            else
            {
                try
                {
                    return Convert.ToDouble(str);
                }
                catch (FormatException) { return -1; }
                catch (OverflowException) { return -1; }
            }
        }

        //convertira string int16
        private Int16 stoi(string str)
        {
            if (str == "")
            {
                return -1;
            }
            else
            {
                try
                {
                    return Convert.ToInt16(str);
                }
                catch (FormatException) { return -1; }
                catch (OverflowException) { return -1; }
            }
        }

        //vry6ta kilogramite na bloka

        public double totl(BlockReference GBb)
        {
            return stoi(Qte) * stoi(Nbr) * stod(Long);
        }

    }
}
