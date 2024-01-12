using UnityEngine;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;

public class ExcelToXmlConverter : MonoBehaviour
{
    void Start()
    {
        // Excel ���� ���
        string excelFilePath = "Assets/Resources/PopUpdata/PopUpData.xlsx";

        // XML ���� ���
        string xmlFilePath = "Assets/Resources/PopUpdata/PopUpData.xml";

        // Excel ������ �о� DataSet���� ��ȯ
        DataSet dataSet = ReadExcelFile(excelFilePath);

        if (dataSet != null)
        {
            // DataSet�� XML�� ��ȯ�Ͽ� ����
            ConvertDataSetToXML(dataSet, xmlFilePath);

            // XML ������ �о�ͼ� ������ ����
            Dictionary<string, Dictionary<string, string>> data = ReadXmlFile(xmlFilePath);

            // ������ ����� ������ ��� (����)
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
            // DataSet�� XML�� ��ȯ�Ͽ� ����
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
            // XML ������ �о� DataSet���� ��ȯ
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlFilePath);

            // �� ��Ʈ�� �����͸� Dictionary�� ����
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
