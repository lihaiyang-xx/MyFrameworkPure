﻿#if (UNITY_EDITOR ||  UNITY_STANDALONE) && EXCEL
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using MyFrameworkPure;
using UnityEngine;

namespace MyFrameworkPure
{
    public class ExcelTool
    {
        static ExcelTool()
        {
            ICSharpCode.SharpZipLib.Zip.ZipConstants.DefaultCodePage = 65001;
        }
        public static string[,] GetExcelData(string path, string sheetName = "sheet1")
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = false
                        },

                        UseColumnDataType = false
                    });
                    DataTable table = result.Tables[sheetName];
                    int row = table.Rows.Count;
                    int colmon = table.Columns.Count;
                    string[,] data = new string[row, colmon];
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
}
#endif


