using UnityEngine;
using UnityEngine.UI;

#if TMP_PRO
using TMPro;
#endif

public class NoBreakingLine : MonoBehaviour
{
#if TMP_PRO
    private TextMeshProUGUI m_TextMeshPro;
    private TMP_InputField m_TextMeshInput;
#endif
    private Text m_Text;
    private InputField m_TextInput;

    private string lastText = "";

    void Start()
    {
#if TMP_PRO
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
        m_TextMeshInput = GetComponent<TMP_InputField>();
#endif
        m_Text = GetComponent<Text>();
        m_TextInput = GetComponent<InputField>();

#if TMP_PRO
        if (m_TextMeshPro != null)
        {
            m_TextMeshPro.RegisterDirtyVerticesCallback(OnTextChange);
            OnTextChange();
        }
#endif
        if (m_Text != null)
        {
            m_Text.RegisterDirtyVerticesCallback(OnTextChange);
            OnTextChange();
        }

#if TMP_PRO
        if (m_TextMeshInput != null)
        {
            m_TextMeshInput.onValueChanged.AddListener(OnInputChangeTMP);
            OnInputChangeTMP(m_TextMeshInput.text);
        }
#endif
        if (m_TextInput != null)
        {
            m_TextInput.onValueChanged.AddListener(OnInputChangeUGUI);
            OnInputChangeUGUI(m_TextInput.text);
        }
    }

    private string ReplaceSpaces(string input, float? fontSize = null)
    {
        if (fontSize.HasValue)
        {
            float halfSize = fontSize.Value / 2f;
            return input.Replace(" ", $"<space={halfSize}>");
        }
        else
        {
            return input.Replace(" ", "\u00A0"); // 使用不换行空格
        }
    }

    private string WrapWithNoBrTag(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        const string startTag = "<nobr>";
        const string endTag = "</nobr>";

        if (input.StartsWith(startTag) && input.EndsWith(endTag))
            return input;

        return $"{startTag}{input}{endTag}";
    }

    public void OnTextChange()
    {
#if TMP_PRO
        if (m_TextMeshPro)
        {
            string replaced = WrapWithNoBrTag(ReplaceSpaces(m_TextMeshPro.text, m_TextMeshPro.fontSize));
            if (replaced != lastText)
            {
                lastText = replaced;
                m_TextMeshPro.text = replaced;
            }
            return;
        }
#endif
        if (m_Text)
        {
            string replaced = ReplaceSpaces(m_Text.text); // 非 TMP 使用不换行空格
            if (replaced != lastText)
            {
                lastText = replaced;
                m_Text.text = replaced;
            }
        }
    }

#if TMP_PRO
    private void OnInputChangeTMP(string input)
    {
        string replaced = WrapWithNoBrTag(ReplaceSpaces(input, m_TextMeshInput.pointSize));
        if (replaced != input)
        {
            m_TextMeshInput.SetTextWithoutNotify(replaced);
        }
    }
#endif

    private void OnInputChangeUGUI(string input)
    {
        string replaced = ReplaceSpaces(input); // 非 TMP 使用不换行空格
        if (replaced != input)
        {
            m_TextInput.SetTextWithoutNotify(replaced);
        }
    }
}
