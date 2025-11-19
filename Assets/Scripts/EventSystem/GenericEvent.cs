using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEvent <T> where T: class, new()
{
    private Dictionary<string, T> map = new Dictionary<string, T>();

    //Dividimos los eventos en canales para filtra que se suscribe y que no.
    //Cada vez que creamos un nuevo evento, comprobamos si el canal ya existe.
    //Si el canal no existe, se añade al dictionary map
    public T Get(string channel = "")      
    {
        map.TryAdd(channel, new T());
        return map[channel];
    }
}
