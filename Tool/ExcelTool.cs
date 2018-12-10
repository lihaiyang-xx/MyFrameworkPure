using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEngine;

public class ExcelTool
{
    public static string[,] GetExcelData(string path,string sheetName = "sheet1")
    {
        using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                DataSet result = reader.AsDataSet();
                DataTable table = result.Tables[sheetName];
                int row = table.Rows.Count;
                int colmon = table.Columns.Count;
                string[,] data = new string[row,colmon];
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < colmon; j++)
                    {
                        data[i, j] = table.Rows[i][j].ToString();
                    }
                }

                return data;

            }
        }
    }
}

