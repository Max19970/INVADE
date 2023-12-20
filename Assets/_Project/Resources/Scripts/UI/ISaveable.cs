using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface ISaveable 
{
    void Load();
    void Save();
    void SetDefault();
}
