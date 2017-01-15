using System;
using System.Drawing;
using System.Windows.Forms;


namespace ITfamily.Utils.Helpers
{
    public class UiHelper
    {
        public static void Info(String infoText, String caption)
        {
            MessageBox.Show(infoText, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Question(String infoText, String caption)
        {
            return MessageBox.Show(infoText, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void Error(String error)
        {
            MessageBox.Show(
                   error,
                   "Ошибка",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
        }

        /// <summary>
        /// Create buttons
        /// </summary>
        /// <param name="text">Button text</param>
        /// <returns>Created buttons</returns>
        public static ToolStripButton CreateButton(String text)
        {
            return new ToolStripButton(text);
        }


        /// <summary>
        /// Create buttons
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="img">Image for button</param>
        /// <returns>Created buttons</returns>
        public static ToolStripButton CreateButton(String text, Image img)
        {
            return new ToolStripButton(text,img);
        }

        public static Int32 GridRowHeaderWidth = 15;
        public static Int32 GridNumColumnWidth = 50;
        public static Int32 GridRowWidth = 25;

        public static Font GridHeaderTextFont = new Font(FontFamily.GenericSansSerif, 9);
        public static Font CommonTextFontCommon = new Font(FontFamily.GenericSansSerif, 12);
        public static Font GridTextFontOrder = new Font(FontFamily.GenericSansSerif, 13);

        public static Color GridColumnsHeaderBackColor = Color.FromArgb(128, 0, 0);
        public static Color GridColumnsHeaderTextColor = Color.White;

        public static Color GridTextColor = Color.Black;

        /// <summary>
        /// Returns grid row template
        /// </summary>
        public static DataGridViewRow GridRowTemplate
        {
            get
            {
                var row = new DataGridViewRow
                    {
                        Height = GridRowWidth
                    };
                return row;
            }
        }

        /// <summary>
        /// Cell style
        /// </summary>
        private static DataGridViewCellStyle CellStyle
        {
            get
            {
                var cellsStyle = new DataGridViewCellStyle
                    {
                        ForeColor = GridTextColor,
                        Font = CommonTextFontCommon
                    };
                //cellsStyle.BackColor = GridBackColor;
                return cellsStyle;
            }
        }

        /// <summary>
        /// Return cell template for grids
        /// </summary>
        public static DataGridViewTextBoxCell GridCellTemplate
        {
            get
            {
                var cellTemp = new DataGridViewTextBoxCell { Style = CellStyle };
                return cellTemp;
            }
        }


        /// <summary>
        /// Return style for header
        /// </summary>
        public static DataGridViewCellStyle GridHeaderStyle
        {
            get
            {

                return new DataGridViewCellStyle
                    {
                        //BackColor = GridColumnsHeaderBackColor,
                        //ForeColor = GridColumnsHeaderTextColor,
                        Font = GridHeaderTextFont
                    };
            }
        }

        /// <summary>
        /// Set grid to common setup color,width,font e.t.c
        /// </summary>
        /// <param name="grids">GridViews to setup as params</param>
        public static void SetUpGrid(params DataGridView[] grids)
        {
            var headerStyle = GridHeaderStyle;
            foreach (var grid in grids)
            {
                grid.MultiSelect = false;
                grid.ReadOnly = true;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grid.AllowUserToAddRows = false;
                grid.AllowUserToDeleteRows = false;
                grid.AllowUserToOrderColumns = false;
//                grid.AllowUserToResizeColumns = false;
                grid.AllowUserToResizeRows = false;
                grid.AutoGenerateColumns = false;
               // grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                //grid.RowHeadersWidth = GridRowHeaderWidth;
                //grid.BackgroundColor = GridBackColor;
                grid.ColumnHeadersDefaultCellStyle = headerStyle;
                grid.RowHeadersVisible = false;
                grid.RowTemplate = GridRowTemplate;
            }
        }

        /// <summary>
        /// Returns first selected row, first cell value
        /// </summary>
        /// <param name="gridView">GridView to get value</param>
        /// <returns>Value converted to Int32</returns>
        public static Int32 GridGetIntValue(DataGridView gridView)
        {
            var rowCol = gridView.SelectedRows;
            return rowCol.Count <= 0 ? 0 : Convert.ToInt32(rowCol[0].Cells[0].Value);
        }

        /// <summary>
        /// Returns first selected row, first cell value
        /// </summary>
        /// <param name="gridView">GridView to get value</param>
        /// <returns>Value converted to Guid</returns>
        public static Guid? GridGetGuidValue(DataGridView gridView)
        {
            var rowCol = gridView.SelectedRows;
            return rowCol.Count > 0 ? (Guid?)new Guid(rowCol[0].Cells["Guid"].Value.ToString()) : null;
        }

        /// <summary>
        /// Sets common style to column
        /// </summary>
        /// <param name="font"> </param>
        /// <param name="columns">columns</param>
        public static void SetUpGridColumnsFont(Font font, params DataGridViewColumn[] columns)
        {
            var cellTemp = GridCellTemplate;
            cellTemp.Style.Font = font;
            foreach (var col in columns)
                col.CellTemplate = cellTemp;

        }

        /// <summary>
        /// Sets common style to column
        /// </summary>
        /// <param name="columns">columns</param>
        public static void SetUpGridColumns(params DataGridViewColumn[] columns)
        {
            var cellTemp = GridCellTemplate;
            foreach (var col in columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.CellTemplate = cellTemp;
            }
        }

        /// <summary>
        /// Creates column with style
        /// </summary>
        /// <param name="dataName">Column name(Table name)</param>
        /// <returns>new column</returns>
        public static DataGridViewColumn CreateIdColumn(String dataName = "Id")
        {
            return CreateColumn("Id", dataName, false);
        }

        /// <summary>
        /// Creates column with style
        /// </summary>
        /// <param name="dataName">Column name(Table name)</param>
        /// <returns>new column</returns>
        public static DataGridViewColumn CreateValueColumn(String dataName)
        {
            return CreateColumn(
                dataName
                , dataName
                , false
                );
        }


        /// <summary>
        /// Creates column with style
        /// </summary>
        /// <param name="name">Name for user</param>
        /// <param name="dataName">Column name(Table name)</param>
        /// <returns>new column</returns>
        public static DataGridViewColumn CreateColumn(String name, String dataName)
        {
            return CreateColumn(name, dataName, true);
        }
        
        /// <summary>
        /// Creates column with style
        /// </summary>
        /// <param name="name">Name for user</param>
        /// <param name="dataName">Column name(Table name)</param>
        /// <returns>new column</returns>
        public static DataGridViewColumn CreateColumn(String name, String dataName, Int32 width)
        {
            var colGroupsName = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = dataName,
                    Name = name,
                    Width = width
                };
            SetUpGridColumns(colGroupsName);
            return colGroupsName;
        }


        /// <summary>
        /// Creates column with style
        /// </summary>
        /// <param name="name">Name for user</param>
        /// <param name="dataName">Column name(Table name)</param>
        /// <param name="visible">Is visible?</param>
        /// <returns>new column</returns>
        public static DataGridViewColumn CreateColumn
            (
            String name
            , String dataName
            , Boolean visible)
        {
            DataGridViewColumn colGroupsName = new DataGridViewTextBoxColumn();
            colGroupsName.DataPropertyName = dataName;
            colGroupsName.Name = name;
            colGroupsName.Visible = visible;
            SetUpGridColumns(colGroupsName);
            return colGroupsName;
        }
    }
}