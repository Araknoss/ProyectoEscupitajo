using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignSpritesToChildren : MonoBehaviour
{
    [SerializeField] private List<Trick> trickList;

    private void Start()
    {
        // 1. Ordenar por ID
        trickList.Sort((a, b) => a.id.CompareTo(b.id));

        // 2. Obtener todas las imágenes hijas
        Image[] images = GetComponentsInChildren<Image>();

        // Opcional: ignorar la imagen del propio padre
        //images = images.Skip(1).ToArray();

        // 3. Asignar sprites
        int count = Mathf.Min(images.Length, trickList.Count);

        for (int i = 1; i < count; i++)
        {
            images[i].sprite = trickList[i].sprite;
        }
    }
}
