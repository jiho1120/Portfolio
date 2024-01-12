using UnityEngine;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;

public class ExcelToXmlConverter : MonoBehaviour
{
    void Start()
    {
        // Excel 파일 경로
        string excelFilePath = "Assets/Resources/PopUpdata/PopUpData.xlsx";

        // XML 파일 경로
        string xmlFilePath = "Assets/Resources/PopUpdata/PopUpData.xml";

        // Excel 파일을 읽어 DataSet으로 변환
        DataSet dataSet = ReadExcelFile(excelFilePath);

        if (dataSet != null)
        {
            // DataSet을 XML로 변환하여 저장
            ConvertDataSetToXML(dataSet, xmlFilePath);

            // XML 파일을 읽어와서 변수에 저장
            Dictionary<string, Dictionary<string, string>> data = ReadXmlFile(xmlFilePath);

            // 변수에 저장된 데이터 출력 (예시)
            foreach (var sheet in data)
            {
                Debug.Log($"Sheet: {sheet.Key}");
                foreach (var row in sheet.Value)
                {
                    Debug.Log($"  {row.Key}: {row.Value}");
                }
            }
        }
        else
        {
            Debug.LogError("Failed to read Excel file.");
        }
    }

    private DataSet ReadExcelFile(string filePath)
    {
        try
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    return reader.AsDataSet();
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error reading Excel file: {e.Message}");
            return null;
        }
    }

    private void ConvertDataSetToXML(DataSet dataSet, string xmlFilePath)
    {
        try
        {
            // DataSet을 XML로 변환하여 저장
            dataSet.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);

            Debug.Log("Conversion completed successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error converting DataSet to XML: {e.Message}");
        }
    }

    private Dictionary<string, Dictionary<string, string>> ReadXmlFile(string xmlFilePath)
    {
        Dictionary<string, Dictionary<string, string>> data = new Dictionary<string, Dictionary<string, string>>();

        try
        {
            // XML 파일을 읽어 DataSet으로 변환
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlFilePath);

            // 각 시트의 데이터를 Dictionary에 저장
            foreach (DataTable table in dataSet.Tables)
            {
                Dictionary<string, string> sheetData = new Dictionary<string, string>();
                foreach (DataRow row in table.Rows)
                {
                    sheetData[row[0].ToString()] = row[1].ToString();
                }
                data[table.TableName] = sheetData;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error reading XML file: {e.Message}");
        }

        return data;
    }
}
