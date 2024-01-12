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

                // XML 문서 생성
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement rootElement = xmlDoc.CreateElement("PopUpData");
                xmlDoc.AppendChild(rootElement);

                // 시트 개수만큼 반복
                for (int i = 0; i < result.Tables.Count; i++)
                {
                    xmlFilePath = Path.Combine(Application.dataPath, $"Resources/PopUpdata/PopUpData.xml");

                    // 해당 시트의 행데이터(한줄씩)로 반복
                    for (int j = 0; j < result.Tables[0].Rows.Count; j++)
                    {
                        // 해당 시트의 열데이터(한줄씩)로 반복
                        XmlElement rowElement = xmlDoc.CreateElement("Row");

                        for (int k = 1; k < result.Tables[0].Columns.Count; k++) // 처음꺼는 버려야함
                        {
                            string columnName = result.Tables[0].Columns[k].ColumnName;
                            string data = result.Tables[0].Rows[j][k].ToString();

                            // 데이터를 XML에 추가
                            XmlElement dataElement = xmlDoc.CreateElement(columnName);
                            dataElement.InnerText = data;
                            rowElement.AppendChild(dataElement);
                        }

                        // 한 행을 루트에 추가
                        rootElement.AppendChild(rowElement);
                    }
                }
                // XML 파일 저장
                xmlDoc.Save(xmlFilePath);

            }
        }

        Debug.Log("저장 성공");
    }
}
