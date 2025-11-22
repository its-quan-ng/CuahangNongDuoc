using System;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc
{
	
	public class DataService : DataTable
	{
        
		// The connection to a database of this data service.
		private static SqlConnection	m_Connection;

        //
        public static String m_ConnectString = "Server=.\\SQLEXPRESS;Initial Catalog=QLCHNongDuoc;Integrated Security=SSPI;TrustServerCertificate=True;";
		// The command to execute query or non-query command on a database of this data service.
		private SqlCommand		m_Command;
      
		// The data adapter to execute query on a database of this data service.
		private SqlDataAdapter	m_DataAdapter;

        public DataService(){}


        public SqlCommand Command
        {
            get { return m_Command; }
            set { m_Command = value; }
        }

		public void Load(SqlCommand command)
		{
            OpenConnection();
            m_Command = command;
            try
            {
                
                m_Command.Connection = m_Connection;

                m_DataAdapter = new SqlDataAdapter();
                m_DataAdapter.SelectCommand = m_Command;

                this.Clear();
                m_DataAdapter.Fill(this);

            }
            catch (Exception e) 
            {
                System.Diagnostics.Debug.WriteLine("DataService.Load Error: " + e.Message);
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối database:\n" + e.Message + "\n\nConnection: " + m_ConnectString, "Lỗi", 
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
		}


        public static bool OpenConnection()
        {
            try
            {
                if (m_Connection == null)
                    //m_Connection = new SqlConnection("Data Source=LAPTOP07\\SQLEXPRESS;Initial Catalog=VCB;Integrated Security=True;");
                    //m_Connection = new SqlConnection("Data Source=localhost;Initial Catalog=phongkham;User ID=sa; Password=tvteo;");
                    m_Connection = new SqlConnection(m_ConnectString);
                    
                    
                if (m_Connection.State == ConnectionState.Closed)
                    m_Connection.Open();
                return true;
            }
            catch (Exception e)
            {
                m_Connection.Close();
                return false;
            }
            
        }
		/// <summary>
		/// Closes the connection of this data service.
		/// </summary>
		public void CloseConnection()
		{
			m_Connection.Close();
		}

        /// <summary>
        /// Update DataTable
        /// </summary>
        /// <returns></returns>
        public int ExecuteNoneQuery()
		{
            int result = 0;
            SqlTransaction tr = null;
			try
			{
                OpenConnection();
                
                if (m_Command == null)
                {
                    if (string.IsNullOrEmpty(this.TableName))
                    {
                        throw new InvalidOperationException("TableName chưa được set. Không thể tạo command để update.");
                    }
                    m_Command = new SqlCommand("SELECT * FROM " + this.TableName);
                }
                
                tr =  m_Connection.BeginTransaction();

                m_Command.Connection = m_Connection;
                m_Command.Transaction = tr;

                m_DataAdapter = new SqlDataAdapter();
                m_DataAdapter.SelectCommand = m_Command;

                SqlCommandBuilder builder = new SqlCommandBuilder(m_DataAdapter);                

                result = m_DataAdapter.Update(this);
               

                tr.Commit();
                
			}
			catch ( Exception e)
            {
                if (tr != null) tr.Rollback();
                System.Windows.Forms.MessageBox.Show("Lỗi khi lưu dữ liệu:\n" + e.Message, "Lỗi", 
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                throw; 
            }
            return result;
		}
        /// <summary>
        /// Thuc thi mot command
        /// </summary>
        /// <param name="command">SqlCommand hay Store Procedure</param>
        /// <returns></returns>
        public int ExecuteNoneQuery(SqlCommand cmd)
        {

            int result = 0;
            SqlTransaction tr = null;

            try
            {
                tr = m_Connection.BeginTransaction();

                cmd.Connection = m_Connection;

                cmd.Transaction = tr;

                result = cmd.ExecuteNonQuery();

                this.AcceptChanges();

                tr.Commit();

            }
            catch(Exception e)
            {
                if (tr != null) tr.Rollback();
                throw;
            }
            return result;
            
        }

        public object ExecuteScalar(SqlCommand cmd)
        {
            object result = null;
            SqlTransaction tr = null;
            
            try
            {
                tr = m_Connection.BeginTransaction();

                cmd.Connection = m_Connection;

                cmd.Transaction = tr;

                result = cmd.ExecuteScalar();

                this.AcceptChanges();

                tr.Commit();

                if (result == DBNull.Value)
                {
                    result = null;
                }
            }
            catch
            {
                if (tr != null) tr.Rollback();
                throw;
            }
            return result;
        }
	}
}