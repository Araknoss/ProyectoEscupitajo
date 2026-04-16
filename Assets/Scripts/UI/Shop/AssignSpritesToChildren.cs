using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignSpritesToGrandChildren : MonoBehaviour
{
    [SerializeField] private List<Trick> trickList;

    private void Start()
    {
        // 1. Ordenar por ID
        trickList.Sort((a, b) => a.id.CompareTo(b.id));

        // 2. Obtener SOLO hijos de los hijos (nietos)
        List<Image> grandChildrenImages = new List<Image>();

        foreach (Transform child in transform) // hijos
        {
            foreach (Transform grandChild in child) // hijos de los hijos
            {
                Image img = grandChild.GetComponent<Image>();
                if (img != null)
                {
                    grandChildrenImages.Add(img);
                }
            }
        }

        // 3. Asignar sprites
        int count = Mathf.Min(grandChildrenImages.Count, trickList.Count);

        for (int i = 0; i < count; i++)
        {
            grandChildrenImages[i].sprite = trickList[i].sprite;
        }
    }
}
