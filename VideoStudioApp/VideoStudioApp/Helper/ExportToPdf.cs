using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace VideoStudioApp.Helper
{
   public class ExportToPdf
    {
       // private void ExportToPdf(DataGrid grid)
       // { 
       // PdfPTable table = new PdfPTable(grid.Columns.Count);
       // Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35); 
       // PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream("Test.pdf", System.IO.FileMode.Create));
       // doc.Open();
       // for (int j = 0; j < grid.Columns.Count;j++)
       // {
       // table.AddCell(new Phrase(grid.Columns[j].Header.ToString()));
       // }
       // table.HeaderRows = 1;
       // IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
       // if (itemsSource != null)
       // {
       // foreach (var item in itemsSource)
       // {
       // DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
       // if (row != null)
       // {
       //// DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
       // for (int i = 0; i < grid.Columns.Count; ++i)
       // {
       // DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
       // TextBlock txt = cell.Content as TextBlock;
       // if (txt != null)
       // {
       // table.AddCell(new Phrase(txt.Text));
       // }
       // }
       // }
       // }

       // doc.Add(table); 
       // doc.Close();
       // }
       // }

    }
}
