using SQL;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;

namespace WindowsFormsApp0703 {
    public partial class frmRanking : Form {
        private SQLHelper sqlHelper = new SQLHelper();
        private DataSet dataSet;

        public frmRanking() {
            InitializeComponent();
            dataSet = new DataSet();
            frmRanking_Load();
        }

        // 窗体加载时获取互动榜数据并显示
        private void frmRanking_Load() {
            // 获取互动榜前三名数据
            string query = @"
                SELECT TOP 3 ts.studentName AS user_name,
       SUM(CASE WHEN is_sender = 1 THEN interactions ELSE 0 END) AS send_count,
       SUM(CASE WHEN is_sender = 0 THEN interactions ELSE 0 END) AS receive_count,
       SUM(interactions) AS total_interactions
FROM (
    SELECT from_user, 1 AS is_sender, COUNT(*) AS interactions
    FROM tblMsgs WHERE status != 4
    GROUP BY from_user
    UNION ALL
    SELECT to_user, 0 AS is_sender, COUNT(*) AS interactions
    FROM tblMsgs WHERE status != 4
    GROUP BY to_user
) AS subquery
JOIN tblTopStudents ts ON subquery.from_user = ts.studentNo
GROUP BY ts.studentName
ORDER BY total_interactions DESC;
            ";

            sqlHelper.RunSQL(query, ref dataSet);

            // 显示互动榜数据
            DisplayRankingData();
        }

        // 显示互动榜数据
        private void DisplayRankingData() {
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++) {
                DataRow row = dataSet.Tables[0].Rows[i];
                string userName = row["user_name"].ToString();
                int sendCount = Convert.ToInt32(row["send_count"]);
                int receiveCount = Convert.ToInt32(row["receive_count"]);
                int totalInteractions = Convert.ToInt32(row["total_interactions"]);

                // 根据索引 i 获取对应的 Label
                Label lblName = (Label)this.Controls.Find($"lblName{i + 1}", true).FirstOrDefault();
                Label lblFrom = (Label)this.Controls.Find($"lblFrom{i + 1}", true).FirstOrDefault();
                Label lblTo = (Label)this.Controls.Find($"lblTo{i + 1}", true).FirstOrDefault();

                // 在对应的 Label 上显示数据
                lblName.Text = userName;
                lblFrom.Text = $"Sent:{sendCount}";
                lblFrom.Size = new System.Drawing.Size(sendCount/2, 40);
                lblTo.Text = $"Received:{receiveCount}";
                lblTo.Location = new System.Drawing.Point(lblFrom.Right, 124 + 100 * i);
                lblTo.Size = new System.Drawing.Size(receiveCount / 2, 40);
            }
        }

    }
}
