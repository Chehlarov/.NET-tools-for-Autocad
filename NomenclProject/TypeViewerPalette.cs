using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Runtime;
using System;
using Nomproject;
using Autodesk.AutoCAD.Geometry;


namespace Nomproject
{
    public class TypeViewerPalette
    {
        // We cannot derive from PaletteSet
        // so we contain it
        static PaletteSet ps;
        
        // We need to make the textbox available
        // via a static member
        static TypeViewerControl tvc;
        static options opts;
        
        static HA bd;

        //public BlockReference selRef = null;
        GreenBox GB;
        
        
        public TypeViewerPalette()
        {
            tvc = new TypeViewerControl();
            opts = new options();
            bd = new HA();
            GB = new GreenBox();
            

            tvc.textBox1.LostFocus += new System.EventHandler(tvc_Leave);
            tvc.textBox2.LostFocus += new System.EventHandler(tvc_Leave);
            tvc.textBox3.LostFocus += new System.EventHandler(tvc_Leave);
            tvc.textBox4.LostFocus += new System.EventHandler(tvc_Leave);
            tvc.textBox5.LostFocus += new System.EventHandler(tvc_Leave);
            tvc.textBox6.LostFocus += new System.EventHandler(tvc_Leave);
            tvc.textBox7.LostFocus += new System.EventHandler(tvc_Leave);
            tvc.textBox8.LostFocus += new System.EventHandler(tvc_Leave);
            tvc.textBox9.LostFocus += new System.EventHandler(tvc_Leave);
            tvc.Rbutt.Click += new System.EventHandler(Rbutt_Click);
            tvc.Lbutt1.Click += new EventHandler(Lbutt1_Click);
            tvc.Lbutt2.Click += new EventHandler(Lbutt2_Click);
            tvc.Lbutt3.Click += new EventHandler(Lbutt3_Click);
            tvc.b2cr45.Click += new EventHandler(b2cr45_Click);
            tvc.b2cr90.Click += new EventHandler(b2cr90_Click);
            tvc.b1cr45.Click += new EventHandler(b1cr45_Click);
            tvc.b1cr90.Click += new EventHandler(b1cr90_Click);
            tvc.bcadr.Click += new EventHandler(bcadr_Click);
            tvc.bubar.Click += new EventHandler(bubar_Click);
            tvc.bepingle.Click += new EventHandler(bepingle_Click);
            tvc.count.Click += new EventHandler(count_Click);
            tvc.count_p.Click += new EventHandler(count_p_Click);
            tvc.R10butt.Click += new EventHandler(R10butt_Click);
        }

        double recL(int flavec10)
        {
            //flavec10 = 0 ne dobavq dobavka za 10f
            //flavec10 = 1 dobavq dobavka za 10f
            double rez = -1;

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            AttributeCollection attCol = GB.selRef.AttributeCollection;
            DocumentLock loc = doc.LockDocument();

            //bool flag = false;
            //if (selRef.Name == "Spécial") flag = true;
            using (loc)
            {
                Transaction tr = doc.TransactionManager.StartTransaction();
                using (tr)
                {
                    try
                    {
                        if (GB.selRef.Name == "Spécial")
                        {
                            ed.WriteMessage("Cannot calculate!!!");
                            
                        }
                        if (GB.selRef.Name == "Barre libre")
                            rez= Convert.ToDouble(tvc.textBox8.Text);
                        if (GB.selRef.Name == "U")
                        {
                            rez = 2 * Convert.ToDouble(tvc.textBox8.Text) + Convert.ToDouble(tvc.textBox9.Text);
                        }
                        if (GB.selRef.Name == "Epingle")
                        {
                            rez= Convert.ToDouble(tvc.textBox8.Text) + bd.dlepingle(Convert.ToInt16(tvc.textBox4.Text)) + Convert.ToInt16(tvc.textBox4.Text)*flavec10;
                        }
                        if (GB.selRef.Name == "Cadre")
                        {
                            rez=
                                2 * Convert.ToDouble(tvc.textBox8.Text) +
                                2 * Convert.ToDouble(tvc.textBox9.Text) +
                                bd.dlcadre(Convert.ToInt16(tvc.textBox4.Text)) +
                                Convert.ToInt16(tvc.textBox4.Text) * flavec10
                                ;
                        }
                        if (GB.selRef.Name == "1cr45")
                        {
                            rez=
                                Convert.ToDouble(tvc.textBox8.Text) +
                                Convert.ToDouble(tvc.textBox7.Text) -
                                Convert.ToDouble(tvc.textBox4.Text) * 1 +
                                bd.dlcr45(Convert.ToInt16(tvc.textBox4.Text))
                                ;
                        }
                        if (GB.selRef.Name == "1cr90")
                        {
                            rez=
                                Convert.ToDouble(tvc.textBox8.Text) +
                                Convert.ToDouble(tvc.textBox7.Text) +
                                bd.dlcr90(Convert.ToInt16(tvc.textBox4.Text))
                                ;
                        }
                        if (GB.selRef.Name == "2cr45")
                        {
                            rez=
                                Convert.ToDouble(tvc.textBox7.Text) * 1 +
                                Convert.ToDouble(tvc.textBox8.Text) +
                                Convert.ToDouble(tvc.textBox9.Text) * 1 -
                                Convert.ToDouble(tvc.textBox4.Text) * 2 +
                                bd.dlcr45(Convert.ToInt16(tvc.textBox4.Text)) * 2
                                ;
                        }
                        if (GB.selRef.Name == "2cr90")
                        {
                            rez=
                                Convert.ToDouble(tvc.textBox7.Text) * 1 +
                                Convert.ToDouble(tvc.textBox8.Text) +
                                Convert.ToDouble(tvc.textBox9.Text) * 1 +
                                bd.dlcr90(Convert.ToInt16(tvc.textBox4.Text)) * 2
                                ;
                        }
                        
                    }//end try
                    catch (FormatException) 
                    {
                        ed.WriteMessage("Unable to convert to a Double");
                        
                    }
                    catch (OverflowException) 
                    {
                        ed.WriteMessage("Unable to convert to a Double");
                           
                    }
                    tr.Commit();
                    return rez;
                }
            }
           

        }//end recL

        void R10butt_Click(object sender, EventArgs e)
        {
            tvc.textBox5.Text = Ltostr(recL(1));
            Updatedef();
        } //end R10butt_Click

        void count_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            double dis = getdist(2);
            double esp = 0;
            try
            {
                esp = Convert.ToDouble(tvc.textBox6.Text);
            }
            catch (FormatException) { ed.WriteMessage("Unable to convert to a Double"); }
            catch (OverflowException) { ed.WriteMessage("Unable to convert to a Double"); }

            double nb = Math.Ceiling(dis / esp);
            tvc.textBox3.Text = Convert.ToString(nb);
            Updatedef();
        }
        void count_p_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            double dis = getdist(2);
            double esp = 0;
            try
            {
                esp = Convert.ToDouble(tvc.textBox6.Text);
            }
            catch (FormatException) { ed.WriteMessage("Unable to convert to a Double"); }
            catch (OverflowException) { ed.WriteMessage("Unable to convert to a Double"); }

            double nb = Math.Ceiling(dis / esp) + 1;
            tvc.textBox3.Text = Convert.ToString(nb);
            Updatedef();
        }

        void bepingle_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Int16 ha = 0;
            try
            {
                ha = Convert.ToInt16(tvc.newHA.Text);
                DrawEpingle(ha);
            }
            catch (FormatException) { ed.WriteMessage("Unable to convert to a Int16"); }
            catch (OverflowException) { ed.WriteMessage("Unable to convert to a Int16"); }
        }

        void bubar_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Int16 ha = 0;
            try
            {
                ha = Convert.ToInt16(tvc.newHA.Text);
                DrawUbar(ha);
            }
            catch (FormatException) { ed.WriteMessage("Unable to convert to a Int16"); }
            catch (OverflowException) { ed.WriteMessage("Unable to convert to a Int16"); }
        }

        void bcadr_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Int16 ha = 0;
            try
            {
                ha = Convert.ToInt16(tvc.newHA.Text);
                DrawCadre(ha);
            }
            catch (FormatException) { ed.WriteMessage("Unable to convert to a Int16"); }
            catch (OverflowException) { ed.WriteMessage("Unable to convert to a Int16"); }
        }

        void b1cr90_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Int16 ha = 0;
            try
            {
                ha = Convert.ToInt16(tvc.newHA.Text);
                Draw1cr90(ha);
            }
            catch (FormatException) { ed.WriteMessage("Unable to convert to a Int16"); }
            catch (OverflowException) { ed.WriteMessage("Unable to convert to a Int16"); }
        }

        void b1cr45_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Int16 ha = 0;
            try
            {
                ha = Convert.ToInt16(tvc.newHA.Text);
                Draw1cr45(ha);
            }
            catch (FormatException) { ed.WriteMessage("Unable to convert to a Int16"); }
            catch (OverflowException) { ed.WriteMessage("Unable to convert to a Int16"); }
        }

        void b2cr90_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Int16 ha = 0;
            try
            {
                ha = Convert.ToInt16(tvc.newHA.Text);
                Draw2cr90(ha);
            }
            catch (FormatException) { ed.WriteMessage("Unable to convert to a Int16"); }
            catch (OverflowException) { ed.WriteMessage("Unable to convert to a Int16"); }
        }

        void b2cr45_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Int16 ha = 0;
            try
            {
                ha = Convert.ToInt16(tvc.newHA.Text);
                Draw2cr45(ha);
            }
            catch (FormatException) { ed.WriteMessage("Unable to convert to a Int16"); }
            catch (OverflowException) { ed.WriteMessage("Unable to convert to a Int16"); }  
        }

        public void Show()
        {
            if (ps == null)
            {
                ps = new PaletteSet("Nomenclature");
                ps.Style =
                  PaletteSetStyles.NameEditable |
                  PaletteSetStyles.ShowPropertiesMenu |
                  PaletteSetStyles.ShowAutoHideButton |
                  PaletteSetStyles.ShowCloseButton;
                ps.MinimumSize =
                  new System.Drawing.Size(100, 300);
                ps.Add("Nomenclature", tvc);
                ps.Add("Options", opts);
            }
            if (ps.Visible == false) 
                ps.Visible = true;
            else 
                ps.Visible = false;
            annulateText();

        }

        void Lbutt1_Click(object sender, EventArgs e)
        {
            double dis = getdist(1);
            if (dis != -1) tvc.textBox7.Text = Convert.ToString(dis);
            Updatedef();
        }

        void Lbutt2_Click(object sender, EventArgs e)
        {
            double dis = getdist(1);
            if (dis != -1) tvc.textBox8.Text = Convert.ToString(dis);
            Updatedef();
        }

        void Lbutt3_Click(object sender, EventArgs e)
        {
            double dis = getdist(1);
            if (dis != -1) tvc.textBox9.Text = Convert.ToString(dis);
            Updatedef();
        }

        private double getdist(int type)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
 
            //ima problemi s focusa ne se dargi mnogo dobre
            //ps.Visible = false;
            // ps.KeepFocus = false;
           /*
            //proba
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Transaction tr = db.TransactionManager.StartTransaction();
            
            BlockReference tmpr = selRef;
           
            using (tr)
            {
                LayoutManager ltman = LayoutManager.Current;
                
                //ed.WriteMessage("\nYou are in {0}", ltman.CurrentLayout);
                string layout = "Model"; // <-- layout name you need 
                ltman.CurrentLayout = layout;
                //ed.WriteMessage("\nNow you've switched to {0}", ltman.CurrentLayout);
                tr.Commit();
            }
            selRef = tmpr;
            SetObjectText(selRef);
            
            //end proba
            */
            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");

            pPtOpts.Message = "\nEnter the start point: ";
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p1 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return -1;

            pPtOpts.Message = "\nEnter the end point: ";
            pPtOpts.UseBasePoint = true;
            pPtOpts.BasePoint = p1;
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p2 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return -1;

            double d = p1.DistanceTo(p2);


            //PromptDoubleResult d = ed.GetDistance("\nDistance: ");
            ed.WriteMessage("\n"+d);

            double sc=0;//scale
            if (opts.scbuttcm.Checked) sc = 1;
            if (opts.scbuttm.Checked) sc = 100;
            if (opts.scbuttmm.Checked) sc = 0.1;
            if (d > 0 && type==1)
            {
                if (opts.Lrad1.Checked) return Math.Truncate(d*sc / 5) * 5 + 5;
                if (opts.Lrad2.Checked) return Math.Ceiling(d * sc);
                if (opts.Lrad3.Checked) return Math.Round(d * sc);
                if (opts.Lrad4.Checked) return Math.Floor(d * sc);
                if (opts.Lrad5.Checked) return Math.Truncate(d*sc / 5) * 5;
            }
            if (d > 0 && type == 2)
            {
                return Math.Ceiling(d * sc);
            }
            return -1; 
        }

        private string Ltostr(double arg)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            string str = "";
            //ed.WriteMessage(Convert.ToString( "\n" + arg % 100));
            if (arg % 10 < 0.001) str = "0";
            if (arg % 100 < 0.001) str = ".00";
            str = Convert.ToString(arg*0.01) + str;
            ed.WriteMessage("\n" + str);
            return str;
        }
        
        void Rbutt_Click(object sender, System.EventArgs e)
        {
            tvc.textBox5.Text = Ltostr(recL(0));
            Updatedef();
        }

        void tvc_Leave(object sender, System.EventArgs e)
        {
            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nEVENT LostFocus on some textBox");
            //throw new System.NotImplementedException();
            Updatedef();
        }

        public void annulateText()
        {
                GB.selRef = null;
                GB.setdata(GB.selRef);
             
                tvc.Rbutt.Enabled = false;
                tvc.R10butt.Enabled = false;
                tvc.textBox1.ReadOnly = true;
                tvc.textBox1.Text = " ";
                tvc.textBox2.ReadOnly = true;
                tvc.textBox2.Text = " ";
                tvc.textBox3.ReadOnly = true;
                tvc.textBox3.Text = " ";
                tvc.textBox4.ReadOnly = true;
                tvc.textBox4.Text = " ";
                tvc.textBox5.ReadOnly = true;
                tvc.textBox5.Text = " ";
                tvc.textBox6.ReadOnly = true;
                tvc.textBox6.Text = " ";
                tvc.textBox7.ReadOnly = true;
                tvc.textBox7.Text = " ";
                tvc.Lbutt1.Enabled = false;
                tvc.textBox8.ReadOnly = true;
                tvc.textBox8.Text = " ";
                tvc.Lbutt2.Enabled = false;
                tvc.textBox9.ReadOnly = true;
                tvc.textBox9.Text = " ";
                tvc.Lbutt3.Enabled = false;

                tvc.label10.Text = "no";

                tvc.textBox5.BackColor = System.Drawing.SystemColors.Control;
            
        }

        public void activateText()
        {
            tvc.Rbutt.Enabled = true;
            tvc.R10butt.Enabled = true;
            tvc.textBox1.ReadOnly = false;
            tvc.textBox2.ReadOnly = false;
            tvc.textBox3.ReadOnly = false;
            tvc.textBox4.ReadOnly = false;
            tvc.textBox5.ReadOnly = false;
            tvc.textBox6.ReadOnly = false;
            tvc.textBox7.ReadOnly = false;
            tvc.Lbutt1.Enabled = true;
            tvc.textBox8.ReadOnly = false;
            tvc.Lbutt2.Enabled = true;
            tvc.textBox9.ReadOnly = false;
            tvc.Lbutt3.Enabled = true;
        }

        //predifenira zelenite stoinosti
        public void Updatedef()
        {
            //Tuk 6te se updatva definiciqta na bloka
            if (GB.selRef != null)
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Editor ed = doc.Editor;
                Database db = HostApplicationServices.WorkingDatabase;

                AttributeCollection attCol = GB.selRef.AttributeCollection;

                DocumentLock loc = doc.LockDocument();

                //ed.WriteMessage("vlezi v updatedef");
                bool flag = false;
                if (GB.selRef.Name == "Spécial") flag = true;
                using (loc)
                {
                    Transaction tr = doc.TransactionManager.StartTransaction();
                    using (tr)
                    {
                        foreach (ObjectId attId in attCol)
                        {
                            AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForWrite);
                            //ed.WriteMessage("\n" + attRef.Tag);
                            if (attRef.Tag == "NUMERO") attRef.TextString = tvc.textBox1.Text;
                            if (attRef.Tag == "QUANTITE") attRef.TextString = tvc.textBox2.Text;
                            if (attRef.Tag == "NOMBRE") attRef.TextString = tvc.textBox3.Text;
                            if (attRef.Tag == "NUANCE") attRef.TextString = tvc.textBox4.Text;
                            if (attRef.Tag == "LONGUEUR" && flag) attRef.TextString = tvc.textBox5.Text; //NB!!!
                            if (attRef.Tag == "ESPACEMENTS") attRef.TextString = tvc.textBox6.Text;

                            if (attRef.Tag == "Longueur" && !flag)
                            {
                                attRef.TextString = tvc.textBox8.Text;
                                flag = true;
                            }
                            if (attRef.Tag == "Largeur") attRef.TextString = tvc.textBox9.Text;
                            if (attRef.Tag == "Crosse gauche") attRef.TextString = tvc.textBox7.Text;
                            if (attRef.Tag == "LONGUEUR" && !flag)
                            {
                                attRef.TextString = tvc.textBox8.Text;//NB
                                flag = true;
                            }
                            if (attRef.Tag == "LONGEUR" && !flag)
                            {
                                attRef.TextString = tvc.textBox8.Text;//NB
                                flag = true;
                            }
                            if (attRef.Tag == "Crosse droite") attRef.TextString = tvc.textBox9.Text;


                            //if (attRef.Tag == "Longueur") attRef.TextString = tvc.textBox8.Text;
                        }


                        try
                        {

                            tvc.label10.Text = string.Concat(Convert.ToString(Math.Round(Math.PI * 0.25 * Math.Pow(Convert.ToDouble(tvc.textBox4.Text), 2) / Convert.ToDouble(tvc.textBox6.Text), 2)), "cm2");
                        }
                        catch
                        {
                            tvc.label10.Text = "no";
                        }

                        ed.WriteMessage("\nSravnqvane na " + recL(0) + " sys " + Convert.ToDouble(tvc.textBox5.Text) * 100);
                        if (Math.Abs( recL(0) - Convert.ToDouble(tvc.textBox5.Text) * 100) >= 0.1 )
                        {
                            tvc.textBox5.BackColor = System.Drawing.Color.Red;
                            if (Math.Abs( recL(1) - Convert.ToDouble(tvc.textBox5.Text) * 100) < 0.1) tvc.textBox5.BackColor = System.Drawing.Color.Yellow;
                        }
                        else
                        {
                            tvc.textBox5.BackColor = System.Drawing.Color.White;
                        }
                        tr.Commit();
                    }
                }
            }
        }

        //slaga textovete v controla
        public void SetObjectText()
        {
            if (GB.selRef == null)
            {
                annulateText();
            }
            else
            {
                activateText();
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                Database db = HostApplicationServices.WorkingDatabase;
                Transaction tr = db.TransactionManager.StartTransaction();
                AttributeCollection attCol = GB.selRef.AttributeCollection;

                bool flag = false; //ima dva taga LONGUEUR zatova e tozi flag
                if (GB.selRef.Name == "Spécial") flag = true;
                foreach (ObjectId attId in attCol)
                {
                    AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForRead);
                    //string str = ("\n  Attribute Tag: " + attRef.Tag + "\n    Attribute String: " + attRef.TextString);
                    //ed.WriteMessage(str);
                    if (attRef.Tag == "NUMERO") tvc.textBox1.Text = attRef.TextString;
                    if (attRef.Tag == "QUANTITE") tvc.textBox2.Text = attRef.TextString;
                    if (attRef.Tag == "NOMBRE") tvc.textBox3.Text = attRef.TextString;
                    if (attRef.Tag == "NUANCE") tvc.textBox4.Text = attRef.TextString;
                    if (attRef.Tag == "LONGUEUR" && flag) tvc.textBox5.Text = attRef.TextString;
                    if (attRef.Tag == "ESPACEMENTS") tvc.textBox6.Text = attRef.TextString;
                    if (GB.selRef.Name == "Spécial")
                    {
                        tvc.textBox7.Text = "-";
                        tvc.textBox7.ReadOnly = true;
                        tvc.textBox8.Text = "-";
                        tvc.textBox8.ReadOnly = true;
                        tvc.textBox9.Text = "-";
                        tvc.textBox9.ReadOnly = true;
                    }
                    if (GB.selRef.Name == "Barre libre")
                    {
                        tvc.textBox7.Text = "-";
                        tvc.textBox7.ReadOnly = true;
                        if (attRef.Tag == "Longueur" && !flag)
                        {
                            tvc.textBox8.Text = attRef.TextString;
                            flag = true;
                        }
                        tvc.textBox9.Text = "-";
                        tvc.textBox9.ReadOnly = true;
                    }
                    if (GB.selRef.Name == "U")
                    {
                        tvc.textBox7.Text = "-";
                        tvc.textBox7.ReadOnly = true;
                        if (attRef.Tag == "Longueur" && !flag)
                        {
                            tvc.textBox8.Text = attRef.TextString;
                            flag = true;
                        }
                        if (attRef.Tag == "Largeur") tvc.textBox9.Text = attRef.TextString;
                    }
                    if (GB.selRef.Name == "Epingle")
                    {
                        tvc.textBox7.Text = "-";
                        tvc.textBox7.ReadOnly = true;
                        if (attRef.Tag == "Longueur" && !flag)
                        {
                            tvc.textBox8.Text = attRef.TextString;
                            flag = true;
                        }
                        tvc.textBox9.Text = "-";
                        tvc.textBox9.ReadOnly = true;
                    }
                    if (GB.selRef.Name == "Cadre")
                    {
                        tvc.textBox7.Text = "-";
                        tvc.textBox7.ReadOnly = true;
                        if (attRef.Tag == "Longueur" && !flag)
                        {
                            tvc.textBox8.Text = attRef.TextString;
                            flag = true;
                        }
                        if (attRef.Tag == "Largeur") tvc.textBox9.Text = attRef.TextString;
                    }
                    if (GB.selRef.Name == "1cr45")
                    {
                        if (attRef.Tag == "Crosse gauche") tvc.textBox7.Text = attRef.TextString;
                        if (attRef.Tag == "LONGUEUR" && !flag)
                        {
                            tvc.textBox8.Text = attRef.TextString;
                            flag = true;
                        }
                        tvc.textBox9.Text = "-";
                        tvc.textBox9.ReadOnly = true;
                        
                    }
                    if (GB.selRef.Name == "1cr90")
                    {
                        if (attRef.Tag == "Crosse gauche") tvc.textBox7.Text = attRef.TextString;
                        if (attRef.Tag == "LONGUEUR" && !flag)
                        {
                            tvc.textBox8.Text = attRef.TextString;
                            flag = true;
                        }
                        tvc.textBox9.Text = "-";
                        tvc.textBox9.ReadOnly = true;
                        
                    }
                    if (GB.selRef.Name == "2cr45")
                    {
                        if (attRef.Tag == "Crosse gauche") tvc.textBox7.Text = attRef.TextString;
                        if (attRef.Tag == "LONGEUR" && !flag)
                        {
                            tvc.textBox8.Text = attRef.TextString;
                            flag = true;
                        }
                        if (attRef.Tag == "Crosse droite") tvc.textBox9.Text = attRef.TextString;
                    }
                    if (GB.selRef.Name == "2cr90")
                    {
                        if (attRef.Tag == "Crosse gauche") tvc.textBox7.Text = attRef.TextString;
                        if (attRef.Tag == "LONGUEUR" && !flag)
                        {
                            tvc.textBox8.Text = attRef.TextString;
                            flag = true;
                        }
                        if (attRef.Tag == "Crosse droite") tvc.textBox9.Text = attRef.TextString; 
                    }
                }
                Updatedef();
                //tezi otdolu maj trqbva da se razkarat
                try
                {
                    
                    tvc.label10.Text = string.Concat( Convert.ToString(Math.Round(Math.PI * 0.25 * Math.Pow(Convert.ToDouble(tvc.textBox4.Text), 2) / Convert.ToDouble(tvc.textBox6.Text), 2)), "cm2");
                }
                catch 
                {
                    tvc.label10.Text = "no";
                }
                tr.Commit();
            }
        }

        //pri promqna na selekciqta se izvikva
        public void SetSS(SelectionSet ss)
        {
            ObjectId[] idArraych = ss.GetObjectIds();
            if (idArraych.Length == 0)
            {
                annulateText();
            }
            else
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                Database db = HostApplicationServices.WorkingDatabase;
                Transaction tr = db.TransactionManager.StartTransaction();

                try
                {

                    ObjectId[] idArray = ss.GetObjectIds();
                    //BlockTableRecord selbtr = null;
                    GB.selRef = null;

                    foreach (ObjectId blkId in idArray)
                    {
                        /*if (blkId.ObjectClass.DxfName == "INSERT")*/
                        DBObject obj = tr.GetObject(blkId, OpenMode.ForRead);
                        if (obj is BlockReference)
                        {
                            BlockReference blkRef = (BlockReference)tr.GetObject(blkId, OpenMode.ForRead);

                            if (blkRef.Name == "Spécial" ||
                               blkRef.Name == "Barre libre" ||
                               blkRef.Name == "U" ||
                               blkRef.Name == "Epingle" ||
                               blkRef.Name == "Cadre" ||
                               blkRef.Name == "1cr45" ||
                               blkRef.Name == "1cr90" ||
                               blkRef.Name == "2cr45" ||
                               blkRef.Name == "2cr90")
                            {
                                if (GB.selRef == null)
                                {
                                    GB.selRef = blkRef;
                                }
                                else
                                {
                                    GB.selRef = null;
                                    break;
                                }
                            }
                        }
                    } // end foreach
                    GB.setdata(GB.selRef);
                    SetObjectText();
                    tr.Commit();
                }

                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    ed.WriteMessage(("Exception: " + ex.Message));
                }

                finally
                {
                    tr.Dispose();
                }
            }//else

        }

        public void Draw2cr45(Int16 ha)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            DocumentLock loc = acDoc.LockDocument();

            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");

            pPtOpts.Message = "\nEnter the start point: ";
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p1 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            pPtOpts.Message = "\nEnter the end point: ";
            pPtOpts.UseBasePoint = true;
            pPtOpts.BasePoint = p1;
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p2 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            double sc = 0;//scale
            if (opts.scbuttcm.Checked) sc = 1;
            if (opts.scbuttm.Checked) sc = 0.01;
            if (opts.scbuttmm.Checked) sc = 10;

            PromptDoubleResult offR;
            PromptDoubleOptions offOpts = new PromptDoubleOptions("");
            offOpts.Message = "\nEnter concrete cover: ";
            offR = acDoc.Editor.GetDouble(offOpts);
            double off = offR.Value;

            double dist = p1.DistanceTo(p2);
            acDoc.Editor.WriteMessage("\nExact distance: " + Convert.ToString(dist));
            using (loc)
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    BlockTable acBlkTbl;
                    BlockTableRecord acBlkTblRec;

                    // Open Model space for write
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                 OpenMode.ForRead) as BlockTable;

                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                    OpenMode.ForWrite) as BlockTableRecord;

                    //matrix for converting coordinates
                    Matrix3d newMatrix = new Matrix3d();
                    newMatrix = Matrix3d.AlignCoordinateSystem(
                                                         new Point3d(0, 0, 0),
                                                         new Vector3d(1, 0, 0),
                                                         new Vector3d(0, 1, 0),
                                                         new Vector3d(0, 0, 1),
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Origin,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Xaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Yaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis
                                                         );

                    Matrix3d newMatrix2 = new Matrix3d();
                    Vector2d dirtemp = new Vector2d(p2.X - p1.X, p2.Y - p1.Y);

                    double angle = dirtemp.GetAngleTo(new Vector2d(1, 0));
                    if (p2.Y - p1.Y < 0) angle = 2 * Math.PI - angle;
                    newMatrix2 = Matrix3d.Rotation(angle,
                                                  acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis,
                                                  p1.TransformBy(newMatrix));

                    double r = bd.rancr(ha) * sc;
                    double tan3375 = Math.Tan(3 * Math.PI / 16);
                    double sin45 = Math.Sin(Math.PI / 4);
                    // Create a polyline
                    Polyline acPoly = new Polyline();
                    acPoly.SetDatabaseDefaults();

                    acPoly.AddVertexAt(0, new Point2d(p1.X + off + r * (1 - sin45) + 1.0 * sc * ha * sin45, p1.Y + off + r * (1 + sin45) + 1.0 * sc * ha * sin45), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(p1.X + off + r * (1 - sin45), p1.Y + off + r * (1 + sin45)), tan3375, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(p1.X + off + r, p1.Y + off), 0, 0, 0);
                    acPoly.AddVertexAt(3, new Point2d(p1.X + dist - off - r, p1.Y + off), tan3375, 0, 0);
                    acPoly.AddVertexAt(4, new Point2d(p1.X + dist - off - r * (1 - sin45), p1.Y + off + r * (1 + sin45)), 0, 0, 0);
                    acPoly.AddVertexAt(5, new Point2d(p1.X + dist - off - r * (1 - sin45) - 1.0 * sc * ha * sin45, p1.Y + off + r * (1 + sin45) + 1.0 * sc * ha * sin45), 0, 0, 0);

                    acPoly.TransformBy(newMatrix);
                    acPoly.TransformBy(newMatrix2);


                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);

                    acTrans.Commit();

                } //end Transaction
            }//end lock
        }

        public void Draw1cr45(Int16 ha)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            DocumentLock loc = acDoc.LockDocument();

            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");

            pPtOpts.Message = "\nEnter the start point: ";
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p1 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            pPtOpts.Message = "\nEnter the end point: ";
            pPtOpts.UseBasePoint = true;
            pPtOpts.BasePoint = p1;
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p2 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            double sc = 0;//scale
            if (opts.scbuttcm.Checked) sc = 1;
            if (opts.scbuttm.Checked) sc = 0.01;
            if (opts.scbuttmm.Checked) sc = 10;

            PromptDoubleResult offR;
            PromptDoubleOptions offOpts = new PromptDoubleOptions("");
            offOpts.Message = "\nEnter concrete cover: ";
            offR = acDoc.Editor.GetDouble(offOpts);
            double off = offR.Value;

            double dist = p1.DistanceTo(p2);
            using (loc)
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    BlockTable acBlkTbl;
                    BlockTableRecord acBlkTblRec;

                    // Open Model space for write
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                 OpenMode.ForRead) as BlockTable;

                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                    OpenMode.ForWrite) as BlockTableRecord;

                    //matrix for converting coordinates
                    Matrix3d newMatrix = new Matrix3d();
                    newMatrix = Matrix3d.AlignCoordinateSystem(
                                                         new Point3d(0, 0, 0),
                                                         new Vector3d(1, 0, 0),
                                                         new Vector3d(0, 1, 0),
                                                         new Vector3d(0, 0, 1),
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Origin,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Xaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Yaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis
                                                         );

                    Matrix3d newMatrix2 = new Matrix3d();
                    Vector2d dirtemp = new Vector2d(p2.X - p1.X, p2.Y - p1.Y);

                    double angle = dirtemp.GetAngleTo(new Vector2d(1, 0));
                    if (p2.Y - p1.Y < 0) angle = 2 * Math.PI - angle;
                    newMatrix2 = Matrix3d.Rotation(angle,
                                                  acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis,
                                                  p1.TransformBy(newMatrix));

                    double r = bd.rancr(ha) * sc;
                    double tan3375 = Math.Tan(3 * Math.PI / 16);
                    double sin45 = Math.Sin(Math.PI / 4);
                    // Create a polyline
                    Polyline acPoly = new Polyline();
                    acPoly.SetDatabaseDefaults();

                    acPoly.AddVertexAt(0, new Point2d(p1.X + off + r * (1 - sin45) + 1.0 * sc * ha * sin45, p1.Y + off + r * (1 + sin45) + 1.0 * sc * ha * sin45), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(p1.X + off + r * (1 - sin45), p1.Y + off + r * (1 + sin45)), tan3375, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(p1.X + off + r, p1.Y + off), 0, 0, 0);
                    acPoly.AddVertexAt(3, new Point2d(p1.X + dist, p1.Y + off), 0, 0, 0);

                    acPoly.TransformBy(newMatrix);
                    acPoly.TransformBy(newMatrix2);


                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);

                    acTrans.Commit();

                } //end Transaction
            } //loc
        }

        public void Draw2cr90(Int16 ha)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            DocumentLock loc = acDoc.LockDocument();

            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");

            pPtOpts.Message = "\nEnter the start point: ";
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p1 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            pPtOpts.Message = "\nEnter the end point: ";
            pPtOpts.UseBasePoint = true;
            pPtOpts.BasePoint = p1;
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p2 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            double sc = 0;//scale
            if (opts.scbuttcm.Checked) sc = 1;
            if (opts.scbuttm.Checked) sc = 0.01;
            if (opts.scbuttmm.Checked) sc = 10;

            PromptDoubleResult offR;
            PromptDoubleOptions offOpts = new PromptDoubleOptions("");
            offOpts.Message = "\nEnter concrete cover: ";
            offR = acDoc.Editor.GetDouble(offOpts);
            double off = offR.Value;

            double dist = p1.DistanceTo(p2);
            using (loc)
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    BlockTable acBlkTbl;
                    BlockTableRecord acBlkTblRec;

                    // Open Model space for write
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                 OpenMode.ForRead) as BlockTable;

                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                    OpenMode.ForWrite) as BlockTableRecord;

                    //matrix for converting coordinates
                    Matrix3d newMatrix = new Matrix3d();
                    newMatrix = Matrix3d.AlignCoordinateSystem(
                                                         new Point3d(0, 0, 0),
                                                         new Vector3d(1, 0, 0),
                                                         new Vector3d(0, 1, 0),
                                                         new Vector3d(0, 0, 1),
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Origin,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Xaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Yaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis
                                                         );

                    Matrix3d newMatrix2 = new Matrix3d();
                    Vector2d dirtemp = new Vector2d(p2.X - p1.X, p2.Y - p1.Y);

                    double angle = dirtemp.GetAngleTo(new Vector2d(1, 0));
                    if (p2.Y - p1.Y < 0) angle = 2 * Math.PI - angle;
                    newMatrix2 = Matrix3d.Rotation(angle,
                                                  acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis,
                                                  p1.TransformBy(newMatrix));

                    double r = bd.rcoud(ha) * sc;
                    double tan225 = Math.Tan(Math.PI / 8);
                    // Create a polyline
                    Polyline acPoly = new Polyline();
                    acPoly.SetDatabaseDefaults();

                    acPoly.AddVertexAt(0, new Point2d(p1.X + off, p1.Y + off + r + 1.5 * sc * ha), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(p1.X + off, p1.Y + off + r), tan225, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(p1.X + off + r, p1.Y + off), 0, 0, 0);
                    acPoly.AddVertexAt(3, new Point2d(p1.X + dist - off - r, p1.Y + off), tan225, 0, 0);
                    acPoly.AddVertexAt(4, new Point2d(p1.X + dist - off, p1.Y + off + r), 0, 0, 0);
                    acPoly.AddVertexAt(5, new Point2d(p1.X + dist - off, p1.Y + off + r + 1.5 * sc * ha), 0, 0, 0);

                    acPoly.TransformBy(newMatrix);
                    acPoly.TransformBy(newMatrix2);


                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);

                    acTrans.Commit();

                } //end Transaction
            } // loc
        }

        public void Draw1cr90(Int16 ha)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            DocumentLock loc = acDoc.LockDocument();

            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");

            pPtOpts.Message = "\nEnter the start point: ";
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p1 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            pPtOpts.Message = "\nEnter the end point: ";
            pPtOpts.UseBasePoint = true;
            pPtOpts.BasePoint = p1;
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p2 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            double sc = 0;//scale
            if (opts.scbuttcm.Checked) sc = 1;
            if (opts.scbuttm.Checked) sc = 0.01;
            if (opts.scbuttmm.Checked) sc = 10;
            
            PromptDoubleResult offR;
            PromptDoubleOptions offOpts = new PromptDoubleOptions("");
            offOpts.Message = "\nEnter concrete cover: ";
            offR = acDoc.Editor.GetDouble(offOpts);
            double off = offR.Value;

            double dist = p1.DistanceTo(p2);
            using (loc)
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    BlockTable acBlkTbl;
                    BlockTableRecord acBlkTblRec;

                    // Open Model space for write
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                 OpenMode.ForRead) as BlockTable;

                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                    OpenMode.ForWrite) as BlockTableRecord;

                    //matrix for converting coordinates
                    Matrix3d newMatrix = new Matrix3d();
                    newMatrix = Matrix3d.AlignCoordinateSystem(
                                                         new Point3d(0, 0, 0),
                                                         new Vector3d(1, 0, 0),
                                                         new Vector3d(0, 1, 0),
                                                         new Vector3d(0, 0, 1),
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Origin,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Xaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Yaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis
                                                         );

                    Matrix3d newMatrix2 = new Matrix3d();
                    Vector2d dirtemp = new Vector2d(p2.X - p1.X, p2.Y - p1.Y);

                    double angle = dirtemp.GetAngleTo(new Vector2d(1, 0));
                    if (p2.Y - p1.Y < 0) angle = 2 * Math.PI - angle;
                    newMatrix2 = Matrix3d.Rotation(angle,
                                                  acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis,
                                                  p1.TransformBy(newMatrix));

                    double r = bd.rcoud(ha) * sc;
                    double tan225 = Math.Tan(Math.PI / 8);
                    // Create a polyline
                    Polyline acPoly = new Polyline();
                    acPoly.SetDatabaseDefaults();

                    acPoly.AddVertexAt(0, new Point2d(p1.X + off, p1.Y + off + r + 1.5 * sc * ha), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(p1.X + off, p1.Y + off + r), tan225, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(p1.X + off + r, p1.Y + off), 0, 0, 0);
                    acPoly.AddVertexAt(3, new Point2d(p1.X + dist, p1.Y + off), 0, 0, 0);

                    acPoly.TransformBy(newMatrix);
                    acPoly.TransformBy(newMatrix2);


                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);

                    acTrans.Commit();

                } //end Transaction
            }//loc
        }

        public void DrawUbar(Int16 ha)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            DocumentLock loc = acDoc.LockDocument();

            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");

            pPtOpts.Message = "\nEnter the start point: ";
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d pt1 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            pPtOpts.Message = "\nEnter the end point: ";
            pPtOpts.UseBasePoint = true;
            pPtOpts.BasePoint = pt1;
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d pt2 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            double sc = 0;//scale
            if (opts.scbuttcm.Checked) sc = 1;
            if (opts.scbuttm.Checked) sc = 0.01;
            if (opts.scbuttmm.Checked) sc = 10;

            Point3d p1 = new Point3d(Math.Min(pt1.X, pt2.X), Math.Min(pt1.Y, pt2.Y), 0);
            Point3d p2 = new Point3d(Math.Max(pt1.X, pt2.X), Math.Max(pt1.Y, pt2.Y), 0);

            PromptDoubleResult offR;
            PromptDoubleOptions offOpts = new PromptDoubleOptions("");
            offOpts.Message = "\nEnter concrete cover: ";
            offR = acDoc.Editor.GetDouble(offOpts);
            double off = offR.Value;

            offOpts.Message = "\nEnter Ubar distance: ";
            offR = acDoc.Editor.GetDouble(offOpts);
            double height = offR.Value;

            double dist = p1.DistanceTo(p2);
            using (loc)
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    BlockTable acBlkTbl;
                    BlockTableRecord acBlkTblRec;

                    // Open Model space for write
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                 OpenMode.ForRead) as BlockTable;

                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                    OpenMode.ForWrite) as BlockTableRecord;

                    //matrix for converting coordinates
                    Matrix3d newMatrix = new Matrix3d();
                    newMatrix = Matrix3d.AlignCoordinateSystem(
                                                         new Point3d(0, 0, 0),
                                                         new Vector3d(1, 0, 0),
                                                         new Vector3d(0, 1, 0),
                                                         new Vector3d(0, 0, 1),
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Origin,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Xaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Yaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis
                                                         );

                    Matrix3d newMatrix2 = new Matrix3d();
                    Vector2d dirtemp = new Vector2d(p2.X - p1.X, p2.Y - p1.Y);

                    double angle = dirtemp.GetAngleTo(new Vector2d(1, 0));
                    if (p2.Y - p1.Y < 0) angle = 2 * Math.PI - angle;
                    newMatrix2 = Matrix3d.Rotation(angle,
                                                  acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis,
                                                  p1.TransformBy(newMatrix));

                    double r = bd.rcadr(ha) * sc;
                    double tan225 = Math.Tan(Math.PI / 8);
                    // Create a polyline
                    Polyline acPoly = new Polyline();
                    acPoly.SetDatabaseDefaults();

                    acPoly.AddVertexAt(0, new Point2d(p1.X + off, p1.Y + off + height), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(p1.X + off, p1.Y + off + r), tan225, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(p1.X + off + r, p1.Y + off), 0, 0, 0);
                    acPoly.AddVertexAt(3, new Point2d(p1.X + dist - off - r, p1.Y + off), tan225, 0, 0);
                    acPoly.AddVertexAt(4, new Point2d(p1.X + dist - off, p1.Y + off + r), 0, 0, 0);
                    acPoly.AddVertexAt(5, new Point2d(p1.X + dist - off, p1.Y + off + height), 0, 0, 0);

                    acPoly.TransformBy(newMatrix);
                    acPoly.TransformBy(newMatrix2);

                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);

                    acTrans.Commit();
                }
            }//loc
        }

        public void DrawCadre(Int16 ha)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            DocumentLock loc = acDoc.LockDocument();

            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");

            pPtOpts.Message = "\nEnter the start point: ";
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d pt1 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            pPtOpts.Message = "\nEnter the end point: ";
            pPtOpts.UseBasePoint = true;
            pPtOpts.BasePoint = pt1;
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d pt2 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            double sc = 0;//scale
            if (opts.scbuttcm.Checked) sc = 1;
            if (opts.scbuttm.Checked) sc = 0.01;
            if (opts.scbuttmm.Checked) sc = 10;

            //Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("\npt1: " + pt1);
            //point are in local coordinate system

            Point3d p1 = new Point3d(Math.Min(pt1.X, pt2.X), Math.Min(pt1.Y, pt2.Y), 0);
            Point3d p2 = new Point3d(Math.Max(pt1.X, pt2.X), Math.Max(pt1.Y, pt2.Y), 0);



            PromptDoubleResult offR;
            PromptDoubleOptions offOpts = new PromptDoubleOptions("");
            offOpts.Message = "\nEnter concrete cover: ";
            offR = acDoc.Editor.GetDouble(offOpts);
            double off = offR.Value;

            using (loc)
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    BlockTable acBlkTbl;
                    BlockTableRecord acBlkTblRec;

                    // Open Model space for write
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                 OpenMode.ForRead) as BlockTable;

                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                    OpenMode.ForWrite) as BlockTableRecord;

                    //matrix for converting coordinates
                    Matrix3d newMatrix = new Matrix3d();
                    newMatrix = Matrix3d.AlignCoordinateSystem(
                                                         new Point3d(0, 0, 0),
                                                         new Vector3d(1, 0, 0),
                                                         new Vector3d(0, 1, 0),
                                                         new Vector3d(0, 0, 1),
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Origin,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Xaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Yaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis
                                                         );



                    //Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("\nOrigin: " + acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Origin);
                    //Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("\nBefore p1: " + p1);
                    //p1 = p1.TransformBy(newMatrix);
                    //Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("\nAfter p1: " + p1.TransformBy(newMatrix));
                    //p2 = p2.TransformBy(newMatrix);

                    double r = bd.rcadr(ha) * sc;
                    double tan225 = 0 - Math.Tan(Math.PI / 8);
                    // Create a polyline
                    Polyline acPoly = new Polyline();
                    acPoly.SetDatabaseDefaults();

                    acPoly.AddVertexAt(0, new Point2d(p1.X + off, p1.Y + off + r), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(p1.X + off, p2.Y - r - off), tan225, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(p1.X + r + off, p2.Y - off), 0, 0, 0);
                    acPoly.AddVertexAt(3, new Point2d(p2.X - r - off, p2.Y - off), tan225, 0, 0);
                    acPoly.AddVertexAt(4, new Point2d(p2.X - off, p2.Y - r - off), 0, 0, 0);
                    acPoly.AddVertexAt(5, new Point2d(p2.X - off, p1.Y + r + off), tan225, 0, 0);
                    acPoly.AddVertexAt(6, new Point2d(p2.X - r - off, p1.Y + off), 0, 0, 0);
                    acPoly.AddVertexAt(7, new Point2d(p1.X + r + off, p1.Y + off), tan225, 0, 0);

                    acPoly.Closed = true;
                    acPoly.TransformBy(newMatrix);


                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);

                    //hooks
                    double sin45 = Math.Sin(Math.PI / 4);
                    Polyline acPolyhooks = new Polyline();
                    acPolyhooks.SetDatabaseDefaults();

                    acPolyhooks.AddVertexAt(0, new Point2d(p1.X + off + r * (1 - sin45) + 0.5 * sc * ha * sin45,
                                                           p2.Y - off - r - r * sin45 - 0.5 * sc * ha * sin45),
                                                           0, 0, 0);
                    acPolyhooks.AddVertexAt(1, new Point2d(p1.X + off + r * (1 - sin45),
                                                           p2.Y - off - r - r * sin45),
                                                           0 - Math.Tan(Math.PI / 4), 0, 0);
                    acPolyhooks.AddVertexAt(2, new Point2d(p1.X + off + r * (1 + sin45),
                                                           p2.Y - off - r * (1 - sin45)),
                                                           0, 0, 0);
                    acPolyhooks.AddVertexAt(3, new Point2d(p1.X + off + r * (1 + sin45) + 0.5 * sc * ha * sin45,
                                                           p2.Y - off - r * (1 - sin45) - 0.5 * sc * ha * sin45),
                                                           0, 0, 0);
                    acPolyhooks.TransformBy(newMatrix);


                    acBlkTblRec.AppendEntity(acPolyhooks);
                    acTrans.AddNewlyCreatedDBObject(acPolyhooks, true);

                    acTrans.Commit();
                }
            }//loc
        }

        public void DrawEpingle(Int16 ha)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            DocumentLock loc = acDoc.LockDocument();

            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");

            pPtOpts.Message = "\nEnter the start point: ";
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p1 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            pPtOpts.Message = "\nEnter the end point: ";
            pPtOpts.UseBasePoint = true;
            pPtOpts.BasePoint = p1;
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d p2 = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.Cancel) return;

            double sc = 0;//scale
            if (opts.scbuttcm.Checked) sc = 1;
            if (opts.scbuttm.Checked) sc = 0.01;
            if (opts.scbuttmm.Checked) sc = 10;

            double dist = p1.DistanceTo(p2);

            using (loc)
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    BlockTable acBlkTbl;
                    BlockTableRecord acBlkTblRec;

                    // Open Model space for write
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                 OpenMode.ForRead) as BlockTable;

                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                    OpenMode.ForWrite) as BlockTableRecord;

                    //matrix for converting coordinates
                    Matrix3d newMatrix = new Matrix3d();
                    newMatrix = Matrix3d.AlignCoordinateSystem(
                                                         new Point3d(0, 0, 0),
                                                         new Vector3d(1, 0, 0),
                                                         new Vector3d(0, 1, 0),
                                                         new Vector3d(0, 0, 1),
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Origin,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Xaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Yaxis,
                                                         acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis
                                                         );

                    Matrix3d newMatrix2 = new Matrix3d();
                    Vector2d dirtemp = new Vector2d(p2.X - p1.X, p2.Y - p1.Y);

                    double angle = dirtemp.GetAngleTo(new Vector2d(1, 0));
                    if (p2.Y - p1.Y < 0) angle = 2 * Math.PI - angle;
                    newMatrix2 = Matrix3d.Rotation(angle,
                                                  acDoc.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis,
                                                  p1.TransformBy(newMatrix));

                    double r = bd.rcadr(ha)*sc;
                    double tan45 = 0 - Math.Tan(Math.PI / 4);
                    // Create a polyline
                    Polyline acPoly = new Polyline();
                    acPoly.SetDatabaseDefaults();

                    acPoly.AddVertexAt(0, new Point2d(p1.X + r + 0.5 * sc * ha, p1.Y - r), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(p1.X + r, p1.Y - r), tan45, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(p1.X + r, p1.Y + r), 0, 0, 0);
                    acPoly.AddVertexAt(3, new Point2d(p1.X + dist - r, p1.Y + r), tan45, 0, 0);
                    acPoly.AddVertexAt(4, new Point2d(p1.X + dist - r, p1.Y - r), 0, 0, 0);
                    acPoly.AddVertexAt(5, new Point2d(p1.X + dist - r - 0.5 * sc * ha, p1.Y - r), 0, 0, 0);

                    acPoly.TransformBy(newMatrix);
                    acPoly.TransformBy(newMatrix2);


                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);

                    acTrans.Commit();

                } //end Transaction
            }//loc

        } 
       
    }
}

