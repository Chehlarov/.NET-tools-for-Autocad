using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.EditorInput;

[assembly: CommandClass(typeof(Nomproject.Commands))]
namespace Nomproject
{
    public class Commands : IExtensionApplication
    {
        static TypeViewerPalette tvp;

        public void Initialize()
        {
            tvp = new TypeViewerPalette();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Nomproject succesfully loaded!");
            DocumentCollection dm = Application.DocumentManager;
            dm.DocumentCreated += new DocumentCollectionEventHandler(OnDocumentCreated);
            foreach (Document doc in dm)
            {
               doc.ImpliedSelectionChanged += new EventHandler(doc_ImpliedSelectionChanged);               
            }
        }

        void doc_ImpliedSelectionChanged(object sender, EventArgs e)
        {
            Document doc = (Document)sender;
            Editor ed = doc.Editor;
            PromptSelectionResult sr = ed.SelectImplied();
            if (sr.Value == null)
            {
                tvp.annulateText();
            }
            else
            {
                tvp.SetSS(sr.Value); 
            }
        }

        public void Terminate()
        {
            try
            {
                DocumentCollection dm = Application.DocumentManager;
                if (dm != null)
                {
                    Editor ed = dm.MdiActiveDocument.Editor;
                    dm.MdiActiveDocument.ImpliedSelectionChanged -= new EventHandler(doc_ImpliedSelectionChanged);
                }
            }

            catch (System.Exception)
            {
                // The editor may no longer

                // be available on unload
            }
        }

        private void OnDocumentCreated(object sender, DocumentCollectionEventArgs e)
        {
            e.Document.ImpliedSelectionChanged += new EventHandler(doc_ImpliedSelectionChanged);
        }
         
        [CommandMethod("bl")]
        public void ShowPalette()
        {
            tvp.Show();
        }
    }
}