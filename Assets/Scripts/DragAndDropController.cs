using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class DragAndDropController : MonoBehaviour
{
    public GameObject gameObjectInstance, parentInstance, panelFinish, aprobados, reprobados;
    public Button FinishBtn;
    private string jsonString;
    private JsonData student;
    int parenChild;

    private void Awake()
    {
        jsonString = File.ReadAllText(Application.dataPath + "/StreamingAssets/Estudiantes.json");
        student = JsonMapper.ToObject(jsonString);
    }
    void Start()
    {
        LoadStudents();
        parenChild = parentInstance.transform.childCount;
        FinishBtn.onClick.AddListener(() =>
        {
            panelFinish.SetActive(false);
            Application.Quit();
        });
    }

    void Update()
    {
        if (aprobados.transform.childCount + reprobados.transform.childCount == parenChild)
        {
            panelFinish.SetActive(true);
        }
    }

    public void LoadStudents()
    {
        for (int i = 0; i < student["datos"].Count; i++)
        {
            InstanceStudent();
            for (int j = 0; j < parentInstance.transform.childCount; j++)
            {
                gameObjectInstance.GetComponent<EstudianteController>().nombreEstudiante.text = student["datos"][j]["Nombre"].ToString();
                gameObjectInstance.GetComponent<EstudianteController>().notaEstudiante = float.Parse(student["datos"][j]["Nota"].ToString());
            }
        }

    }

    public void InstanceStudent()
    {
        GameObject studentItem = gameObjectInstance;
        studentItem = Instantiate(studentItem, new Vector2(-3000, 0), Quaternion.identity);
        studentItem.transform.SetParent(parentInstance.transform);
        studentItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
    }
}
