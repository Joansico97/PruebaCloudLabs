using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilasController : MonoBehaviour
{
    public filaElementos filaElementos;

    public bool calificado = false;

    private void Start()
    {
        filaElementos.confirmacionNota.onClick.AddListener(() =>
        {
            calificado = true;
            NotasController.contador++;
            filaElementos.confirmacionNota.interactable = false;
            GameObject qualificationPanel = NotasController.ctrl.qualificationPanel;
            qualificationPanel = Instantiate(qualificationPanel, new Vector2(-3000, 0), Quaternion.identity);
            qualificationPanel.transform.SetParent(GameObject.Find("/Canvas/Spawn").transform);
            qualificationPanel.GetComponent<RectTransform>().sizeDelta = GameObject.Find("/Canvas").GetComponent<RectTransform>().sizeDelta;
            qualificationPanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
            qualificationPanel.transform.position = GameObject.Find("/Canvas/Spawn").transform.position;

            qualificationPanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
            {
                if (filaElementos.notaEstudiante >= 3)
                {
                    filaElementos.confirmacionNota.GetComponentInChildren<Text>().text = "Aprobado";
                    filaElementos.confirmacionNota.GetComponentInChildren<Text>().color = NotasController.ctrl.letterCol;
                    filaElementos.confirmacionNota.GetComponent<Image>().color = NotasController.ctrl.correctCol;
                    StartCoroutine(DestroyQualificationPanel(0.5f, qualificationPanel));
                }
                else
                {
                    Debug.Log("Califica bien al estudiante");
                }
            });
            qualificationPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
            {
                if (filaElementos.notaEstudiante < 3)
                {
                    filaElementos.confirmacionNota.GetComponentInChildren<Text>().text = "Reprobado";
                    filaElementos.confirmacionNota.GetComponentInChildren<Text>().color = NotasController.ctrl.letterCol;
                    filaElementos.confirmacionNota.GetComponent<Image>().color = NotasController.ctrl.wrongCol;
                    StartCoroutine(DestroyQualificationPanel(0.5f, qualificationPanel));
                }
                else
                {
                    Debug.Log("Califica correctamente al estudiante");
                }
            });
        });
    }

    public IEnumerator DestroyQualificationPanel(float time, GameObject qualificationPanel)
    {
        yield return new WaitForSeconds(time);
        Destroy(qualificationPanel);
    }
}

[System.Serializable]
public class filaElementos
{
    public Text textoNombre;
    public Text textoApellido;
    public Text textoCodigo;
    public Text textoCorreo;
    public Text textoNota;
    public float notaEstudiante;
    public Button confirmacionNota;
}
