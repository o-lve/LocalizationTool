using System.Collections;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class LocalizationTool
{
    static string  createPath = @"D:\GitHub\LocalizationTool\Assets\Excel\data.xlsx";
    private static Dictionary<int, string> _excelData = new Dictionary<int, string>();

    [MenuItem("Tools/检测所有Text添加本地化脚本")]
    public static void CheckTextFont()
    {
        string[] allPath = AssetDatabase.FindAssets("t:Prefab", new string[] {"Assets/GameRes"});
        for (int i = 0; i < allPath.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(allPath[i]);
            var obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
            if (obj != null)
            {
                var texts = obj.GetComponentsInChildren<Text>();
                foreach (var text in texts)
                {
                    if (!text.TryGetComponent<LocalizetionText>(out var component))
                    {
                        component = text.gameObject.AddComponent<LocalizetionText>();
                    }
                    SaveData(text, component);
                }
            }
        }

        SaveExcel();
        AssetDatabase.Refresh();
    }
    
    private static int _index = 0;
    private static void SaveData(Text text,LocalizetionText locaText)
    {
        locaText.index = _index++;
        _excelData.Add(locaText.index,text.text);
    }

    private static FileInfo CreateExcel()
    {
        FileInfo newFile = new FileInfo(createPath);
        if (newFile.Exists)
        {
            newFile.Delete();
            newFile = new FileInfo(createPath);
        }

        return newFile;
    }

    private static void SaveExcel()
    {
        int verticalIndex = 1;
        var file = CreateExcel();
        ExcelPackage package = new ExcelPackage(file);
        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("table1");//创建worksheet
        foreach (var data in _excelData)
        {
            worksheet.Cells[verticalIndex, 1].Value = data.Key;
            worksheet.Cells[verticalIndex, 2].Value = data.Value;
            verticalIndex++;
        }
        package.Save();//保存excel
    }
}
