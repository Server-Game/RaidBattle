using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour {

	public InputField GetInput;
    private string text;

    public void InputOK()
    {
		if (string.IsNullOrEmpty(GetInput.text.ToString())) { return; }

        text = GetInput.text.ToString();
        GetInput.text = null;
    }

    public void InputCancel()
	{
		GetInput.text = null;
	}
}
