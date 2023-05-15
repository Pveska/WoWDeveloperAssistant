using DB2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WoWDeveloperAssistant.Properties;

namespace DB2Storage
{
    public class MySqlStorageBase<T> : Dictionary<int, T>
    {
        public bool WorldStorage = false;

        public MySqlStorageBase() {  }

        public void Init(string DatabaseConnectionName)
        {
            MySql.Data.MySqlClient.MySqlConnection lConnection;
            try
            {
                lConnection = new MySql.Data.MySqlClient.MySqlConnection($"Server={Settings.Default.Host};Port={Settings.Default.Port};UserID={Settings.Default.Username};Password={Settings.Default.Password};Database={Settings.Default.DB2Database};CharacterSet=utf8;ConnectionTimeout=5;");
                lConnection.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show($"Errno {ex.Number}{Environment.NewLine}{ex.Message}");
                return;
            }

            using (lConnection)
            {
                string lTableName = string.Empty;
                var lAttributes = typeof(T).GetCustomAttributes(true);
                foreach (var attr in lAttributes)
                {
                    if (attr is HotfixAttribute lHotfixInfo)
                    {
                        lTableName = lHotfixInfo.TableName;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(lTableName))
                    return;

                MySql.Data.MySqlClient.MySqlCommand lCommand = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM " + lTableName, lConnection);

                try
                {
                    using (var lReader = lCommand.ExecuteReader())
                    {
                        int lPlaceHolderKey = 0;
                        while (lReader.Read())
                        {
                            TypeInfo lTypeInfo = typeof(T).GetTypeInfo();
                            object lRow = (T)Activator.CreateInstance(typeof(T));
                            var lFieldsInfo = lTypeInfo.DeclaredFields.ToArray();
                            int lKey = 0;

                            bool lKeyFound = false;
                            int lFieldIndex = 0;

                            int FieldCount = lReader.FieldCount;
                            if (!WorldStorage)
                                FieldCount--;

                            for (var lI = 0; lI < FieldCount; lI++)
                            {
                                if (lReader.IsDBNull(lI))
                                    continue;

                                if (lReader.GetName(lI) == "ROW_ID")
                                {
                                    lKey = lReader.GetInt32(lI);
                                    lKeyFound = true;
                                    continue;
                                }

                                var lFieldType = lFieldsInfo[lFieldIndex].FieldType;

                                if (lFieldType == typeof(UInt32))
                                    lFieldsInfo[lFieldIndex].SetValue(lRow, lReader.GetUInt32(lI));

                                else if (lFieldType == typeof(Int32) || lFieldType == typeof(int?))
                                {
                                    if (!WorldStorage)
                                        lFieldsInfo[lFieldIndex].SetValue(lRow, BitConverter.ToInt32(BitConverter.GetBytes(lReader.GetInt32(lI)), 0));
                                    else
                                        lFieldsInfo[lFieldIndex].SetValue(lRow, lReader.GetInt32(lI));
                                }

                                else if (lFieldType == typeof(UInt16))
                                    lFieldsInfo[lFieldIndex].SetValue(lRow, lReader.GetUInt16(lI));

                                else if (lFieldType == typeof(Int16))
                                {
                                    if (!WorldStorage)
                                        lFieldsInfo[lFieldIndex].SetValue(lRow, BitConverter.ToInt16(BitConverter.GetBytes(lReader.GetInt16(lI)), 0));
                                    else
                                        lFieldsInfo[lFieldIndex].SetValue(lRow, lReader.GetInt16(lI));
                                }

                                else if (lFieldType == typeof(float))
                                    lFieldsInfo[lFieldIndex].SetValue(lRow, lReader.GetFloat(lI));

                                else if (lFieldType == typeof(UInt64))
                                    lFieldsInfo[lFieldIndex].SetValue(lRow, lReader.GetUInt64(lI));

                                else if (lFieldType == typeof(Int64))
                                {
                                    if (!WorldStorage)
                                        lFieldsInfo[lFieldIndex].SetValue(lRow, BitConverter.ToInt64(BitConverter.GetBytes(lReader.GetInt64(lI)), 0));
                                    else
                                        lFieldsInfo[lFieldIndex].SetValue(lRow, lReader.GetInt64(lI));
                                }

                                else if (lFieldType == typeof(bool))
                                    lFieldsInfo[lFieldIndex].SetValue(lRow, lReader.GetBoolean(lI));

                                else if (lFieldType == typeof(byte))
                                    lFieldsInfo[lFieldIndex].SetValue(lRow, lReader.GetByte(lI));

                                else if (lFieldType == typeof(sbyte))
                                {
                                    if (!WorldStorage)
                                        lFieldsInfo[lFieldIndex].SetValue(lRow, (sbyte)BitConverter.ToInt16(BitConverter.GetBytes(lReader.GetSByte(lI)), 0));
                                    else
                                        lFieldsInfo[lFieldIndex].SetValue(lRow, lReader.GetSByte(lI));
                                }

                                else if (lFieldType == typeof(string))
                                    lFieldsInfo[lFieldIndex].SetValue(lRow, lReader.GetString(lI));

                                else if (lFieldType.BaseType == typeof(Array))
                                {
                                    Array lArray = (Array)lFieldsInfo[lFieldIndex].GetValue(lRow);

                                    for (int lArrayIndex = 0; lArrayIndex < lArray.Length; lArrayIndex++)
                                    {
                                        if (lArray.GetType().GetElementType() == typeof(UInt32))
                                            lArray.SetValue(lReader.GetUInt32(lI), lArrayIndex);

                                        else if (lArray.GetType().GetElementType() == typeof(Int32))
                                            lArray.SetValue(BitConverter.ToInt32(BitConverter.GetBytes(lReader.GetInt32(lI)), 0), lArrayIndex);

                                        else if (lArray.GetType().GetElementType() == typeof(UInt16))
                                            lArray.SetValue(lReader.GetUInt16(lI), lArrayIndex);

                                        else if (lArray.GetType().GetElementType() == typeof(Int16))
                                            lArray.SetValue(BitConverter.ToInt16(BitConverter.GetBytes(lReader.GetInt16(lI)), 0), lArrayIndex);

                                        else if (lArray.GetType().GetElementType() == typeof(float))
                                            lArray.SetValue(lReader.GetFloat(lI), lArrayIndex);

                                        else if (lArray.GetType().GetElementType() == typeof(UInt64))
                                            lArray.SetValue(lReader.GetUInt64(lI), lArrayIndex);

                                        else if (lArray.GetType().GetElementType() == typeof(Int64))
                                            lArray.SetValue(BitConverter.ToInt64(BitConverter.GetBytes(lReader.GetInt64(lI)), 0), lArrayIndex);

                                        else if (lArray.GetType().GetElementType() == typeof(byte))
                                            lArray.SetValue(lReader.GetByte(lI), lArrayIndex);

                                        else if (lArray.GetType().GetElementType() == typeof(sbyte))
                                            lArray.SetValue((sbyte)BitConverter.ToInt16(BitConverter.GetBytes(lReader.GetSByte(lI)), 0), lArrayIndex);

                                        else if (lArray.GetType().GetElementType() == typeof(string))
                                            lArray.SetValue(lReader.GetString(lI), lArrayIndex);
                                        else
                                            Console.WriteLine("Unhandle type " + lFieldType + " in array can't load " + lTableName);

                                        lI++;
                                    }
                                    lI--;
                                }
                                else
                                {
                                    Console.WriteLine("Unhandle type " + lFieldType + " can't load " + lTableName);
                                    return;
                                }

                                lFieldIndex++;
                            }

                            if (!lKeyFound)
                                lKey = ++lPlaceHolderKey;

                            Add(lKey, (T)lRow);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Table " + lTableName + " structure error! Error: " + e.Message);
                }

                if (Count == 0)
                    MessageBox.Show($"Table {lTableName} is empty!");
            }

            lConnection.Close();
            lConnection.Dispose();
        }
    }

    public class MySqlStorage<T> : MySqlStorageBase<T>
    {
        public MySqlStorage()
        {
            Init("db2");
        }
    }

    public class MySqlWorldStorage<T> : MySqlStorageBase<T>
    {
        public MySqlWorldStorage()
        {
            WorldStorage = true;
            Init("world_dragonflight");
        }
    }
}
