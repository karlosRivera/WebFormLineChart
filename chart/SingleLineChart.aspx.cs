using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

namespace chart
{
    public partial class ChartTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String connString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ToString();
            String sSQL = "select c.CategoryName, sum(p.UnitPrice) UnitPrice ";
            sSQL = sSQL + "from  dbo.Categories c inner join dbo.Products p ";
            sSQL = sSQL + "on c.CategoryID = p.CategoryID ";
            sSQL = sSQL + "group by c.CategoryName";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    conn.Open();
                    cmd.CommandText = sSQL;
                    cmd.Connection = conn;

                    using (SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        using (DataTable result = new DataTable())
                        {
                            result.Load(dr);

                            if (result.Rows.Count > 0)
                            {
                                Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
                                //Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
                                Chart1.BorderlineColor = System.Drawing.Color.FromArgb(26, 59, 105);
                                Chart1.BorderlineWidth = 3;
                                Chart1.BackColor = Color.NavajoWhite;

                                Chart1.ChartAreas.Add("chtArea");
                                Chart1.ChartAreas[0].AxisX.Title = "Category Name";
                                Chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);
                                Chart1.ChartAreas[0].AxisY.Title = "UnitPrice";
                                Chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);
                                Chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                                Chart1.ChartAreas[0].BorderWidth = 2;
                                //Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                                //Chart1.ChartAreas[0].Area3DStyle.Inclination = 45;
                                //Chart1.ChartAreas[0].Area3DStyle.Rotation = 45;
                                //Chart1.ChartAreas[0].Area3DStyle.PointDepth = 100;
                                //Chart1.ChartAreas[0].Area3DStyle.PointGapDepth = 1;

                                Chart1.Legends.Add("UnitPrice");
                                Chart1.Series.Add("UnitPrice");
                                //Chart1.Series[0].Palette = ChartColorPalette.Bright;
                                Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
                                Chart1.Series[0].Points.DataBindXY(result.DefaultView, "CategoryName", result.DefaultView, "UnitPrice");

                                //Chart1.Series[0].IsVisibleInLegend = true;
                                Chart1.Series[0].IsValueShownAsLabel = true;
                                Chart1.Series[0].ToolTip = "Data Point Y Value: #VALY{G}";

                                // Setting Line Width
                                Chart1.Series[0].BorderWidth = 3;
                                Chart1.Series[0].Color = Color.Red;

                                // Setting Line Shadow
                                //Chart1.Series[0].ShadowOffset = 5;

                                //Legend Properties
                                Chart1.Legends[0].LegendStyle = LegendStyle.Table;
                                Chart1.Legends[0].TableStyle = LegendTableStyle.Wide;
                                Chart1.Legends[0].Docking = Docking.Bottom;
                            }
                        }
                    }
                }
            }
        }
    }
}