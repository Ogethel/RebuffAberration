using UnityEngine;

namespace ALS.Aberration
{
    /// <summary>
    /// Base class for ScriptableObjects that need a public description field.
    /// </summary>
    public class DescriptionSO : ScriptableObject
    {
        [TextArea] public string Description;
    }
}
