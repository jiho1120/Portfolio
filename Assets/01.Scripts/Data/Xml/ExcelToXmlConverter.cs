using ExcelDataReader;
using System.Data;
using System.IO;
using System.Xml;
using UnityEngine;

public class ExcelToXmlConverter : MonoBehaviour
{
    private void Start()
    {
        string excelFilePath = Path.Combine(Application.dataPath, "Resources/PopUpData/PopUpData.xlsx");
        CreateXmlFromExcel(excelFilePath);
    }

    public void CreateXmlFromExcel(string excelFilePath)
    {
        FileStream stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();

        for (int sheetIndex = 0; sheetIndex < result.Tables.Count; sheetIndex++)
        {
            DataTable table = result.Tables[sheetIndex];
            var xmlDocument = new XmlDocument();
            var xmlRoot = xmlDocument.CreateElement($"{table.Rows[0][0]}");
            xmlDocument.AppendChild(xmlRoot);

            for (int i = 1; i < table.Columns.Count; i++)
            {
                for (int j = 1; j < table.Rows.Count; j++)
                {
                    string statName = table.Rows[0][i].ToString();
                    string ratingName = table.Rows[j][0].ToString();
                    string value = table.Rows[j][i].ToString();

                    var xmlElement = xmlDocument.CreateElement("Stat");
                    xmlRoot.AppendChild(xmlElement);

                    var xmlAttributeStat = xmlDocument.CreateAttribute("Name");
                    xmlAttributeStat.Value = statName;
                    xmlElement.Attributes.Append(xmlAttributeStat);

                    var xmlAttributeRating = xmlDocument.CreateAttribute("Rating");
                    xmlAttributeRating.Value = ratingName;
                    xmlElement.Attributes.Append(xmlAttributeRating);

                    var xmlAttributeValue = xmlDocument.CreateAttribute("Value");
                    xmlAttributeValue.Value = value;
                    xmlElement.Attributes.Append(xmlAttributeValue);
                }
            }

            string xmlFileName = table.Rows[0][0].ToString() + ".xml";
            string xmlFilePath = Path.Combine(Application.dataPath, "Resources/PopUpdata", xmlFileName);
            xmlDocument.Save(xmlFilePath);
            Debug.Log("XML data created for sheet: " + xmlFileName);
        }

        Debug.Log("All XML files created successfully.");
    }
}