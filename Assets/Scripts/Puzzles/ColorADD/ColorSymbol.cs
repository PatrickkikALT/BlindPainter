using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum ColorAdd {
  Red = 1,
  Yellow = 2,
  Blue = 4
}

public class ColorSymbol : MonoBehaviour {
  public ColorAdd colorType;
}