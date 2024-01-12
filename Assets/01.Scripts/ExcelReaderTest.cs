using UnityEngine;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using System.Xml;

public class ExcelReaderTest : MonoBehaviour
{
    string excelFilePath = "";
    string xmlFilePath = "";

    void Start()
    {
        excelFilePath = Path.Combine(Application.dataPath, "Resources/PopUpdata/PopUpData.xlsx");

        using (var stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet();

                // XML ���� ����
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement rootElement = xmlDoc.CreateElement("PopUpData");
                xmlDoc.AppendChild(rootElement);

                // ��Ʈ ������ŭ �ݺ�
                for (int i = 0; i < result.Tables.Count; i++)
                {
                    xmlFilePath = Path.Combine(Application.dataPath, $"Resources/PopUpdata/PopUpData.xml");

                    // �ش� ��Ʈ�� �൥����(���پ�)�� �ݺ�
                    for (int j = 0; j < result.Tables[0].Rows.Count; j++)
                    {
                        // �ش� ��Ʈ�� ��������(���پ�)�� �ݺ�
                        XmlElement rowElement = xmlDoc.CreateElement("Row");

                        for (int k = 1; k < result.Tables[0].Columns.Count; k++) // ó������ ��������
                        {
                            string columnName = result.Tables[0].Columns[k].ColumnName;
                            string data = result.Tables[0].Rows[j][k].ToString();

                            // �����͸� XML�� �߰�
                            XmlElement dataElement = xmlDoc.CreateElement(columnName);
                            dataElement.InnerText = data;
                            rowElement.AppendChild(dataElement);
                        }

                        // �� ���� ��Ʈ�� �߰�
                        rootElement.AppendChild(rowElement);
                    }
                }
                // XML ���� ����
                xmlDoc.Save(xmlFilePath);

            }
        }

        Debug.Log("���� ����");
    }
}
