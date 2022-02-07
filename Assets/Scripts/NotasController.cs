using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
using UnityEngine.SceneManagement;

public class NotasController : MonoBehaviour
{
    public static NotasController ctrl;
    public GameObject gameObjectInstance, parentInstance, qualificationPanel;
    public Color correctCol, wrongCol, letterCol;
    public Button verifyBtn;
    private string jsonString;
    private JsonData student;

    public static int contador;

    void Awake()
    {
        if (ctrl == null)
        {
            ctrl = this;
        }
        else if (ctrl != null)
            Destroy(gameObject);

        jsonString = File.ReadAllText(Application.dataPath + "/StreamingAssets/Estudiantes.json");
        student = JsonMapper.ToObject(jsonString);
    }

    void Start()
    {
        LoadRows();
        verifyBtn.onClick.AddListener(() =>
        {
            if (parentInstance.transform.childCount == contador)
            {
                SceneManager.LoadScene("DragAndDropScene");
            }
            else
            {
                Debug.Log("Debes calificar a todos los estudiantes");
            }
        });
    }

    void Update()
    {
        Debug.Log(contador);
    }

    public void LoadRows()
    {
        for (int i = 0; i < student["datos"].Count; i++)
        {
            InstanceRow();
            for (int j = 0; j < parentInstance.transform.childCount; j++)
            {
                gameObjectInstance.GetComponent<FilasController>().filaElementos.textoNombre.text = student["datos"][j]["Nombre"].ToString();
                gameObjectInstance.GetComponent<FilasController>().filaElementos.textoApellido.text = student["datos"][j]["Apellido"].ToString();
                gameObjectInstance.GetComponent<FilasController>().filaElementos.textoCodigo.text = student["datos"][j]["Codigo"].ToString();
                gameObjectInstance.GetComponent<FilasController>().filaElementos.textoCorreo.text = student["datos"][j]["Correo"].ToString();
                gameObjectInstance.GetComponent<FilasController>().filaElementos.textoNota.text = student["datos"][j]["Nota"].ToString();
                gameObjectInstance.GetComponent<FilasController>().filaElementos.notaEstudiante = float.Parse(student["datos"][j]["Nota"].ToString());
            }
        }
    }

    public void InstanceRow()
    {
        GameObject rowItem = gameObjectInstance;
        rowItem = Instantiate(rowItem, new Vector2(-3000, 0), Quaternion.identity);
        rowItem.transform.SetParent(parentInstance.transform);
        rowItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
    }
}
